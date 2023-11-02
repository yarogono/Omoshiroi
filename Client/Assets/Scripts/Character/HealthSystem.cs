using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private CharacterStats stats;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 초과 치유 또는 사망 조건에 대한 예외처리 포함. 치유의 경우 -데미지를 입력값으로 받도록 처리하면 됨. 
    /// </summary>
    /// <param name="damege"></param>
    public void TakeDamege(int damege)
    {
        
        if(stats.Hp - damege <= 0)
        {
            stats.SetHP(0);
            //플레이어 사망 또는 무언가를 처리하는 부분
        }
        else if (stats.MaxHp < stats.Hp)
        {
            //최대 체력을 초과한 치유 시 처리 부분
        }

    }
}
