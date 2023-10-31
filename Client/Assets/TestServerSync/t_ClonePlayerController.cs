using Google.Protobuf.Protocol;
using System.Collections;
using UnityEngine;

public class t_ClonePlayerController : t_PlayerController
{
    void Update()
    {
        UpdateController();
        Debug.Log($"Clone State : {State}");
        Debug.Log($"Clone position : {position}");
    }

    protected virtual void UpdateController()
    {
        Debug.Log($"Clone State : {State}");
        switch (State)
        {
            case CreatureState.Moving:
                UpdateMoving();
                break;
            // case CreatureState.Skill:
            //     UpdateSkill();
            //     break;
            // case CreatureState.Dead:
            //     UpdateDead();
            //     break;
        }
    }

    protected virtual void UpdateMoving() { }

    public void SyncPos()
    {
        transform.position = position;
    }
}
