using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Net;
using System.Numerics;

namespace DummyClient
{
    public class ServerSession : PacketSession
    {
        public void Send(IMessage packet)
        {
            string msgName = packet.Descriptor.Name.Replace("_", string.Empty);
            MsgId msgId = (MsgId)Enum.Parse(typeof(MsgId), msgName);
            ushort size = (ushort)packet.CalculateSize();
            byte[] sendBuffer = new byte[size + 4];
            Array.Copy(BitConverter.GetBytes((ushort)(size + 4)), 0, sendBuffer, 0, sizeof(ushort));
            Array.Copy(BitConverter.GetBytes((ushort)msgId), 0, sendBuffer, 2, sizeof(ushort));
            Array.Copy(packet.ToByteArray(), 0, sendBuffer, 4, size);

            Send(new ArraySegment<byte>(sendBuffer));
        }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            C_EnterGame enterGamePacket = new C_EnterGame();

            enterGamePacket.Player = new ObjectInfo();

            enterGamePacket.Player.Name = "test";
            enterGamePacket.Player.PosInfo = new PositionInfo() { PosX = 0, PosY = 0 };

            Send(enterGamePacket);

            Thread.Sleep(1000);

            C_FarmingBoxOpen farmingBoxOpen = new C_FarmingBoxOpen();
            farmingBoxOpen.FarmingBoxId = 50331648;

            Send(farmingBoxOpen);
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}
