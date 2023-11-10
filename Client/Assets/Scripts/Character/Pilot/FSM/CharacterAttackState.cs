using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : BaseState
{
    public bool IsAttacking { get; protected set; }
    public int ComboIndex { get; protected set; }
    public CharacterAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _stateMachine.MovementSpeedMultiflier = 0.1f;
        IsAttacking = true;

        StartAnimation(_stateMachine.Character.AnimationData.AttackParameterHash);
        _stateMachine.Character.Sync?.SendC_BattlePacket((int)eStateType.Attack, 0.0f, _stateMachine.Character.transform.position, _stateMachine.AttackDirection);
    }

    public override void Exit()
    {
        base.Exit();

        _stateMachine.MovementSpeedMultiflier = 1.0f;
        IsAttacking = false;

        StopAnimation(_stateMachine.Character.AnimationData.AttackParameterHash);
    }
}
