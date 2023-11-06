using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CombineStateMachine
{
    CharacterStateMachine[] _stateMachine;

    public CombineStateMachine(CharacterDataContainer character)
    {
        _stateMachine = new CharacterStateMachine[2];

        _stateMachine[0] = new CharacterStateMachine(character, 0);
        _stateMachine[0].AddState(eStateType.Idle, new CharacterIdleState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Walk, new CharacterWalkState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Run, new CharacterRunState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Fall, new CharacterFallState(_stateMachine[0]));
        _stateMachine[0].AddState(eStateType.Dodge, new CharacterDodgeState(_stateMachine[0]));
        _stateMachine[0].ChangeState(eStateType.Idle);

        _stateMachine[1] = new CharacterStateMachine(character, 1);
        _stateMachine[1].AddState(eStateType.ComboAttack, new CharacterComboAttackState(_stateMachine[1]));
        _stateMachine[1].AddState(eStateType.Aim, new CharacterAimState(_stateMachine[1]));
        _stateMachine[1].AddState(eStateType.None, new CharacterNoneState(_stateMachine[1]));
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
}
