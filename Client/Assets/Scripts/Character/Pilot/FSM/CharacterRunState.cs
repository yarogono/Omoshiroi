using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRunState : CharacterWalkState
{
    public bool IsRun { get; private set; }
    public CharacterRunState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        _stateMachine.MovementSpeedMultiflier = _stateMachine.CharacterSpeedMultiflier;
        IsRun = true;
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        _stateMachine.MovementSpeedMultiflier = 1.0f;
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.RunParameterHash);
    }

    protected override void RunEvent(bool isRun)
    {
        if (!isRun)
        {
            IsRun = false;
            _stateMachine.ChangeState(eStateType.Walk);
        }
    }

    protected override void MoveEvent(Vector2 direction)
    {
        base.MoveEvent(direction);
        if (direction == Vector2.zero)
        {
            _stateMachine.ChangeState(eStateType.Idle);
        }
    }

    protected override void DodgeEvent()
    {
        _stateMachine.ChangeState(eStateType.Dodge);
    }
}
