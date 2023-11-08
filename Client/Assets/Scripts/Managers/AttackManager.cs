using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackManager : CustomSingleton<RangeAttackManager>
{   
     
    // 오브젝트 풀을 위한 큐
    private Queue<GameObject> attackPool = new Queue<GameObject>();

    // 원거리 공격 프리팹
    [SerializeField] 
    private GameObject attackPrefab;
    // 풀 크기 
    [SerializeField] 
    private int poolSize = 10;
    //풀 
    [SerializeField]
    private GameObject ObjPool;

    public void RqAttack (CharacterDataContainer dataContainer,Vector3 position , Vector2 direction ) 
    {
        // 오브젝트풀링 
        // 풀링한오브젝트를 초기화  : 
        // 
    }

    private void Awake()
    {
        InitializePool(); // 오브젝트 풀 초기화
    }

    private void InitializePool()
    {
        // 풀 크기만큼 오브젝트를 생성하여 비활성화 상태로 큐에 추가
        for (int i = 0; i < poolSize; i++)
        {
            var newAttack = Instantiate(attackPrefab);
            newAttack.SetActive(false);
            attackPool.Enqueue(newAttack);
        }
    }

    public GameObject GetAttackFromPool()
    {
        // 풀에 오브젝트가 있으면 하나를 꺼내서 활성화하고 반환
        if (attackPool.Count > 0)
        {
            var attack = attackPool.Dequeue();
            attack.SetActive(true);
            return attack;
        }
        else
        {
            // 풀이 비어있으면 새로운 오브젝트를 생성하여 반환
            var newAttack = Instantiate(attackPrefab,ObjPool.transform);
            return newAttack;
        }
    }

    public void ReturnAttackToPool(GameObject attack)
    {
        // 오브젝트를 비활성화하고 다시 풀에 반환
        attack.SetActive(false);
        attackPool.Enqueue(attack);
    }

}
