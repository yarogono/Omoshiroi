using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalkState : CharacterGroundState
{
    private bool _needSend;
    public CharacterWalkState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.WalkParameterHash);
        _stateMachine.Character.Sync?.SendC_MovePacket((int)_stateMachine.currentStateType, _stateMachine.Character.transform.position, _stateMachine.Character.Controller.velocity);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.WalkParameterHash);
    }
    public override void Update()
    {
        base.Update();
        if (_needSend)
        {
            _stateMachine.Character.Sync?.SendC_MovePacket((int)_stateMachine.currentStateType, _stateMachine.Character.transform.position, _stateMachine.Character.Controller.velocity);
            _needSend = false;
        }

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!_needSend)
            _needSend = true;
    }

    protected override void RunEvent(bool isRun)
    {
        if (isRun)
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
