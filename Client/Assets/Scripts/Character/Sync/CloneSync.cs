using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class CloneSync : SyncModule
{
    public event Action<Vector3, float, int, Vector3> OnCloneEvent;

    protected override void Update()
    {
        base.Update();

        SyncPosition();
    }

    public void SyncPosition()
    {
        gameObject.transform.position = new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
    }

    public void CallCloneEvent(Vector3 velocity, float animTime, int state, Vector3 position)
    {
        OnCloneEvent?.Invoke(velocity, animTime, state, position);
    }

    public void ReceiveS_MovePacket() { }

    public void ReceiveS_AttackPacket() { }

    public void ReceiveS_FallPacket() { }

    public void ReceiveS_DodgePacket() { }

    public void ReceiveS_AimPacket() { }
}
