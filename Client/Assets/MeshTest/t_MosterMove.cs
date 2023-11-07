using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class t_MonsterMove : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = goal.position;
    }
}
