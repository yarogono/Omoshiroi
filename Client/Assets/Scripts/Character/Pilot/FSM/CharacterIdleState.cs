using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterIdleState : CharacterGroundState
{
    private bool _isRun;
    private bool _needUpdate;
    private bool _stoped;
    public CharacterIdleState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _isRun = false;
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.IdleParameterHash);
        _stateMachine.Character.Sync?.SendC_MovePacket((int)_stateMachine.currentStateType, _stateMachine.Character.transform.position, _stateMachine.Character.Controller.velocity);
        _stoped = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (_needUpdate && _stoped)
        {
            if (_stateMachine.Character.Controller.velocity.sqrMagnitude > 0)
                _stateMachine.Character.Sync?.SendC_MovePacket((int)_stateMachine.currentStateType, _stateMachine.Character.transform.position, _stateMachine.Character.Controller.velocity);
            else
            {
                _stoped = true;
                _stateMachine.Character.Sync?.SendC_MovePacket((int)_stateMachine.currentStateType, _stateMachine.Character.transform.position, Vector3.zero);
            }
            _needUpdate = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!_needUpdate && !_stoped)
            _needUpdate = true;
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
