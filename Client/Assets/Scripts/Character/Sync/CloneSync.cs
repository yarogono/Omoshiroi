using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class CloneSync : SyncModule
{
    public event Action<int, Vector3, Vector3> OnMoveEvent;
    public event Action<int, Vector3> OnAimEvent;
    public event Action<int, float, Vector3, Vector3> OnBattleEvent;
    public event Action<int, Vector3, Vector3> OnAttackEvent;

    protected override void Update()
    {
        base.Update();
    }

    public void CallMoveEvent(int state, Vector3 posInfo, Vector3 velInfo)
    {
        OnMoveEvent?.Invoke(state, posInfo, velInfo);
    }

    public void CallAimEvent(int state, Vector3 velInfo)
    {
        OnAimEvent?.Invoke(state, velInfo);
    }

    public void CallBattleEvent(int state, float animTime, Vector3 posInfo, Vector3 velInfo)
    {
        OnBattleEvent?.Invoke(state, animTime, posInfo, velInfo);
    }

    public void CallAttackEvent(int comboIndex, Vector3 posInfo, Vector3 velInfo)
    {
        OnAttackEvent?.Invoke(comboIndex, posInfo, velInfo);
    }

    public void ReceiveS_MovePacket() { }

    public void ReceiveS_AttackPacket() { }

    public void ReceiveS_FallPacket() { }

    public void ReceiveS_DodgePacket() { }

    public void ReceiveS_AimPacket() { }
}
