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
            player.Info.Name = $"Player_{player.Id}";
            player.Info.Position = packetPlayer.Position;

            StatInfo stat = null;
            player.Stat.MergeFrom(stat);

            player.Session = clientSession;
        }

        clientSession.MyPlayer = player;

        room.Push(room.EnterGame, player);
    }


    public static void C_SyncHandler(PacketSession session, IMessage packet)
    {
        C_Sync syncPacket = packet as C_Sync;
        ClientSession clientSession = session as ClientSession;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

        if (player.Id != syncPacket.Player.ObjectId)
            return;

        room.Push(room.HandleMove, player, syncPacket);
    }

    public static void C_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        C_LeaveGame leaveGamePacket = packet as C_LeaveGame;
        ClientSession clientSession = session as ClientSession;

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

    public static void C_HpDamageHandler(PacketSession session, IMessage packet)
    {
        C_HpDamage leaveGamePacket = packet as C_HpDamage;
        ClientSession clientSession = session as ClientSession;

        Player player = clientSession.MyPlayer;
        if (player == null)
            return;
    }
}
