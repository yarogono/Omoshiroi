using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDataContainer : DataContainer
{
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
        SpriteRotator.Register(this);
    }

    void Update() { }

    private void FixedUpdate() { }
}
