using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//[최우선 구현]
//1.대기 상태: 배회 상태에서 현재 목표 지점까지 도달했을 때 랜덤한 시간 동안 아무 것도 하지 않고 제자리에 머무른다. 
//별다른 트리거 없이 지정된 시간이 경과하면 배회 상태로 진입한다.
//(base 의 통상 상태를 유지한다)

//[최우선 구현]
//2.배회 상태: 별다른 트리거가 없다면, 지정된 목표 지점들 중 하나를 선택하여 해당 위치까지 이동한다. 
//목표 지점에 도달했다면 대기 상태로 진입한다.
//(base 의 걷기 상태를 유지한다)

//[최우선 구현]
//3.추적 상태: 적대 대상을 확실하게 감지했을 때(일단은 일정 범위 내에 들어왔을 때 한정) 이 상태가 된다. 적대 대상의 위치를 목표 지점으로 지정하고,
//지속적으로 목표 지점을 갱신하며 접근한다. 적대 대상이 공격 범위 내에 들어왔다면 공격을 시도한다.
//(적대 대상에게 다가갈 때는 base 의 달리기 상태를 유지한다.)
//(적대 대상과 가까워졌을 때는 base 의 조준 - 공격 상태를 유지한다. 거리가 멀어지면 이를 반복한다)

//[추후 검토]
//4.수색 상태: 공격받았지만 아직 적대 대상을 확실하게 감지하지 못 했을 때(일단은 일정 범위 내에 들어왔을 때 한정), 
//추적 상태에서 적대 대상과 거리가 멀어졌을 때 이 상태가 된다. 감지 범위가 크게 증가하며,
//현재 위치 내에서 일정 범위를 배회한다. 이 상태에서 일정 시간 별다른 트리거가 없었다면 배회 상태로 진입한다.
//(base 의 걷기 상태를 유지한다)

//[추후 검토]
//5.도망 상태: 체력이 낮을 때 적대 대상을 확실하게 감지했다면, 일정 확률로 이 상태가 된다. 도망 상태는 일정 시간 유지되며, 적대 대상과 멀어지는 방향으로 이동한다. 
//이 상태가 유지되는 동안 대부분의 트리거를 무시한다. 
//(base 의 달리기 상태를 유지한다)

public class NPCAIController : MonoBehaviour
{
    //해당 컨트롤러를 가지는 객체의 데이터 컨테이너
    [SerializeField] private NPCDataContainer dataContainer;

    //길찾기용 컴포넌트
    [SerializeField] private NavMeshAgent agent;

    //목적지 좌표
    [SerializeField] private List<Vector3> wanderDestinations;

    [SerializeField] private float characterRadiusMultiplier = 1.5f;
    [SerializeField] private float minWanderDistance = 5f;
    [SerializeField] private float maxWanderDistance = 15f;
    [SerializeField] private float detectDistance = 10f;
    [SerializeField] private float chaseDistance = 15f;
    [SerializeField] private float attackDistance = 5f;

    private Vector2 viewDirection = new Vector2();
    private Vector2 aimDirection = new Vector2();
    private Vector3 curDestination;
    private GameObject target;
    private Collider collider;

    private float characterRadius;

    private eStateType state;
    private eAIStateType aiState;

    public List<Vector3> WanderDestinations { get { return wanderDestinations; } private set { wanderDestinations = value; } }
    public NavMeshAgent Agent { get { return agent; } private set { agent = value; } }
    public float MinWanderDistance { get { return minWanderDistance; } private set { minWanderDistance = value; } }
    public float MaxWanderDistance { get { return maxWanderDistance; } private set { maxWanderDistance = value; } }
    public float DetectDistance { get { return detectDistance; } private set { detectDistance = value; } }
    public float ChaseDistance { get { return chaseDistance; } private set { chaseDistance = value; } }
    public float AttackDistance { get { return attackDistance; } private set { attackDistance = value; } }

    public Vector2 ViewDirection { get { return viewDirection; } private set { viewDirection = value; } }
    public Vector2 AimDirection { get { return aimDirection; } private set { aimDirection = value; } }
    public Vector3 CurDestination { get { return curDestination; } private set { curDestination = value; } }

    public GameObject Target { get { return target; } private set { target = value; } }

    public eStateType State { get { return state; } private set { state = value; } }
    public eAIStateType AIState { get { return aiState; } private set { aiState = value; } }

    private void Awake()
    {
        characterRadius = agent.radius * characterRadiusMultiplier;
        State = eStateType.Idle;
        AIState = eAIStateType.Wait;
        Agent.updateRotation = false;
        collider = GetComponent<Collider>();
    }

    //private void Start()
    //{
    //    int index = Random.Range(0, WanderDestinations.Count);
    //    SetNextDestination(WanderDestinations[index]);
    //}

    //private void Update()
    //{
    //    if (IsArrived())
    //    {
    //        //목표 지점에 도달했다면 대기 상태로 진입하는 내용

    //        int index = Random.Range(0, WanderDestinations.Count);
    //        SetNextDestination(WanderDestinations[index]);
    //    }
    //}

    public void UpdateAIState()
    {
        Debug.Log($"AIState : {AIState}");
        switch (AIState)
        {
            case eAIStateType.Wait: { WaitState(); break; }
            case eAIStateType.Wander: { WanderState(); break; }
            case eAIStateType.Chase: { ChaseState(); break; }
            case eAIStateType.Attack: { AttackState(); break; }
            case eAIStateType.Search: { SearchState(); break; }
            case eAIStateType.Runaway: { RunawayState(); break; }
            default: { break; }
        }
    }

