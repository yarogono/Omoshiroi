using UnityEngine;

public class BaseState : IState
{
    protected CharacterStateMachine _stateMachine;


    public BaseState(CharacterStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        
    }

    // Input
    protected virtual void AddInputActionsCallbacks()
    {
        BaseInput input = _stateMachine.InputActions;
        input.OnMoveEvent += MoveEvent;
        input.OnRunEvent += RunEvent;
        input.OnAttackEvent += AttackEvent;
        input.OnAimEvent += AimEvent;
        input.OnDodgeEvent += DodgeEvent;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        BaseInput input = _stateMachine.InputActions;
        input.OnMoveEvent -= MoveEvent;
        input.OnRunEvent -= RunEvent;
        input.OnAttackEvent -= AttackEvent;
        input.OnAimEvent -= AimEvent;
        input.OnDodgeEvent -= DodgeEvent;
    }

    protected virtual void RunEvent(bool isRun)
    {
        // 달리기
    }

    protected virtual void MoveEvent(Vector2 direction)
    {
        // 이동
        // MoveCharacter(direction);
    }

    protected virtual void AttackEvent(Vector2 direction)
    {
        // TODO
        // 공격
    }

    protected virtual void AimEvent(Vector2 direction)
    {
        // TODO
        // 조준
    }

    protected virtual void DodgeEvent()
    {
        // TODO
        // HealthSystem에 무적 적용
    }

    protected void StartAnimation(int animationHash)
    {
        _stateMachine.Character.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        _stateMachine.Character.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, int layer, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(layer);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(layer);
        if (animator.IsInTransition(layer) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(layer) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected void MoveCharacter(Vector2 direction)
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        direction.Normalize();

        _stateMachine.Movement.ControlMove(forward * direction.y + right * direction.x);
    }
}
