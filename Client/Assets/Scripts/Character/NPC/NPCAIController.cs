using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCAIController : MonoBehaviour
{
    //목적지 좌표
    [SerializeField] public List<Vector3> targets;
    
    //길찾기용 컴포넌트
    [SerializeField] public NavMeshAgent agent;

    [SerializeField] public float minWanderDistance = 5f; 
    [SerializeField] public float maxWanderDistance = 15f;
    [SerializeField] public float detectDistance = 10f;

    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, targets.Count);
        destination = targets[index];
        //목적지 설정.
        agent.destination = destination;
    }

    // Update is called once per frame
    void Update()
    {
        //현재 위치에서 hit 까지의 거리가 매우 근접했다면 새 목적지를 설정한다.
        if (Vector3.Distance(transform.position, agent.destination) < 1.5f)
        {
            destination = GetWanderLocation();
            agent.destination = destination;
        }
    }

    //배회 상태일 때 최소, 최대 배회 범위를 기반으로 랜덤한 다음 목적지 좌표를 반환한다.
    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        //지정된 범위 내에서 생성한 다음 목적지를 hit 에 지정.
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        Debug.Log($"목적지 설정 : ({hit.position})");
        return hit.position;
    }
}
