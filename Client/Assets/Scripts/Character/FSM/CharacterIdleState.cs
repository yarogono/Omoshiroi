using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterGroundState
{
    public CharacterIdleState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    protected override void MoveEvent(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _stateMachine.ChangeState(_stateMachine.WalkState);
            base.MoveEvent(direction);
        }
    }
}
