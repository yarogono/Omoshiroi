using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodgeState : CharacterGroundState
{
    float passedTime;
    float duration;

    public CharacterDodgeState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        duration = _stateMachine.Character.Stats.DodgeTime;
    }

    public override void Enter()
    {
        base.Enter();
        passedTime = 0.0f;
        StartAnimation(_stateMachine.Character.AnimationData.DodgeParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.DodgeParameterHash);
    }

    public override void Update()
    {
        base.Update();
        // 무적 시간이 지나면 Event에 반응해서 state를 변하도록
        if (passedTime < duration)
            passedTime += Time.deltaTime;
        else
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }


    }

    protected override void MoveEvent(Vector2 direction)
    {
        // 아무고토 못하죠
    }

    protected override void RunEvent(bool isRun)
    {
        // 아무고토 못하죠
    }

    protected override void AttackEvent(Vector2 direction)
    {
        // 아무고토 못하죠
    }

    protected override void AimEvent(Vector2 direction)
    {
        // 아무고토 못하죠
    }

    protected override void DodgeEvent()
    {
        // 아무고토 못하죠
    }
}
