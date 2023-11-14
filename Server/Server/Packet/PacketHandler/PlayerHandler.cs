﻿using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Game.Object;
using Server.Game.Room;
using Server;
using ServerCore;


partial class PacketHandler
{
    public static void C_EnterGameHandler(PacketSession session, IMessage packet)
    {
        C_EnterGame enterGamePacket = (C_EnterGame)packet;
        ClientSession clientSession = (ClientSession)session;

        Player sessionPlayer = clientSession.MyPlayer;

        GameRoom room = GameLogic.Instance.Find(1);

        var packetPlayer = enterGamePacket.Player;

        Console.WriteLine($"Enter User : {packetPlayer.Name} plyer");
        Player player = ObjectManager.Instance.Add<Player>();
        {
            player.Info.Name = enterGamePacket.Player.Name;
            player.Info.PosInfo = packetPlayer.PosInfo;

            StatInfo stat = enterGamePacket.Player.StatInfo;
            player.Stat.MergeFrom(stat);

            player.Session = clientSession;
        }

        clientSession.MyPlayer = player;

        room.Push(room.EnterGame, player);
    }


    public static void C_MoveHandler(PacketSession session, IMessage packet)
    {
        C_Move movePacket = (C_Move)packet;
        ClientSession clientSession = (ClientSession)session;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.HandleMove, player, movePacket);
    }

    public static void C_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        C_LeaveGame leaveGamePacket = (C_LeaveGame)packet;
        ClientSession clientSession = (ClientSession)session;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        if (player.Id != leaveGamePacket.PlayerId)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.LeaveGame, leaveGamePacket.PlayerId);
    }

    public static void C_ChangeHpHandler(PacketSession session, IMessage packet)
    {
        C_ChangeHp hpDamagePacket = (C_ChangeHp)packet;
        ClientSession clientSession = (ClientSession)session;

        if (hpDamagePacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null) 
            return;

        player.HpDamage(hpDamagePacket);
    }

    public static void C_AimHandler(PacketSession session, IMessage packet)
    {
        C_Aim aimPacket = (C_Aim)packet;
        ClientSession clientSession = (ClientSession)session;

        if (aimPacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.HandleAim, player, aimPacket);
    }

    public static void C_BattleHandler(PacketSession session, IMessage packet)
    {
        C_Battle battlePacket = (C_Battle)packet;
        ClientSession clientSession = (ClientSession)session;

        if (battlePacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.HandleBattle, player, battlePacket);
    }

    public static void C_AttackHandler(PacketSession session, IMessage packet)
    {
        C_Attack attackPacket = (C_Attack)packet;
        ClientSession clientSession = (ClientSession)session;

        if (attackPacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.HandleAttack, player, attackPacket);
    }


    public static void C_PongHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = (ClientSession)session;
        clientSession.HandlePong();
    }
}
