
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    // Character Info
    public CharacterDataContainer Character { get; }
    public BaseInput InputActions { get; }
    public CharacterMovement Movement { get; }
    public float CharacterSpeedMultiflier { get => Character.Stats.RunMultipiler; }
    public float CharacterBaseSpeed { get => Character.Stats.MoveSpeed; }

    // State Info
    public float MovementSpeedMultiflier { set => Movement.SpeedMultiflier = value; }
    public int LayerInAnimator { get; }
    public Vector2 AttackDirection { get; set; }

    public CharacterStateMachine(CharacterDataContainer character, int layerInAnimator)
    {
        Character = character;
        InputActions = character.InputActions;
        Movement = character.Movement;
        LayerInAnimator = layerInAnimator;
    }
}