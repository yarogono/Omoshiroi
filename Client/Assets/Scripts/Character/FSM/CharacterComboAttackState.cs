using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComboAttackState : CharacterAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    private CharacterStats stats;
    // AttackInfoData attackInfoData;

    public CharacterComboAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Character.AnimationData.ComboAttackParameterHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        int comboIndex = _stateMachine.ComboIndex;
        stats = _stateMachine.Character.Stats;
        // TODO
        // 콤보에 따른 공격 정보를 가지고 올 것
        // attackInfoData = _stateMachine.Character.Data.AttakData.GetAttackInfo(comboIndex);
        _stateMachine.Character.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Character.AnimationData.ComboAttackParameterHash);

        if (!alreadyApplyCombo)
            _stateMachine.ComboIndex = 0;
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        //if (attackInfoData.ComboStateIndex == -1) return;

        if (!_stateMachine.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        //_stateMachine.Movement.AddImpact(_stateMachine.Character.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(_stateMachine.Character.Animator, "Attack");
        //if (normalizedTime < 1f)
        //{
        //    if (normalizedTime >= attackInfoData.ForceTransitionTime)
        //        TryApplyForce();

        //    if (normalizedTime >= attackInfoData.ComboTransitionTime)
        //        TryComboAttack();
        //}
        //else
        //{
        //    if (alreadyApplyCombo)
        //    {
        //        _stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
        //        _stateMachine.ChangeState(_stateMachine.ComboAttackState);
        //    }
        //    else
        //    {
        //        _stateMachine.ChangeState(_stateMachine.IdleState);
        //    }
        //}
    }
}
