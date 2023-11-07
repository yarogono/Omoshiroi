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

        C_Move movePacket = new C_Move { PosInfo = PosInfo };
        NetworkManager.Instance.Send(movePacket);
    }

    public void SencC_AttackPacket() { }

    public void SencC_FallPacket() { }

    public void SencC_DodgePacket() { }

    public void SencC_AimPacket() { }
}
