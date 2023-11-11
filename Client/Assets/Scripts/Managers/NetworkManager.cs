using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;

public class NetworkManager : CustomSingleton<NetworkManager>
{
    ServerSession _session = new ServerSession();

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        ConfigManager.LoadConfig();

        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = IPAddress.Parse(ConfigManager.Config.gameServerIpAddr);
        IPEndPoint endPoint = new IPEndPoint(ipAddr, ConfigManager.Config.gameServerPort);

        Connector connector = new Connector();

        connector.Connect(
            endPoint,
            () =>
            {
                return _session;
            },
            1
        );

        C_EnterGame enterGamePacket = new C_EnterGame { Player = new ObjectInfo() };
        enterGamePacket.Player.Name = "BAEINHO";
        enterGamePacket.Player.PosInfo = new PositionInfo()
        {
            PosX = 0,
            PosY = 1.58f,
            PosZ = 0
        };

        Send(enterGamePacket);
    }

    void Update()
    {
        List<PacketMessage> list = PacketQueue.Instance.PopAll();

        if (list.Count != 0)
        {
            foreach (PacketMessage packet in list)
            {
                Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(
                    packet.Id
                );
                handler?.Invoke(_session, packet.Message);
            }
        }
    }

    public void Send(IMessage packet)
    {
        _session.Send(packet);
    }

    public void LeaveGame()
    {
        PilotSync pilotPlayerController = ObjectManager.Instance.pilotSync;
        if (pilotPlayerController == null)
            return;

        int id = pilotPlayerController.Id;

        C_LeaveGame leaveGamePacket = new C_LeaveGame { PlayerId = id };

        Send(leaveGamePacket);
    }
}
