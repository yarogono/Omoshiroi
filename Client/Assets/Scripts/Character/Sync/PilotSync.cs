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

    public void SendC_MovePacket(int state, Vector3 posInfo, Vector3 velInfo)
    {
        State = state;

        PosInfo = new PositionInfo
        {
            PosX = posInfo.x,
            PosY = posInfo.y,
            PosZ = posInfo.z
        };

        VelInfo = new VelocityInfo
        {
            VelX = velInfo.x,
            VelY = velInfo.y,
            VelZ = velInfo.z,
        };

        C_Move movePacket = new C_Move
        {
            State = State,
            PosInfo = PosInfo,
            VelInfo = VelInfo
        };

        NetworkManager.Instance.Send(movePacket);
    }

    public void SendC_AimPacket(int state, Vector3 velInfo)
    {
        State = state;

        VelInfo = new VelocityInfo
        {
            VelX = velInfo.x,
            VelY = velInfo.y,
            VelZ = velInfo.z,
        };

        C_Aim aimPacket = new C_Aim { State = State, VelInfo = VelInfo };
        NetworkManager.Instance.Send(aimPacket);
    }

    public void SendC_BattlePacket(int state, float animTime, Vector3 posInfo, Vector3 velInfo)
    {
        State = state;

        AnimTime = animTime;

        PosInfo = new PositionInfo
        {
            PosX = posInfo.x,
            PosY = posInfo.y,
            PosZ = posInfo.z
        };
        VelInfo = new VelocityInfo
        {
            VelX = velInfo.x,
            VelY = velInfo.y,
            VelZ = velInfo.z,
        };

        C_Battle battlePacket = new C_Battle
        {
            State = State,
            AnimTime = animTime,
            PosInfo = PosInfo,
            VelInfo = VelInfo
        };
        NetworkManager.Instance.Send(battlePacket);
    }

    public void SendC_AttackPacket(int comboIndex, Vector3 posInfo, Vector3 velInfo)
    {
        ComboIndex = comboIndex;

        PosInfo = new PositionInfo
        {
            PosX = posInfo.x,
            PosY = posInfo.y,
            PosZ = posInfo.z
        };
        VelInfo = new VelocityInfo
        {
            VelX = velInfo.x,
            VelY = velInfo.y,
            VelZ = velInfo.z,
        };

        C_Attack attackPacket = new C_Attack
        {
            ComboIndex = ComboIndex,
            PosInfo = PosInfo,
            VelInfo = VelInfo
        };
        NetworkManager.Instance.Send(attackPacket);
    }

    public void SendC_ChangeHpPacket(int hp)
    {
        // NetworkManager.Instance.Send();
    }
}
