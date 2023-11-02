using Google.Protobuf.Protocol;
using System.Collections;
using UnityEngine;

public class t_ClonePlayerController : t_PlayerController
{
    protected override void Update()
    {
        base.Update();

        UpdateController();
    }

    protected virtual void UpdateController()
    {
        // switch (State)
        // {
        //     case CreatureState.Moving:
        //         SyncPos();
        //         break;
        //     // case CreatureState.Skill:
        //     //     UpdateSkill();
        //     //     break;
        //     // case CreatureState.Dead:
        //     //     UpdateDead();
        //     //     break;
        // }

        SyncPos();
    }

    public void SyncPos()
    {
        Debug.Log($"S_r {Id} => {PosInfo}");
        gameObject.transform.position = new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
    }
}
