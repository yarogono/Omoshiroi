using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
}

public class NPCDataContainer : DataContainer
{
    public CharacterController Controller { get; private set; }
    public CharacterMovement Movement { get; private set; }
    public BaseInput InputActions { get; private set; }
    public PilotSync Sync { get; private set; }

    [Header("AI 관련 필드")]
    private AIState aiState;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public List<Vector3> destinations;
    [SerializeField] public float minWanderDistance = 5f;
    [SerializeField] public float maxWanderDistance = 10f;
    [SerializeField] public float minWanderWaitTime = 3f;
    [SerializeField] public float maxWanderWaitTime = 8f;
    [SerializeField] public float detectDistance;
    [SerializeField] public float safeDistance;
    [SerializeField] public float attackDistance;
    [SerializeField] private float playerDistance;

    //private CombineStateMachine stateMachine;

    [Header("테스트용 착용아이템")]
    [SerializeField]
    private BaseItem[] TestEquipItem;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Movement = GetComponent<CharacterMovement>();
        InputActions = GetComponent<BaseInput>();
        Animator = GetComponent<Animator>();
        Sync = GetComponent<PilotSync>();

        AnimationData.Initialize();
    }

    private void Start()
    {
        //if (Equipments == null)
        //    Equipments = new EquipSystem();

        //foreach (var item in TestEquipItem)
        //    Equipments.Equip(item);

        //SetState(AIState.Wandering);


        //stateMachine = new CombineStateMachine(this);
        //SpriteRotator.Register(this);
    }

    void Update()
    {
        //stateMachine.Update();
        //playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        //Animator.SetBool("Moving", aiState != AIState.Idle);

        //switch (aiState)
        //{
        //    case AIState.Idle: PassiveUpdate(); break;
        //    case AIState.Wandering: PassiveUpdate(); break;
        //    case AIState.Attacking: AttackingUpdate(); break;
        //    case AIState.Fleeing: FleeingUpdate(); break;
        //}
    }

    private void FixedUpdate()
    {
        //stateMachine.PhysicsUpdate();
    }

    private void OnDrawGizmos()
    {
        Ray downRay = new Ray(transform.position, Vector3.down);

        Gizmos.color = Color.red;

        Gizmos.DrawRay(downRay);
    }

    private void FleeingUpdate()
    {
        //if (agent.remainingDistance < 0.1f)
        //{
        //    agent.SetDestination(GetFleeLocation());
        //}
        //else
        //{
        //    SetState(AIState.Wandering);
        //}
    }

    private void AttackingUpdate()
    {
        //if (playerDistance > attackDistance || !IsPlaterInFireldOfView())
        //{
        //    agent.isStopped = false;
        //    NavMeshPath path = new NavMeshPath();
        //    if (agent.CalculatePath(PlayerController.instance.transform.position, path))
        //    {
        //        agent.SetDestination(PlayerController.instance.transform.position);
        //    }
        //    else
        //    {
        //        SetState(AIState.Fleeing);
        //    }
        //}
        //else
        //{
        //    agent.isStopped = true;
        //    if (Time.time - lastAttackTime > attackRate)
        //    {
        //        lastAttackTime = Time.time;
        //        PlayerController.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage);
        //        animator.speed = 1;
        //        animator.SetTrigger("Attack");
        //    }
        //}
    }

    private void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    //bool IsPlaterInFireldOfView()
    //{
    //    Vector3 directionToPlayer = PlayerController.instance.transform.position - transform.position;
    //    float angle = Vector3.Angle(transform.forward, directionToPlayer);
    //    return angle < fieldOfView * 0.5f;
    //}

    private void SetState(AIState newState)
    {
        aiState = newState;
        switch (aiState)
        {
            case AIState.Idle:
                {
                    agent.speed = Stats.MoveSpeed;
                    agent.isStopped = true;
                }
                break;
            case AIState.Wandering:
                {
                    agent.speed = Stats.MoveSpeed;
                    agent.isStopped = false;
                }
                break;

            case AIState.Attacking:
                {
                    agent.speed = Stats.MoveSpeed;
                    agent.isStopped = false;
                }
                break;
            case AIState.Fleeing:
                {
                    agent.speed = Stats.MoveSpeed * Stats.RunMultiplier;
                    agent.isStopped = false;
                }
                break;
        }

        Animator.speed = agent.speed / Stats.MoveSpeed;
    }

    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle)
        {
            return;
        }
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }


    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    //Vector3 GetFleeLocation()
    //{
    //    NavMeshHit hit;

    //    NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

    //    int i = 0;
    //    while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
    //    {

    //        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
    //        i++;
    //        if (i == 30)
    //            break;
    //    }

    //    return hit.position;
    //}

    //float GetDestinationAngle(Vector3 targetPos)
    //{
    //    return Vector3.Angle(transform.position - PlayerController.instance.transform.position, transform.position + targetPos);
    //}
}
