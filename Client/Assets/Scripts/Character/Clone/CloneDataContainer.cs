using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDataContainer : MonoBehaviour
{
    public CloneSync Sync { get; private set; }
    public CloneMovement Movement { get; private set; }

    private CombineCloneStatemachine _stateMachine;


    private void Awake()
    {

    }

    void Start()
    {
        Sync = GetComponent<CloneSync>();
        Movement = GetComponent<CloneMovement>();

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
