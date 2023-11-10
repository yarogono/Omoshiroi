using Unity.VisualScripting;
using UnityEngine;

public class CharacterAirState : BaseState
{
    public CharacterAirState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.AirParameterHash);
        _stateMachine.Character.Sync?.SendC_MovePacket((int)eStateType.Fall, _stateMachine.Character.transform.position, _stateMachine.Character.Movement.FinalDirection);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.AirParameterHash);
    }
}
