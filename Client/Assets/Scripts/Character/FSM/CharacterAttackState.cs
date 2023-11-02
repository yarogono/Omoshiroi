using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : BaseState
{
    public CharacterAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 0.1f;
        base.Enter();

        StartAnimation(_stateMachine.Character.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Character.AnimationData.AttackParameterHash);
    }

    protected override void RunEvent(bool isRun)
    {
        // 아무고토 못하죠
    }

    protected override void AttackEvent(Vector2 direction)
    {
        base.AttackEvent(direction);
    }

    protected override void DodgeEvent()
    {
        base.DodgeEvent();
        _stateMachine.IsAttacking = false;
        _stateMachine.ChangeState(_stateMachine.DodgeState);
    }
}
