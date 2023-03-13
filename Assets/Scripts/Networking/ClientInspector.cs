using UnityEngine;
using UnityEditor;
using RotmgClient.Networking;

[CustomEditor(typeof(Client))]
public class ClientInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Client client = (Client)target;
        if (GUILayout.Button("Connect"))
        {
            client.Connect();
        }
        if (GUILayout.Button("Disconnect"))
        {
            client.Disconnect();
        }
    }
}
