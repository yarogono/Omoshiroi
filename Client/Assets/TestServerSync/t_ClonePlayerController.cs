using Google.Protobuf.Protocol;
using System.Collections;
using UnityEngine;

public class t_ClonePlayerController : t_PlayerController
{
    void Update()
    {
        checkIdTest.text = "C_ID : " + Id;

        UpdateController();
    }

    protected virtual void UpdateController()
    {
        Debug.Log($"Clone State : {State}");
        switch (State)
        {
            case CreatureState.Idle:
                InitPos();
                break;
            case CreatureState.Moving:
                SyncPos();
                break;
            // case CreatureState.Skill:
            //     UpdateSkill();
            //     break;
            // case CreatureState.Dead:
            //     UpdateDead();
            //     break;
        }
    }

    public void InitPos() { }

    public void SyncPos()
    {
        transform.position = position;
    }
}
