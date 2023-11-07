using System.Collections;
using System.Collections.Generic;
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

    public CombineCloneStatemachine(CloneDataContainer character)
    {
        _stateMachine = new CloneStateMachine[2];

        _stateMachine[0] = new CloneStateMachine(character, 0);
        //_stateMachine[0].AddState(eStateType.Idle, new CharacterIdleState(_stateMachine[0]));
        //_stateMachine[0].ChangeState(eStateType.Idle);

        _stateMachine[1] = new CloneStateMachine(character, 1);
        //_stateMachine[1].AddState(eStateType.ComboAttack, new CharacterComboAttackState(_stateMachine[1]));
        //_stateMachine[1].ChangeState(eStateType.None);
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
}