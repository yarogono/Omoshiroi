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

    public void CallMoveEvent(int state, PositionInfo posInfo, VelocityInfo velInfo)
    {
        OnMoveEvent?.Invoke(
            state,
            ToVector3(posInfo.PosX, posInfo.PosY, posInfo.PosZ),
            ToVector3(velInfo.VelX, velInfo.VelY, velInfo.VelZ)
        );
        Debug.Log($"Clone Move ({velInfo.VelX},{velInfo.VelY},{velInfo.VelZ})");
    }

    public void CallAimEvent(int state, VelocityInfo velInfo)
    {
        OnAimEvent?.Invoke(state, ToVector3(velInfo.VelX, velInfo.VelY, velInfo.VelZ));
        Debug.Log($"Clone Aim ({velInfo.VelX},{velInfo.VelY},{velInfo.VelZ})");
    }

    public void CallBattleEvent(
        int state,
        float animTime,
        PositionInfo posInfo,
        VelocityInfo velInfo
    )
    {
        OnBattleEvent?.Invoke(
            state,
            animTime,
            ToVector3(posInfo.PosX, posInfo.PosY, posInfo.PosZ),
            ToVector3(velInfo.VelX, velInfo.VelY, velInfo.VelZ)
        );
        Debug.Log($"Clone Battle ({animTime}) ({velInfo.VelX},{velInfo.VelY},{velInfo.VelZ})");
    }

    public void CallAttackEvent(int comboIndex, PositionInfo posInfo, VelocityInfo velInfo)
    {
        OnAttackEvent?.Invoke(
            comboIndex,
            ToVector3(posInfo.PosX, posInfo.PosY, posInfo.PosZ),
            ToVector3(velInfo.VelX, velInfo.VelY, velInfo.VelZ)
        );
        Debug.Log($"Clone Attack ({comboIndex}) ({velInfo.VelX},{velInfo.VelY},{velInfo.VelZ})");
    }
}
