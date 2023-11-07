using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneStateMachine : StateMachine
{
    // Character Info
    public CloneDataContainer Clone { get; }
    public CloneSync Sync { get; }
    public CloneMovement Movement { get; }

    // Animation Info
    public int LayerInAnimator { get; }

    public CloneStateMachine(CloneDataContainer clone, int layerInAnimator)
    {
        Clone = clone;
        Sync = Clone.Sync;
        Movement = Clone.Movement;
        LayerInAnimator = layerInAnimator;
    }
}

public class CombineCloneStatemachine
{
    CloneStateMachine[] _stateMachine;

    public CombineCloneStatemachine(CloneDataContainer clone)
    {
        _stateMachine = new CloneStateMachine[2];

        _stateMachine[0] = new CloneStateMachine(clone, 0);
        _stateMachine[0].AddState(eStateType.Idle, new CloneIdleState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Walk, new CloneWalkState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Run, new CloneRunState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Dodge, new CloneDodgeState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Fall, new CloneFallState(_stateMachine[0]));
        _stateMachine[0].ChangeState(eStateType.Idle);

        _stateMachine[1] = new CloneStateMachine(clone, 1);
        _stateMachine[1].AddState(
            eStateType.ComboAttack,
            new CloneComboAttackState(_stateMachine[1])
        );
        _stateMachine[1].AddState(eStateType.Aim, new CloneAimState(_stateMachine[1]));
        _stateMachine[1].AddState(eStateType.None, new CloneNoneState(_stateMachine[1]));
        _stateMachine[1].ChangeState(eStateType.None);
    }

    public void Update()
    {
        foreach (var stateMachine in _stateMachine)
            stateMachine.Update();
    }

    public void PhysicsUpdate()
    {
        foreach (var stateMachine in _stateMachine)
            stateMachine.PhysicsUpdate();
    }

    public void ChangeState(eStateType state)
    {
        foreach (var stateMachine in _stateMachine)
        {
            if (stateMachine.States.ContainsKey(state))
            {
                stateMachine.ChangeState(state);
                break;
            }
        }
    }

    public void SetAnimation(eStateType state, float normalizeTime)
    {
        if (
            _stateMachine[0].currentStateType != state && _stateMachine[1].currentStateType != state
        )
            ChangeState(state);
        foreach (var stateMachine in _stateMachine)
        {
            if (stateMachine.currentStateType == state)
            {
                stateMachine.CurrentState.SetAnimation(
                    stateMachine.Clone.Animator,
                    stateMachine.LayerInAnimator,
                    normalizeTime
                );
                break;
            }
        }
    }
}
