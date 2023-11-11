using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [field: SerializeField]
    DataContainer dataContainer;
    public CharacterStats stats;

    PilotSync Sync;

    private void Awake()
    {
        stats = dataContainer.Stats;
        Sync = GetComponent<PilotSync>();
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
        Sync.SendC_ChangeHpPacket(stats.Hp);
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
        Sync.SendC_ChangeHpPacket(stats.Hp);
    }
}
