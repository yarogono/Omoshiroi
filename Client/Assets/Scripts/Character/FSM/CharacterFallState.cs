using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFallState : CharacterAirState
{
    public CharacterFallState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(_stateMachine.Character.AnimationData.FallParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Character.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (_stateMachine.Character.Controller.isGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
    }
}
