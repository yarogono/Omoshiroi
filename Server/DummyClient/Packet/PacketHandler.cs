
using ServerCore;

class PacketHandler
{
    internal static void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chatPacket = (S_Chat)packet;
        Console.WriteLine( $"{chatPacket.accountName} {chatPacket.chatMessage}");
    }
}
