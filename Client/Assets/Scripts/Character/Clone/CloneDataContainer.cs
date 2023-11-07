using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDataContainer : MonoBehaviour
{
    [SerializeField] public Inventory1 Inven { get; }
    [field: SerializeField] public CharacterStats Stats { get; private set; }
    [field: SerializeField] public EquipSystem Equipments { get; private set; }
    public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterAnimationData AnimationData { get; private set; }
    public CloneSync Sync { get; private set; }
    public CloneMovement Movement { get; private set; }

    private CombineCloneStatemachine _stateMachine;


    private void Awake()
    {
        Movement = GetComponent<CloneMovement>();
        Animator = GetComponent<Animator>();
        Sync = GetComponent<CloneSync>();
        AnimationData.Initialize();
    }

    void Start()
    {
        if (Stats.cbs != null)
            Stats.Initialize();

        if (Equipments == null)
            Equipments = new EquipSystem();

        _stateMachine = new CombineCloneStatemachine(this);
    }

    void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }
}
