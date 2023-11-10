using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterFallState : CharacterAirState
{
    private eStateType _nextState;
    public CharacterFallState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _stateMachine.MovementSpeedMultiflier = 0.0f;
        _nextState = _stateMachine.previousStateType;

        StartAnimation(_stateMachine.Character.AnimationData.FallParameterHash);
        _stateMachine.Character.Sync?.SendC_MovePacket((int)eStateType.Fall, _stateMachine.Character.transform.position, _stateMachine.Character.Movement.FinalDirection);
    }

    public override void Exit()
    {
        base.Exit();

        _stateMachine.MovementSpeedMultiflier = 1.0f;

        StopAnimation(_stateMachine.Character.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (CheckGround())
        {
            _stateMachine.ChangeState(_nextState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _stateMachine.Character.Sync?.SendC_MovePacket((int)eStateType.Fall, _stateMachine.Character.transform.position, _stateMachine.Character.Movement.FinalDirection);
    }

    protected override void RunEvent(bool isRun)
    {
        if (isRun)
            _nextState = eStateType.Run;
    }

    protected override void MoveEvent(Vector2 direction)
    {
        MoveCharacter(direction);
        if (direction == Vector2.zero)
            _nextState = eStateType.Idle;
        else
        {
            if (_nextState != eStateType.Run)
                _nextState = eStateType.Walk;
        }
    }
}
