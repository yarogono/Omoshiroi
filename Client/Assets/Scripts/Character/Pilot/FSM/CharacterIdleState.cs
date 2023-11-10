using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterIdleState : CharacterGroundState
{
    private bool _isRun;
    public CharacterIdleState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _isRun = false;
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.IdleParameterHash);
    }

    protected override void MoveEvent(Vector2 direction)
    {
        base.MoveEvent(direction);
        if (direction != Vector2.zero)
        {
            if (_isRun)
                _stateMachine.ChangeState(eStateType.Run);
            else
                _stateMachine.ChangeState(eStateType.Walk);
        }
    }

    protected override void RunEvent(bool isRun)
    {
        _isRun = isRun;
    }

    protected override void DodgeEvent()
    {
        _stateMachine.ChangeState(eStateType.Dodge);
    }
}
