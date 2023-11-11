using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [field: SerializeField]
    DataContainer dataContainer;
    public CharacterStats stats;

    private void Awake()
    {
        stats = dataContainer.Stats;
    }

    public void TakeDamage(int changeAmount)
    {
        int remain = stats.Hp - changeAmount;

        if (remain <= 0)
        {
            //플레이어 사망 또는 무언가를 처리하는 부분
        }
        else if (stats.MaxHp < remain)
        {
            //최대 체력을 초과한 치유 시 처리 부분
        }
        else { }

        stats.Hp = remain;
    }

    public void TakeRecovery(int changeAmount)
    {
        int remain = stats.Hp + changeAmount;

        if (remain <= 0)
        {
            //플레이어 사망 또는 무언가를 처리하는 부분
        }
        else if (stats.MaxHp < remain)
        {
            //최대 체력을 초과한 치유 시 처리 부분
        }
        else { }

        stats.Hp = remain;
    }
}
