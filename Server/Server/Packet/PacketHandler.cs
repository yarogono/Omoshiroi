using Server;
using Server.Session;
using ServerCore;


class PacketHandler
{

    public static void C_ChatHandler(PacketSession session, IPacket packet)
    {
        C_Chat chatPacket = packet as C_Chat;
        ClientSession clientSession = session as ClientSession;

        if (chatPacket == null)
            return;

        Console.WriteLine($"{chatPacket.accountName} {chatPacket.chatMessage}");
        GameRoom room = clientSession.Room;

        room.Push(
            () => room.Broadcast(chatPacket.Write())
        );
    }
}
