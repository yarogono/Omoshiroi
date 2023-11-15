using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    // Character Info
    public CharacterDataContainer Character { get; }
    public BaseInput InputActions { get; }
    public CharacterMovement Movement { get; }
    public float CharacterSpeedMultiflier
    {
        get => Character.Stats.RunMultiplier;
    }
    public float CharacterBaseSpeed
    {
        get => Character.Stats.MoveSpeed;
    }
    public CombineStateMachine combineStateMachine { get; }

    // State Info
    public float MovementSpeedMultiflier
    {
        set
        {
            if (Movement != null)
                Movement.SpeedMultiflier = value;
        }
    }
    public int LayerInAnimator { get; }
    public Vector3 AttackDirection { get; set; }

    public CharacterStateMachine(
        CombineStateMachine combine,
        CharacterDataContainer character,
        int layerInAnimator
    )
    {
        Character = character;
        InputActions = character.InputActions;
        if (character.Movement != null)
            Movement = character.Movement;
        LayerInAnimator = layerInAnimator;
        combineStateMachine = combine;
    }
}
