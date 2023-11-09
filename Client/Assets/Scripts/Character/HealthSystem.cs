using Google.Protobuf.Protocol;
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
    /// 피격이든, 회복이든 내용은 같으나 피격 상황과 회복 상황을 구분하기 위해 별도의 메소드로 두었다.
    /// 단순히 체력 증감량을 현재 체력에 더해주고, 서버 측과 이에 대한 데이터를 통신한다. 
    /// 초과 치유 또는 사망 조건에 대한 예외처리를 고려한 형태이나, 이에 대한 처리는 아직 구현되지 않았다.
    /// </summary>
    public void TakeDamage(int changeAmount)
    {
        int remain = stats.Hp + changeAmount;

        if(remain <= 0)
        {
            //플레이어 사망 또는 무언가를 처리하는 부분
        }
        else if (stats.MaxHp < remain)
        {
            //최대 체력을 초과한 치유 시 처리 부분
        }
        else
        {
        }

        //서버에 자신의 ID 와 입은 피해량 정보를 전달하는 내용
        SendHPDamage(stats.MaxHp, stats.Hp, changeAmount);

        stats.SetHP(remain);
    }
    
    /// <summary>
    /// 피격이든, 회복이든 내용은 같으나 피격 상황과 회복 상황을 구분하기 위해 별도의 메소드로 두었다.
    /// 단순히 체력 증감량을 현재 체력에 더해주고, 서버 측과 이에 대한 데이터를 통신한다. 
    /// 초과 치유 또는 사망 조건에 대한 예외처리를 고려한 형태이나, 이에 대한 처리는 아직 구현되지 않았다.
    /// </summary>
    public void TakeRecovery(int changeAmount)
    {
        int remain = stats.Hp + changeAmount;

        if(remain <= 0)
        {
            //플레이어 사망 또는 무언가를 처리하는 부분
        }
        else if (stats.MaxHp < remain)
        {
            //최대 체력을 초과한 치유 시 처리 부분
        }
        else
        {
        }

        //서버에 자신의 ID 와 입은 피해량 정보를 전달하는 내용
        SendHPDamage(stats.MaxHp, stats.Hp, changeAmount);

        stats.SetHP(remain);
    }

    /// <summary>
    /// 서버 측에 피격 정보를 전송한다.
    /// </summary>
    private void SendHPDamage(int maxHP, int currentHP, int changeAmount)
    {
        C_HpDamage hpDamage = new C_HpDamage();
        hpDamage.MaxHp = maxHP; hpDamage.CurrentHp = currentHP; hpDamage.ChangeAmount = changeAmount;
        NetworkManager.Instance.Send(hpDamage);
    }

    /// <summary>
    /// 서버 측에 회복 정보를 전송한다.
    /// </summary>
    private void SendHPRecovery(int maxHP, int currentHP, int changeAmount)
    {
        C_HpDamage hpDamage = new C_HpDamage();
        hpDamage.MaxHp = maxHP; hpDamage.CurrentHp = currentHP; hpDamage.ChangeAmount = changeAmount;
        NetworkManager.Instance.Send(hpDamage);
    }
}



