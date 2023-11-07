using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class CloneSync : SyncModule
{
    protected override void Update()
    {
        base.Update();

        SyncPosition();
    }

    public void SyncPosition()
    {
        gameObject.transform.position = new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
    }

    public void ReceiveS_MovePacket() { }

    public void ReceiveS_AttackPacket() { }

    public void ReceiveS_FallPacket() { }

    public void ReceiveS_DodgePacket() { }

    public void ReceiveS_AimPacket() { }
}
