
using DummyClient;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;

class PacketHandler
{
    internal static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePacket = packet as S_EnterGame;
        //Managers.Object.Add(enterGamePacket.Player, myPlayer: true);
        Console.WriteLine($"{enterGamePacket.Player.Name} player Enter");
    }

    internal static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {

    }

    internal static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = packet as S_Spawn;
        ServerSession serverSession = session as ServerSession;

        if (serverSession == null)
            return;

        if (spawnPacket == null)
            return;
        foreach (ObjectInfo obj in spawnPacket.Objects)
        {
            Console.WriteLine(obj.Name);
            //Managers.Object.Add(obj, myPlayer: false);
        }
    }

    internal static void S_DespawnHandler(PacketSession session, IMessage packet)
    {

    }

    internal static void S_MoveHandler(PacketSession session, IMessage packet)
    {

    }

    internal static void S_ChangeHpHandler(PacketSession session, IMessage packet)
    {

    }

    internal static void S_DieHandler(PacketSession session, IMessage packet)
    {

    }
}
