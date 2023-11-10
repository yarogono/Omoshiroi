using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAimState : BaseState
{
    public CharacterAimState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        _stateMachine.MovementSpeedMultiflier = 0.5f;

        StartAnimation(_stateMachine.Character.AnimationData.AimParameterHash);
        _stateMachine.Character.Sync?.SendC_AimPacket((int)eStateType.Aim, _stateMachine.AttackDirection);
    }

    public override void Exit()
    {
        base.Exit();

        _stateMachine.MovementSpeedMultiflier = 1.0f;

        StopAnimation(_stateMachine.Character.AnimationData.AimParameterHash);
    }

    public override void PhysicsUpdate()
    {
        if (!CheckGround())
            _stateMachine.ChangeState(eStateType.None);
    }

    protected override void AimEvent(Vector2 direction)
    {
        // TODO
        if (direction.magnitude < 100f)
            _stateMachine.ChangeState(eStateType.None);
        base.AimEvent(direction);
        _stateMachine.Character.Sync?.SendC_AimPacket((int)eStateType.Aim, _stateMachine.AttackDirection);
    }

    protected override void AttackEvent(Vector2 direction)
    {
        _stateMachine.ChangeState(eStateType.ComboAttack);
    }
}
