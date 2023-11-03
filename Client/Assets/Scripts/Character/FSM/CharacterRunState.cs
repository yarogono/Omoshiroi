using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRunState : CharacterGroundState
{
    public CharacterRunState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _stateMachine.MovementSpeedModifier = _stateMachine.CharacterSpeedMultiflier;
        StartAnimation(_stateMachine.Character.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.RunParameterHash);
    }

    protected override void RunEvent(bool isRun)
    {
        if (!isRun)
        {
            _stateMachine.ChangeState(_stateMachine.WalkState);
        }
    }

    protected override void MoveEvent(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        base.MoveEvent(direction);
    }

    protected override void DodgeEvent()
    {
        base.DodgeEvent();
        _stateMachine.ChangeState(_stateMachine.DodgeState);
    }

    protected override void AttackEvent(Vector2 direction)
    {
        base.AttackEvent(direction);
        _stateMachine.ChangeState(_stateMachine.ComboAttackState);
    }

    protected override void AimEvent(Vector2 direction)
    {
        base.AimEvent(direction);
        _stateMachine.ChangeState(_stateMachine.AimState);
    }
}
