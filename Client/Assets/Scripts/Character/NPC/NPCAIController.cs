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
//3.추적 상태: 적대 대상을 확실하게 감지했을 때(시야 또는 청각, 후각 등의 요소) 이 상태가 된다. 적대 대상의 위치를 목표 지점으로 지정하고,
//지속적으로 목표 지점을 갱신하며 접근한다. 적대 대상이 공격 범위 내에 들어왔다면 공격을 시도한다.
//(적대 대상에게 다가갈 때는 base 의 걷기, 달리기 상태를 섞어 가며 유지한다. 보통은 달리기 상태를 좀 더 자주, 더 길게 유지한다)
//(적대 대상과 가까워졌을 때는 base 의 조준 - 공격 상태를 유지한다. 거리가 멀어지면 이를 반복한다)

//[추후 검토]
//4.수색 상태: 공격받았지만 아직 적대 대상을 확실하게 감지하지 못 했을 때(시야 또는 청각, 후각 등의 요소), 
//추적 상태에서 적대 대상과 거리가 멀어졌을 때 이 상태가 된다. 감지 범위가 크게 증가하며,
//현재 위치 내에서 일정 범위를 배회한다. 이 상태에서 일정 시간 별다른 트리거가 없었다면 배회 상태로 진입한다.
//(base 의 걷기 상태를 유지한다)

//[추후 검토]
//5.도망 상태: 체력이 낮을 때, 일정 확률로 이 상태가 된다. 도망 상태는 일정 시간 유지되며, 적대 대상과 멀어지는 방향으로 이동한다. 
//이 상태가 유지되는 동안 대부분의 트리거를 무시한다. 
//(base 의 달리기 상태를 유지한다)

public class NPCAIController : MonoBehaviour
{
    //해당 컨트롤러를 가지는 객체의 데이터 컨테이너
    [SerializeField] private NPCDataContainer dataContainer;

    //길찾기용 컴포넌트
    [SerializeField] private NavMeshAgent agent;

    //목적지 좌표
    [SerializeField] private List<Vector3> destinations;

    [SerializeField] private float characterRadiusMultiplier = 1.5f;
    [SerializeField] private float minWanderDistance = 5f;
    [SerializeField] private float maxWanderDistance = 15f;
    [SerializeField] private float detectDistance = 10f;

    private Animator animator;
    private Vector2 viewDirection = new Vector2();
    private Vector2 aimDirection = new Vector2();
    private Vector3 curDestination;

    private float characterRadius;

    private eStateType state;
    private eAIStateType aiState;

    public List<Vector3> Destinations { get { return destinations; } private set { destinations = value; } }
    public NavMeshAgent Agent { get { return agent; } private set { agent = value; } }
    public float MinWanderDistance { get { return minWanderDistance; } private set { minWanderDistance = value; } }
    public float MaxWanderDistance { get { return maxWanderDistance; } private set { maxWanderDistance = value; } }
    public float DetectDistance { get { return detectDistance; } private set { detectDistance = value; } }

    public Vector2 ViewDirection { get { return viewDirection; } private set { viewDirection = value; } }
    public Vector2 AimDirection { get { return aimDirection; } private set { aimDirection = value; } }
    public Vector3 CurDestination { get { return curDestination; } private set { curDestination = value; } }

    public eStateType State { get { return state; } private set { state = value; } }
    public eAIStateType AIState { get { return aiState; } private set { aiState = value; } }

    private void Awake()
    {
        characterRadius = agent.radius * characterRadiusMultiplier;
        animator = dataContainer.Animator;
        State = eStateType.Idle;
        AIState = eAIStateType.Wait;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetNextDestination();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {


        //현재 목적지에 매우 근접했다면 새 목적지를 설정한다.
        if (Vector3.Distance(transform.position, curDestination) < characterRadius)
        {
            if(destinations.Count <= 0)
            {
                SetWanderLocation();
                return;
            }

            SetNextDestination();
        }
    }

    private void UpdateAIState()
    {
        switch (AIState)
        {
            case eAIStateType.Wait: { break; }
            case eAIStateType.Wander: { break; }
            case eAIStateType.Chase: { break; }
            case eAIStateType.Search: { break; }
            case eAIStateType.Runaway: { break; }
            default: { break; }
        }
    }

    //배회 상태일 때 최소, 최대 배회 범위를 기반으로 랜덤한 다음 목적지 좌표를 지정한다.
    private void SetWanderLocation()
    {
        NavMeshHit hit;

        //지정된 범위 내에서 생성한 다음 목적지를 hit 에 지정.
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        CurDestination = hit.position;
        Debug.Log($"목적지 설정 : {CurDestination}");
        agent.destination = curDestination;
    }

    //감지한 적대 대상의 위치를 목적지 좌표를 지정한다.
    private void SetEnemyLocation(GameObject enemy)
    {
        CurDestination = enemy.transform.position;
        Debug.Log($"목적지 설정 : {curDestination}");
        agent.destination = curDestination;
    }

    //목적지 리스트에서 랜덤한 다음 목적지 좌표를 지정한다.
    private void SetNextDestination()
    {
        int index = Random.Range(0, destinations.Count);
        CurDestination = destinations[index];
        Debug.Log($"목적지 설정 : {CurDestination}");
        agent.destination = curDestination;
    }
}
