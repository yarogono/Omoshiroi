using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour
{
    [SerializeField] public Inventory1 Inven { get;}
    [SerializeField] public CharacterStats Stats { get;}
    [SerializeField] public EquipSystem Equipments { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterAnimationData AnimationData { get; private set; }
    public CharacterController Controller { get; private set; }
    public CharacterMovement Movement { get; private set; }
    public BaseInput InputActions { get; private set; }

    private CharacterStateMachine stateMachine;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Movement = GetComponent<CharacterMovement>();
        InputActions = GetComponent<BaseInput>();
    }

    private void Start()
    {
        stateMachine = new CharacterStateMachine(this);
    }

    void Update()
    {
        
    }
}
