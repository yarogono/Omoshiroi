using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Game.Object;
using Server.Game.Room;
using Server;
using ServerCore;


class PacketHandler
{

    public static void C_EnterGameHandler(PacketSession session, IMessage packet)
    {
        C_EnterGame enterGamePacket = packet as C_EnterGame;
        ClientSession clientSession = session as ClientSession;


        Player player = clientSession.MyPlayer;

        GameRoom room = Server.RoomManager.Instance.Find(1);

        var packetPlayer = enterGamePacket.Player;

        Console.WriteLine($"{packetPlayer.Name} plyer");
        player = ObjectManager.Instance.Add<Player>();
        {
            player.Info.Name = packetPlayer.Name;
            player.Info.PosInfo.State = CreatureState.Idle;
            player.Info.PosInfo.MoveDir = MoveDir.Down;
            player.Info.PosInfo.PosX = packetPlayer.PosInfo.PosX;
            player.Info.PosInfo.PosY = packetPlayer.PosInfo.PosY;

            StatInfo stat = null;
            player.Stat.MergeFrom(stat);

            player.Session = clientSession;
        }

        clientSession.MyPlayer = player;

        room.Push(room.EnterGame, player);
    }


    public static void C_MoveHandler(PacketSession session, IMessage packet)
    {
        C_Move movePacket = packet as C_Move;
        ClientSession clientSession = session as ClientSession;

        //Console.WriteLine($"C_Move ({movePacket.PosInfo.PosX}, {movePacket.PosInfo.PosY})");

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        room.Push(room.HandleMove, player, movePacket);
    }
}
