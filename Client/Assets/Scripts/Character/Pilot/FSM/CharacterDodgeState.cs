using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterDodgeState : BaseState
{
    float passedTime;
    float duration;
    private bool alreadyAppliedForce;

    private eStateType _nextState;
    private bool _isRun;
    private bool _isRunInState
    {
        get
        {
            if (_stateMachine.States.ContainsKey(eStateType.Run))
                return (_stateMachine.States[eStateType.Run] as CharacterRunState).IsRun;
            else
                return false;
        }
    }
    public CharacterDodgeState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        duration = _stateMachine.Character.Stats.DodgeTime;
    }

    public override void Enter()
    {
        base.Enter();
        passedTime = 0.1f;
        alreadyAppliedForce = false;
        _nextState = _stateMachine.previousStateType;
        _isRun = _isRunInState;
        StartAnimation(_stateMachine.Character.AnimationData.DodgeParameterHash);
        _stateMachine.Character.Sync?.SendC_BattlePacket((int)eStateType.Dodge, 0.0f, _stateMachine.Character.transform.position, _stateMachine.Character.Controller.velocity);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.DodgeParameterHash);
    }

    public override void Update()
    {
        float normalizedTime = GetNormalizedTime(_stateMachine.Character.Animator, _stateMachine.LayerInAnimator, "Dodge");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= passedTime)
            {
                TryApplyForce();
            }
            _stateMachine.Character.Sync?.SendC_BattlePacket((int)eStateType.Dodge, normalizedTime, _stateMachine.Character.transform.position, _stateMachine.Character.Controller.velocity);
        }
        else
        {
            _stateMachine.ChangeState(_nextState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;
        if (_stateMachine.Movement == null) return;
        _stateMachine.Movement.AddImpact(_stateMachine.Movement.ControlDirection * _stateMachine.CharacterBaseSpeed * _stateMachine.CharacterSpeedMultiflier * 5, 0.5f);
    }

    protected override void MoveEvent(Vector2 direction)
    {
        MoveCharacter(direction);
        if (direction == Vector2.zero)
            _nextState = eStateType.Idle;
        else
        {
            if (_isRun)
                _nextState = eStateType.Run;
            else
                _nextState = eStateType.Walk;
        }
    }

    protected override void RunEvent(bool isRun)
    {
        _isRun = isRun;
        if (_isRun)
            _nextState = eStateType.Run;
        else if (_nextState != eStateType.Idle)
            _nextState = eStateType.Walk;
    }
}
