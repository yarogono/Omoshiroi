using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterStateMachine : StateMachine
{
    // Character Info
    public CharacterDataContainer Character { get; }
    public BaseInput InputActions { get; }
    public CharacterMovement Movement { get; }
    public float CharacterSpeedMultiflier { get => Character.Stats.runMultiplier; }

    // States
    public CharacterIdleState IdleState { get; }
    public CharacterWalkState WalkState { get; }
    public CharacterRunState RunState { get; }
    public CharacterFallState FallState { get; }
    public CharacterComboAttackState ComboAttackState { get; }
    public CharacterDodgeState DodgeState { get; }
    public CharacterAimState AimState { get; }

    // State Info
    public bool IsAttacking { get; set; }
    public float MovementSpeedModifier { set => Movement.SpeedMultiflier = value; }
    public int ComboIndex { get; set; }

    public CharacterStateMachine(CharacterDataContainer character)
    {
        Character = character;
        InputActions = character.InputActions;
        Movement = character.Movement;

        IdleState = new CharacterIdleState(this);
        WalkState = new CharacterWalkState(this);
        RunState = new CharacterRunState(this);
        FallState = new CharacterFallState(this);
        ComboAttackState = new CharacterComboAttackState(this);
        DodgeState = new CharacterDodgeState(this);
        AimState = new CharacterAimState(this);
    }
}
