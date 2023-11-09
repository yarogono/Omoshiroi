using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;
using System;

public class PilotSync : SyncModule
{
    protected override void Update()
    {
        base.Update();

        SendC_SyncPacket();
    }

    public void SendC_SyncPacket()
    {
        Player.PosInfo = new PositionInfo
        {
            PosX = transform.position.x,
            PosY = transform.position.y,
            PosZ = transform.position.z
        };

        C_Sync syncPacket = new C_Sync { Player = Player };
        NetworkManager.Instance.Send(syncPacket);
    }

    public void SendC_AttackPacket() { }

    public void SendC_FallPacket() { }

    public void SendC_DodgePacket() { }

    public void SendC_AimPacket() { }
}
