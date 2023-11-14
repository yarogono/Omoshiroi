using ServerCore;
using System.Net;
using Google.Protobuf.Protocol;
using Google.Protobuf;
using Server.Game.Room;
using Server.Game.Object;

namespace Server
{
    public class ClientSession : PacketSession
	{
		public Player MyPlayer { get; set; }
		public int SessionId { get; set; }


        #region Ping Pong Check
        long _pingpongTick = 0;
        public void Ping()
        {
            if (_pingpongTick > 0)
            {
                long delta = (System.Environment.TickCount64 - _pingpongTick);
                if (delta > 30 * 1000)
                {
                    Console.WriteLine("Disconnected by PingCheck");
                    Disconnect();
                    return;
                }
            }
            else if (_pingpongTick == 0)
            {
                Console.WriteLine("Disconnected by PingCheck : No Response");
                Disconnect();
                return;
            }

            S_Ping pingPacket = new S_Ping();
            Send(pingPacket);

            GameLogic.Instance.PushAfter(60000, Ping);
        }

        public void HandlePong()
        {
            _pingpongTick = System.Environment.TickCount64;
        }
        #endregion

        private const int SizeOffset = 0;
        private const int MsgIdOffset = 2;
        private const int HeaderSize = 4;

        public void Send(IMessage packet)
		{
			string msgName = packet.Descriptor.Name.Replace("_", string.Empty);
			MsgId msgId = (MsgId)Enum.Parse(typeof(MsgId), msgName);
            ushort size = (ushort)packet.CalculateSize();
            byte[] sendBuffer = new byte[size + 4];
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)(size + HeaderSize)), 0, sendBuffer, SizeOffset, sizeof(ushort));
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)msgId), 0, sendBuffer, MsgIdOffset, sizeof(ushort));
            Buffer.BlockCopy(packet.ToByteArray(), 0, sendBuffer, HeaderSize, size);

            Send(new ArraySegment<byte>(sendBuffer));
        }

		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

            GameLogic.Instance.PushAfter(60000, Ping);
        }

		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

		public override void OnDisconnected(EndPoint endPoint)
		{
			if (MyPlayer == null)
				return;

			GameRoom room = GameLogic.Instance.Find(1);
			room.Push(room.LeaveGame, MyPlayer.Info.ObjectId);

            SessionManager.Instance.Remove(this);

			Console.WriteLine($"OnDisconnected : {endPoint}");
		}

		public override void OnSend(int numOfBytes)
		{
			//Console.WriteLine($"Transferred bytes: {numOfBytes}");
		}
	}
}
