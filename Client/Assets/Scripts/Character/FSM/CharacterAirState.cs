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
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.AirParameterHash);
    }

    protected override void RunEvent(bool isRun)
    {
        // 아무고토 못하죠
    }

    protected override void MoveEvent(Vector2 direction)
    {
        // 아무고토 못하죠
    }

    protected override void AttackEvent(Vector2 direction)
    {
        // 아무고토 못하죠
    }

    protected override void AimEvent(Vector2 direction)
    {
        // 아무고토 못하죠
    }

    protected override void DodgeEvent()
    {
        base.DodgeEvent();
    }
}
