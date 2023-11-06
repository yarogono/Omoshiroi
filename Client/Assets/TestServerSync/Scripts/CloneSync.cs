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
    }

    protected virtual void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Moving:
                SyncPosition();
                break;
            // case CreatureState.Skill:
            //     UpdateSkill();
            //     break;
            // case CreatureState.Dead:
            //     UpdateDead();
            //     break;
        }
    }

    public void SyncPosition()
    {
        gameObject.transform.position = new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
    }
}
