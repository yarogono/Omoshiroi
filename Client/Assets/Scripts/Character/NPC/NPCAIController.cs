using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    public List<Vector3> Destinations { get { return destinations; } private set { destinations = value; } }
    public NavMeshAgent Agent { get { return agent; } private set { agent = value; } }
    public float MinWanderDistance { get { return minWanderDistance; } private set { minWanderDistance = value; } }
    public float MaxWanderDistance { get { return maxWanderDistance; } private set { maxWanderDistance = value; } }
    public float DetectDistance { get { return detectDistance; } private set { detectDistance = value; } }

    public Vector2 ViewDirection { get { return viewDirection; } private set { viewDirection = value; } }
    public Vector2 AimDirection { get { return aimDirection; } private set { aimDirection = value; } }
    public Vector3 CurDestination { get { return curDestination; } private set { curDestination = value; } }

    public eStateType State { get { return state; } private set { state = value; } }

    private void Awake()
    {
        characterRadius = agent.radius * characterRadiusMultiplier;
        animator = dataContainer.Animator;
        State = eStateType.Idle;
    }

    // Start is called before the first frame update
    private void Start()
    {
        GetNextDestination();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {


        //현재 목적지에 매우 근접했다면 새 목적지를 설정한다.
        if (Vector3.Distance(transform.position, curDestination) < characterRadius)
        {
            if(destinations.Count <= 0)
            {
                GetWanderLocation();
                return;
            }

            GetNextDestination();
        }
    }

    //배회 상태일 때 최소, 최대 배회 범위를 기반으로 랜덤한 다음 목적지 좌표를 반환한다.
    private void GetWanderLocation()
    {
        NavMeshHit hit;

        //지정된 범위 내에서 생성한 다음 목적지를 hit 에 지정.
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        CurDestination = hit.position;
        Debug.Log($"목적지 설정 : {hit.position}");
        agent.destination = curDestination;
    }

    //목적지 리스트에서 랜덤한 다음 목적지 좌표를 반환한다.
    private void GetNextDestination()
    {
        int index = Random.Range(0, destinations.Count);
        CurDestination = destinations[index];
        Debug.Log($"목적지 설정 : {CurDestination}");
        agent.destination = curDestination;
    }
}