    /// <summary>
    /// 대기 상태: 배회 상태에서 현재 목표 지점까지 도달했을 때 랜덤한 시간 동안 아무 것도 하지 않고 제자리에 머무른다. 
    /// 별다른 트리거 없이 지정된 시간이 경과하면 배회 상태로 진입한다.(base 의 통상 상태를 유지한다)
    /// </summary>
    private void WaitState()
    {
        CurDestination = this.transform.position;
        Invoke("SetWanderState", 2000f);
    }

    /// <summary>
    /// 배회 상태: 별다른 트리거가 없다면, 지정된 목표 지점들 중 하나를 선택하여 해당 위치까지 이동한다. 
    /// 목표 지점에 도달했다면 대기 상태로 진입한다.(base 의 걷기 상태를 유지한다)
    /// </summary>
    private void WanderState()
    {
        //목표 지점에 도달했다면 대기 상태로 전환
        if (IsArrived())
        {
            AIState = eAIStateType.Wait;
        }
    }

    /// <summary>
    /// 추적 상태: 적대 대상을 확실하게 감지했을 때(일단은 일정 범위 내에 들어왔을 때 한정) 이 상태가 된다. 적대 대상의 위치를 목표 지점으로 지정하고,
    /// 지속적으로 목표 지점을 갱신하며 접근한다. 적대 대상이 공격 범위 내에 들어왔다면 공격 상태로 전환한다.
    /// (적대 대상에게 다가갈 때는 base 의 달리기 상태를 유지한다.)
    /// </summary>
    private void ChaseState()
    {
        SetNextDestination(target.transform.position);

        //추적 대상이 추적 범위를 벗어났다면 배회 상태로 전환
        if (Vector3.Distance(transform.position, curDestination) > chaseDistance)
        {
            LostTarget();
            return;
        }

        //추적 대상이 공격 범위 안쪽으로 들어왔다면 공격 상태로 전환
        if (Vector3.Distance(transform.position, curDestination) > attackDistance)
        {
            Debug.Log("Trying Attack!!!");
            AIState = eAIStateType.Attack;
            return;
        }
    }

    /// <summary>
    /// 공격 상태: 적대 대상에게 공격을 시도한다.
    /// (적대 대상 방향으로 base 의 조준 - 공격 상태를 유지한다. 대상이 공격 범위를 벗어나면 추적 상태로 전환한다)
    /// </summary>
    private void AttackState()
    {
        SetNextDestination(target.transform.position);

        //추적 대상이 공격 범위를 벗어났다면 추적 상태로 전환
        if (Vector3.Distance(transform.position, curDestination) < attackDistance)
        {
            AIState = eAIStateType.Chase;
            return;
        }
    }

    /// <summary>
    /// 수색 상태. 추후 검토할 것
    /// </summary>
    private void SearchState()
    {
    }

    /// <summary>
    /// 도망 상태. 추후 검토할 것
    /// </summary>
    private void RunawayState()
    {
    }

    /// <summary>
    /// 다음 목적지 좌표를 지정한다.
    /// </summary>
    private void SetNextDestination(Vector3 next)
    {
        CurDestination = next;
        Debug.Log($"목적지 설정 : {CurDestination}");
        agent.destination = curDestination;
    }

    private void SetWanderState()
    {
        int index = Random.Range(0, WanderDestinations.Count);
        SetNextDestination(WanderDestinations[index]);
    }

    /// <summary>
    /// Collider 를 추적 범위로 이용한다. 범위 내에 들어온 대상이 플레이어라면 추적 상태로 전환한다.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        //추적 대상이 없을 때 대상이 플레이어라면
        if (target == null && other.gameObject.CompareTag("Player"))
        {
            DetectTarget(other.gameObject);
        }
    }

    ///// <summary>
    ///// 현재 추적 대상이 Collider 범위에서 나갔다면 배회 상태로 전환한다.
    ///// </summary>
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player") && other.gameObject == target)
    //    {
    //        LostTarget();
    //        Debug.Log("Target Losted!!!");
    //    }
    //}

    /// <summary>
    /// 추적 대상을 설정하고 추적 상태가 되도록 한다.
    /// </summary>
    public void DetectTarget(GameObject enemy)
    {
        Debug.Log("Target detected!!!");
        Target = enemy;
        dataContainer.Stats.MoveSpeed *= dataContainer.Stats.RunMultiplier;
        aiState = eAIStateType.Chase;
    }

    /// <summary>
    /// 추적 대상을 해제하고 배회 상태가 되도록 한다.
    /// </summary>
    public void LostTarget()
    {
        Debug.Log("Target Lost!!!");
        Target = null;
        dataContainer.Stats.MoveSpeed /= dataContainer.Stats.RunMultiplier;
        aiState = eAIStateType.Wander;
    }

    /// <summary>
    /// 현재 목적지에 매우 근접했다면 true, 아니면 false
    /// </summary>
    private bool IsArrived()
    {
        if (Vector3.Distance(transform.position, curDestination) < characterRadius)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 추적 대상이 추적 한계치보다 멀리 있다면 true, 아니면 false
    /// </summary>
    /// <returns></returns>
    private bool IsTooFar()
    {
        return false;
    }
}
