using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNoneState : BaseState
{
    public CharacterNoneState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter() { base.Enter(); }
    public override void Exit() { base.Exit(); }

    protected override void AimEvent(Vector2 direction)
    {
        _stateMachine.ChangeState(eStateType.Aim);
    }

    protected override void AttackEvent(Vector2 direction)
    {
        _stateMachine.ChangeState(eStateType.ComboAttack);
    }
}
