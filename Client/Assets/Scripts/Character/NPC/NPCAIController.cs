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

    private Vector2 viewDirection = new Vector2();
    private Vector2 aimDirection = new Vector2();
    private Vector3 curDestination;
    private GameObject target;

    private float characterRadius;

    private eStateType state;
    private eAIStateType aiState;

    public List<Vector3> WanderDestinations { get { return wanderDestinations; } private set { wanderDestinations = value; } }
    public NavMeshAgent Agent { get { return agent; } private set { agent = value; } }
    public float MinWanderDistance { get { return minWanderDistance; } private set { minWanderDistance = value; } }
    public float MaxWanderDistance { get { return maxWanderDistance; } private set { maxWanderDistance = value; } }
    public float DetectDistance { get { return detectDistance; } private set { detectDistance = value; } }

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
    }

    public void UpdateAIState()
    {
        switch (AIState)
        {
            case eAIStateType.Wait: { Wait(); break; }
            case eAIStateType.Wander: { Wander(); break; }
            case eAIStateType.Chase: { Chase(); break; }
            case eAIStateType.Search: { Search(); break; }
            case eAIStateType.Runaway: { Runaway(); break; }
            default: { break; }
        }
    }

    /// <summary>
    /// 대기 상태: 배회 상태에서 현재 목표 지점까지 도달했을 때 랜덤한 시간 동안 아무 것도 하지 않고 제자리에 머무른다. 
    /// 별다른 트리거 없이 지정된 시간이 경과하면 배회 상태로 진입한다.(base 의 통상 상태를 유지한다)
    /// </summary>
    private void Wait()
    {
        Debug.Log($"AIState : {AIState}");
    }

    /// <summary>
    /// 배회 상태: 별다른 트리거가 없다면, 지정된 목표 지점들 중 하나를 선택하여 해당 위치까지 이동한다. 
    /// 목표 지점에 도달했다면 대기 상태로 진입한다.(base 의 걷기 상태를 유지한다)
    /// </summary>
    private void Wander()
    {
        //탐지 범위 내에 적이 있다면 추적 상태로 변경 후 return 하는 내용

        if (IsArrived())
        {
            //목표 지점에 도달했다면 대기 상태로 진입하는 내용

            int index = Random.Range(0, WanderDestinations.Count);
            SetNextDestination(WanderDestinations[index]);
        }

        Debug.Log($"AIState : {AIState}");
    }

    /// <summary>
    /// 추적 상태: 적대 대상을 확실하게 감지했을 때(일단은 일정 범위 내에 들어왔을 때 한정) 이 상태가 된다. 적대 대상의 위치를 목표 지점으로 지정하고,
    /// 지속적으로 목표 지점을 갱신하며 접근한다. 적대 대상이 공격 범위 내에 들어왔다면 공격을 시도한다.
    /// (적대 대상에게 다가갈 때는 base 의 달리기 상태를 유지한다.)
    /// (적대 대상과 가까워졌을 때는 base 의 조준 - 공격 상태를 유지한다. 거리가 멀어지면 이를 반복한다)
    /// </summary>
    private void Chase()
    {
        //추적 대상과 어느 정도 멀어졌다면 방황 상태로 변경 후 return 하는 내용

        SetNextDestination(target.transform.position);

        if (IsArrived())
        {
            int index = Random.Range(0, WanderDestinations.Count);
            SetNextDestination(WanderDestinations[index]);
        }
        Debug.Log($"AIState : {AIState}");
    }

    /// <summary>
    /// 수색 상태. 추후 검토할 것
    /// </summary>
    private void Search()
    {
        Debug.Log($"AIState : {AIState}");
    }

    /// <summary>
    /// 도망 상태. 추후 검토할 것
    /// </summary>
    private void Runaway()
    {
        Debug.Log($"AIState : {AIState}");
    }

    /// <summary>
    /// 목적지 리스트에서 랜덤한 다음 목적지 좌표를 지정한다.
    /// </summary>
    private void SetNextDestination(Vector3 next)
    {
        CurDestination = next;
        Debug.Log($"목적지 설정 : {CurDestination}");
        agent.destination = curDestination;
    }

    /// <summary>
    /// 적 감지 시 호출. 대상을 설정하고 추적 상태가 되도록 한다.
    /// </summary>
    private void DetectEnemy(GameObject enemy)
    {
        Target = enemy;
        SetNextDestination(enemy.transform.position);
        aiState = eAIStateType.Chase;
    }

    /// <summary>
    /// 현재 목적지에 매우 근접했다면 true, 아니면 false;
    /// </summary>
    private bool IsArrived()
    {
        if (Vector3.Distance(transform.position, curDestination) < characterRadius)
        {
            return true;
        }

        return false;
    }
}
