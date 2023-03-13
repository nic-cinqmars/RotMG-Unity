using RotmgClient.Cryptography;
using RotmgClient.Networking.Data;
using RotmgClient.Networking.Packets;
using RotmgClient.Networking.Packets.Incoming;
using RotmgClient.Networking.Packets.Outgoing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Json;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace RotmgClient.Networking
{
    public class Client : MonoBehaviour
    {
        [SerializeField]
        private string host = "127.0.0.1";
        [SerializeField]
        private int port = 2050;

        [SerializeField]
        private string guid;
        [SerializeField]
        private string password;

        private TcpClient client;
        private NetworkStream? nStream;
        private PacketBuffer packetBuffer;

        public int newTickCount = 0;

        private int playerID = -1;
        private int charID = 1;

        private bool isConnected = false;

        private delegate void PacketCallback<T>(T packet) where T : Packet;
        private Dictionary<PacketId, Type> incomingPackets;
        private Dictionary<Type, object> packetCallbacks;

        void Start() 
        {
            SetupPacketHooks();
        }

        private void OnApplicationQuit()
        {
            Disconnect();
        }

        private void SetupPacketHooks()
        {
            incomingPackets = new Dictionary<PacketId, Type>();
            packetCallbacks = new Dictionary<Type, object>();

            IEnumerable<Type> incomingTypes =
                    from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "RotmgClient.Networking.Packets.Incoming" && t != typeof(IncomingPacket)
                    select t;

            MethodInfo? hookPacket = GetType().GetMethod("HookPacket", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (Type type in incomingTypes)
            {
                Packet? packet = (Packet?)Activator.CreateInstance(type);
                if (packet != null)
                {
                    incomingPackets.Add(packet.packetId, type);

                    string methodName = "On" + type.Name.Replace("Packet", "");
                    MethodInfo? hookCallback = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
                    if (hookPacket != null && hookCallback != null)
                    {
                        MethodInfo hookPacketGeneric = hookPacket.MakeGenericMethod(type);
                        Type genericType = typeof(PacketCallback<>).MakeGenericType(type);
                        hookPacketGeneric.Invoke(this, new object[] { Convert.ChangeType(Delegate.CreateDelegate(genericType, this, hookCallback), genericType) });
                    }
                }
            }

            /*
            packetCallbacks = new Dictionary<Type, object>();
            HookPacket<QueuePingPacket>(OnQueuePing);
            HookPacket<PingPacket>(OnPing);
            HookPacket<MapInfoPacket>(OnMapInfo);
            HookPacket<AccountListPacket>(OnAccountList);
            HookPacket<UpdatePacket>(OnUpdate);
            */
        }

        private void HookPacket<T>(PacketCallback<T> callback) where T : Packet
        {
            if (!packetCallbacks.ContainsKey(typeof(T)))
            {
                Debug.Log("Added callback : " + callback.Method.Name + " to packet : " + typeof(T));
                packetCallbacks.Add(typeof(T), callback);
            }
            else
                throw new InvalidOperationException("Callback already bound!");
        }

        public void Connect()
        {
            if (!isConnected)
            {
                HelloPacket state = new HelloPacket();
                state.buildVersion = "27.7.X13";
                state.gameID = -2;
                state.guid = guid;
                state.password = password;
                state.secret = "";
                state.keyTime = -1;
                state.key = new byte[0];
                state.mapJSON = "";
                state.hash = "242a6266d2f11b6420af33a94882c4d3";

                client = new TcpClient();
                client.NoDelay = true;
                client.BeginConnect(IPAddress.Parse(host), port, OnConnected, state);
            }
            else
            {
                Debug.Log("Tried to connect but already connected!");
            }
        }

        private void OnConnected(IAsyncResult ar)
        {
            Debug.Log("Connected");
            isConnected = true;
            client.EndConnect(ar);
            packetBuffer = new PacketBuffer();
            nStream = client.GetStream();

            Packet? packet = ar.AsyncState as Packet;

            if (packet != null)
            {
                SendPacket(packet);
                BeginRead(0, 4);
            }
            else
            {
                Disconnect();
            }
        }

        private void SendPacket(Packet packet)
        {
            if (nStream == null)
            {
                Disconnect();
                return;
            }

            //Debug.Log("Sending packet " + packet.packetId.ToString());
            MemoryStream mS = new MemoryStream();
            using (NWriter pW = new NWriter(mS))
            {
                pW.Write(0);
                pW.Write((byte)packet.packetId);
                packet.Write(pW);

                byte[] data = mS.ToArray();
                NWriter.BlockCopyInt32(data, data.Length);
                packet.Crypt(data, 5, data.Length - 5);

                nStream.Write(data, 0, data.Length);
            }
        }

        private void BeginRead(int offset, int amount)
        {
            if (nStream == null)
            {
                Disconnect();
                return;
            }

            nStream.BeginRead(packetBuffer.bytes, offset, amount, ReadMessage, null);
        }

        private void ReadMessage(IAsyncResult ar)
        {
            if (nStream == null)
            {
                Disconnect();
                return;
            }

            if (!nStream.CanRead)
                return;

            int read = nStream.EndRead(ar);
            packetBuffer.Advance(read);

            if (read == 0)
            {
                Disconnect();
                return;
            }
            else if (packetBuffer.index == 4)
            {
                packetBuffer.Resize(IPAddress.NetworkToHostOrder(
                    BitConverter.ToInt32(packetBuffer.bytes, 0)));
                BeginRead(packetBuffer.index, packetBuffer.BytesRemaining());
            }
            else if (packetBuffer.BytesRemaining() > 0)
            {
                BeginRead(packetBuffer.index, packetBuffer.BytesRemaining());
            }
            else
            {
                RC4.getInstance().CryptRecieve(packetBuffer.bytes, 5, packetBuffer.bytes.Length - 5);
                using (NReader pR = new NReader(new MemoryStream(packetBuffer.bytes)))
                {
                    pR.ReadInt32();
                    byte id = pR.ReadByte();
                    PacketId packetID = (PacketId)id;

                    //Debug.Log("Recieved packet " + packetID.ToString());

                    if (incomingPackets.TryGetValue(packetID, out Type? packetType))
                    {
                        Packet? packet = (Packet?)Activator.CreateInstance(packetType);
                        if (packet == null)
                        {
                            Debug.Log("Error creating packet with id " + (int)packetID);
                            return;
                        }
                        packet.Read(pR);
                        if (packetCallbacks.TryGetValue(packetType, out object? callback))
                        {
                            _ = (callback as Delegate).Method.Invoke((callback as Delegate).Target, new object[] { packet });
                        }
                        else
                        {
                            Debug.Log("Couldn't find callback for packet with id " + packetID.ToString());
                        }
                    }
                    else
                    {
                        Debug.Log("Couldn't find packet with id " + packetID.ToString());
                    }
                }
                packetBuffer.Reset();
                BeginRead(0, 4);
            }
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                isConnected = false;
                nStream.Close();
                client.Close();
                packetBuffer.Dispose();
                Debug.Log("Client disconnected");
            }
        }

        private void OnQueuePing(QueuePingPacket packet)
        {
            QueuePongPacket pongPacket = new QueuePongPacket();
            pongPacket.serial = packet.serial;
            pongPacket.time = (int)(DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime()).TotalMilliseconds;
            SendPacket(pongPacket);
        }

        private void OnPing(PingPacket packet)
        {
            PongPacket pongPacket = new PongPacket();
            pongPacket.Serial = packet.serial;
            pongPacket.Time = (int)(DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime()).TotalMilliseconds;
            SendPacket(pongPacket);
        }

        private void OnMapInfo(MapInfoPacket packet)
        {
            LoadPacket loadPacket = new LoadPacket();
            loadPacket.CharId = charID;
            loadPacket.IsFromArena = false;
            SendPacket(loadPacket);
        }

        private void OnAccountList(AccountListPacket packet)
        {
            //Todo
        }

        private void OnUpdate(UpdatePacket packet)
        {
            UpdateAckPacket updateAckPacket = new UpdateAckPacket();
            SendPacket(updateAckPacket);

            for (int i = 0; i < packet.Tiles.Length; i++)
            {
                GameMap.Instance.AddTile(packet.Tiles[i]);
            }

            /* DEBUG
            for (int i = 0; i < packet.Tiles.Length; i++)
            {
                Debug.Log("New tile : " + packet.Tiles[i].x + ", " + packet.Tiles[i].y + ", type=" + packet.Tiles[i].type);
            }

            for (int i = 0; i < packet.NewObjs.Length; i++)
            {
                ObjectStatusData objectStatus = packet.NewObjs[i].status;
                Debug.Log("New object : objectStatus={x:" + objectStatus.pos.x + ",y:" + objectStatus.pos.y + ",objectID:" + objectStatus.objectId + "}, objectType=" + packet.NewObjs[i].objectType);
            }

            for (int i = 0; i < packet.Drops.Length; i++)
            {
                Debug.Log("Drops : " + packet.Drops[i]);
            }
            */
        }

        private void OnNewTick(NewTickPacket packet)
        {
            newTickCount++;
            if (newTickCount % 100 == 0)
            {
                PlayerText pT = new PlayerText();
                pT.Text = "Sending messages! " + (newTickCount / 100) + 1;
                SendPacket(pT);
            }
            /*
            // Debug
            for (int i = 0; i < packet.Statuses.Length; i++)
            {
                Debug.Log(packet.Statuses[i].objectId + " : ");
                for (int j = 0; j < packet.Statuses[i].stats.Length; j++)
                {
                    Debug.Log("-> Stat " + packet.Statuses[i].stats[j].statType + " : " + packet.Statuses[i].stats[j].statValue);
                }
                Debug.Log("-> Pos : " + packet.Statuses[i].pos.x + ", " + packet.Statuses[i].pos.y);
            }
            */
        }

        private void OnCreateSuccess(CreateSuccessPacket packet) 
        {
            playerID = packet.ObjectId;
            charID = packet.CharId;
        }

        private void OnGlobalNotification(GlobalNotificationPacket packet)
        {
            Debug.Log("- Global Notification -");
            switch (packet.Text)
            {
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Debug.Log("yellow");
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Debug.Log("red");
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Debug.Log("green");
                    break;
                case "purple":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Debug.Log("purple");
                    break;
                case "showKeyUI":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Debug.Log("showKeyUI");
                    break;
                case "giftChestOccupied":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Debug.Log("GiftChestOccupied");
                    break;
                case "giftChestEmpty":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Debug.Log("GiftChestEmpty");
                    break;
                case "beginnersPackage":
                    Console.ForegroundColor = ConsoleColor.White;
                    Debug.Log("BeginnersPackage");
                    break;
            }
            if (packet.Type != 0)
            {
                switch (packet.Type)
                {
                    case GlobalNotificationPacket.ADD_ARENA:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Debug.Log("Add arena : " + packet.Text);
                        break;
                    case GlobalNotificationPacket.DELETE_ARENA:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Debug.Log("Delete arena : " + packet.Text);
                        break;
                }
            }
            Console.ResetColor();
        }

        private void OnFailure(FailurePacket packet) 
        {
            Debug.Log("FAILUREPACKET :");
            switch (packet.ErrorId)
            {
                case FailurePacket.INCORRECT_VERSION:
                    Debug.Log("Incorrect version");
                    break;
                case FailurePacket.BAD_KEY:
                    Debug.Log("Incorrect key");
                    break;
                case FailurePacket.INVALID_TELEPORT_TARGET:
                    Debug.Log("Invalid teleport target");
                    break;
                case FailurePacket.EMAIL_VERIFICATION_NEEDED:
                    Debug.Log("Email verification needed");
                    break;
                case FailurePacket.JSON_DIALOG:
                    Debug.Log("JSON error");
                    break;
                default:
                    Debug.Log("Error");
                    Debug.Log(packet.ErrorDescription);
                    break;
            }

        }
    }
}
