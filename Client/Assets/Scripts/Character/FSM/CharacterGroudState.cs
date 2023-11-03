using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterGroundState : BaseState
{
    public CharacterGroundState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 1.0f;
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.GroundParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // 이거 잘 작동하는지 확인이 필요함
        // => Mathf.Abs(_stateMachine.Character.Controller.velocity.y) > Mathf.Abs(Physics.gravity.y * Time.fixedDeltaTime)
        if (!_stateMachine.Character.Controller.isGrounded
            && Mathf.Abs(_stateMachine.Character.Controller.velocity.y) > Mathf.Abs(Physics.gravity.y * Time.fixedDeltaTime))
        {
            _stateMachine.ChangeState(_stateMachine.FallState);
            return;
        }
    }

}
