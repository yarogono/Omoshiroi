using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAimState : CharacterGroundState
{
    public CharacterAimState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 0.5f;
        base.Enter();

        StartAnimation(_stateMachine.Character.AnimationData.AimParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Character.AnimationData.AimParameterHash);
    }

    protected override void MoveEvent(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        base.MoveEvent(direction);
    }

    protected override void AimEvent(Vector2 direction)
    {
        base.AimEvent(direction);
    }

    protected override void DodgeEvent()
    {
        _stateMachine.ChangeState(_stateMachine.DodgeState);
        base.DodgeEvent();
    }

    protected override void RunEvent(bool isRun)
    {
        if (isRun) { _stateMachine.ChangeState(_stateMachine.RunState); }
        base.RunEvent(isRun);
    }

    protected override void AttackEvent(Vector2 direction)
    {
        _stateMachine.ChangeState(_stateMachine.ComboAttackState);
        base.AttackEvent(direction);
    }
}
