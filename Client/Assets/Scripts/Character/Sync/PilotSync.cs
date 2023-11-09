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
    }

    public void SendC_MovePacket()
    {
        position = transform.position;

        C_Sync movePacket = new C_Sync { Player = new ObjectInfo { Position = P_Vector3 } };
        NetworkManager.Instance.Send(movePacket);
    }

    public void SendC_AttackPacket() { }

    public void SendC_FallPacket() { }

    public void SendC_DodgePacket() { }

    public void SendC_AimPacket() { }
}
