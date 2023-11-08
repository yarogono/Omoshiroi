using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class CloneSync : SyncModule
{
    public event Action<UnityEngine.Vector3, float, int, UnityEngine.Vector3> OnCloneEvent;

    protected override void Update()
    {
        base.Update();

        SyncPosition();
    }

    public void SyncPosition()
    {
        gameObject.transform.position = new UnityEngine.Vector3(
            ObjectInfo.Position.X,
            ObjectInfo.Position.Y,
            ObjectInfo.Position.Z
        );
    }

    public void CallCloneEvent(
        UnityEngine.Vector3 velocity,
        float animTime,
        int state,
        UnityEngine.Vector3 position
    )
    {
        OnCloneEvent?.Invoke(velocity, animTime, state, position);
    }

    public void ReceiveS_MovePacket() { }

    public void ReceiveS_AttackPacket() { }

    public void ReceiveS_FallPacket() { }

    public void ReceiveS_DodgePacket() { }

    public void ReceiveS_AimPacket() { }
}
