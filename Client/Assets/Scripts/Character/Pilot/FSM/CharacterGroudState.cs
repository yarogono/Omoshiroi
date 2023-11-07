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
        // TODO
        // 이거 잘 작동하는지 확인이 필요함
        if (!CheckGround()
            && Mathf.Abs(_stateMachine.Character.Controller.velocity.y) > Mathf.Abs(Physics.gravity.y * Time.fixedDeltaTime))
        {
            _stateMachine.ChangeState(eStateType.Fall);
            return;
        }
    }

    protected override void MoveEvent(Vector2 direction)
    {
        MoveCharacter(direction);
        _stateMachine.Character.Sync?.SendC_MovePacket();
    }
}
