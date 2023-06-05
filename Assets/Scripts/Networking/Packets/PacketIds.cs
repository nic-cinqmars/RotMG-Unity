using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets
{
    public enum PacketId
    {
        Failure,
        CreateSuccess,
        Create,
        PlayerShoot,
        Move,
        PlayerText,
        Text,
        ServerPlayerShoot,
        Damage,
        Update,
        Notification,
        NewTick,
        InvSwap,
        UseItem,
        ShowEffect,
        Hello,
        Goto,
        InvDrop,
        InvResult,
        Reconnect,
        MapInfo,
        Load,
        Teleport,
        UsePortal,
        Death,
        Buy,
        BuyResult,
        Aoe,
        PlayerHit,
        EnemyHit,
        AoeAck,
        ShootAck,
        SquareHit,
        EditAccountList,
        AccountList,
        QuestObjId,
        CreateGuild,
        GuildResult,
        GuildRemove,
        GuildInvite,
        AllyShoot,
        EnemyShoot,
        Escape,
        InvitedToGuild,
        JoinGuild,
        ChangeGuildRank,
        PlaySound,
        Reskin,
        GotoAck
    }
}
