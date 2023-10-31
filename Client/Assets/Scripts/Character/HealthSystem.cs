using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private CharacterStats stats;

    HealthSystem(CharacterStats characterStats)
    {
        stats = characterStats;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 체력 회복 요소를 추가한다면 그냥 - 데미지를 넣어서 구현하면 될 듯 함.
    /// </summary>
    public void TakeDamege(int damege)
    {
        stats.BaseHP -= damege;
        
        if(stats.BaseHP <= 0)
        {
            //플레이어 사망 또는 무언가를 처리하는 부분
        }
    }
}
