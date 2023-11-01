using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : CustomSingleton<NetworkManager>
{
    ServerSession _session = new ServerSession();

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

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
        enterGamePacket.Player.Name = "test";
        enterGamePacket.Player.PosInfo = new PositionInfo() { PosX = 0, PosY = 0 };
        enterGamePacket.Player.StatInfo = null;

        Send(enterGamePacket);
    }

    void Update()
    {
        List<PacketMessage> list = PacketQueue.Instance.PopAll();
        foreach (PacketMessage packet in list)
        {
            Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(
                packet.Id
            );
            if (handler != null)
                handler.Invoke(_session, packet.Message);
        }
    }

    public void Send(IMessage packet)
    {
        _session.Send(packet);
    }
}
