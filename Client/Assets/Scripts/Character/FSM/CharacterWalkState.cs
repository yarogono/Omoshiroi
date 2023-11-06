using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalkState : CharacterGroundState
{
    public CharacterWalkState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.WalkParameterHash);
    }

    protected override void RunEvent(bool isRun)
    {
        if(isRun)
        {
            _stateMachine.ChangeState(eStateType.Run);
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
