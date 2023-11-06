using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFallState : CharacterAirState
{
    public CharacterFallState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(_stateMachine.Character.AnimationData.FallParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Character.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (_stateMachine.Character.Controller.isGrounded)
        {
            _stateMachine.ChangeState(eStateType.Idle);
            return;
        }
    }

    protected override void RunEvent(bool isRun)
    {
        // 아무고토 못하죠
    }

    protected override void MoveEvent(Vector2 direction)
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
