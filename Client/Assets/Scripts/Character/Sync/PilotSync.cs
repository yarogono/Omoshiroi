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

        // SendC_MovePacket();
    }

    public void SendC_MovePacket(int state, Vector3 posInfo, Vector3 velInfo)
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

    public void SendC_AimPacket(int state, Vector3 velInfo) { }

    public void SendC_BattlePacket(int state, float animTime, Vector3 posInfo, Vector3 velInfo) { }

    public void SendC_AttackPacket(int comboIndex, Vector3 posInfo, Vector3 velInfo) { }
}
