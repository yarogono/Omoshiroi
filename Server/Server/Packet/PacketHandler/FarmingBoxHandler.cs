using Google.Protobuf;
using Server.Game.Object;
using Server.Game.Room;
using Server;
using ServerCore;
using Google.Protobuf.Protocol;
using Google.Protobuf.Collections;

partial class PacketHandler
{
    public static void C_FarmingBoxOpenHandler(PacketSession session, IMessage packet)
    {
        C_FarmingBoxOpen farmingBoxOpenPacket = (C_FarmingBoxOpen)packet;
        ClientSession clientSession = (ClientSession)session;

        if (farmingBoxOpenPacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        int farmingBoxId = farmingBoxOpenPacket.FarmingBoxId;
        room.Push(room.FarmingBoxOpen, player, farmingBoxId);
    }


    public static void C_FarmingBoxCloseHandler(PacketSession session, IMessage packet)
    {
        C_FarmingBoxClose farmingBoxClosePacket = (C_FarmingBoxClose)packet;
        ClientSession clientSession = (ClientSession)session;

        if (farmingBoxClosePacket == null)
            return;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.FarmingBoxClose, farmingBoxClosePacket);
    }
}

