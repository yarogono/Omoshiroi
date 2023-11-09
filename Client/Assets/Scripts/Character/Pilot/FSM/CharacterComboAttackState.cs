using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComboAttackState : CharacterAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    private CharacterStats stats;
    private AttackInfo attackInfo;

    public CharacterComboAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.ComboAttackParameterHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        stats = _stateMachine.Character.Stats;

        attackInfo = (_stateMachine.Character.Equipments.GetEquippedItem(eItemType.Magic) as BaseMagic).AttackData.GetAttackInfo(ComboIndex);
        _stateMachine.Character.Animator.SetInteger(_stateMachine.Character.AnimationData.ComboIndexParameterHash, ComboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.ComboAttackParameterHash);

        if (!alreadyApplyCombo)
            ComboIndex = 0;
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfo.ComboStateIndex == -1) return;

        if (!IsAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;
        if (_stateMachine.Movement == null) return;
        Vector3 newDir = new Vector3() { x = _stateMachine.AttackDirection.normalized.x, z = _stateMachine.AttackDirection.normalized.y, y = 0.0f };
        //_stateMachine.Movement?.AddImpact(newDir * _stateMachine.CharacterBaseSpeed * _stateMachine.CharacterSpeedMultiflier, 0.1f);
        AttackManager.Instance.RqAttack(attackInfo.AttackType, _stateMachine.Character, _stateMachine.Character.transform.position, _stateMachine.AttackDirection);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(_stateMachine.Character.Animator, _stateMachine.LayerInAnimator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfo.ForceTransitionTime)
                TryApplyForce();
        }
        else
        {
            if (alreadyApplyCombo)
            {
                ComboIndex = attackInfo.ComboStateIndex;
                _stateMachine.ChangeState(eStateType.ComboAttack);
            }
            else
            {
                _stateMachine.ChangeState(eStateType.None);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        if (!CheckGround())
            _stateMachine.ChangeState(eStateType.None);
    }

    protected override void AttackEvent(Vector2 direction)
    {
        float normalizedTime = GetNormalizedTime(_stateMachine.Character.Animator, _stateMachine.LayerInAnimator, "Attack");
        if (normalizedTime >= attackInfo.ComboTransitionTime)
            TryComboAttack();
    }

    protected override void AimEvent(Vector2 direction)
    {
        base.AimEvent(direction);
    }
}
