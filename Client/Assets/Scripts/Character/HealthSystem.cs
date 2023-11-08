using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private CharacterStats stats;

    PlayerHPReq req = new PlayerHPReq();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 단순히 체력 증감량을 현재 체력에 더해주고, 서버 측과 이에 대한 데이터를 통신한다. 
    /// 초과 치유 또는 사망 조건에 대한 예외처리를 고려한 형태이나, 이에 대한 처리는 아직 구현되지 않았다.
    /// </summary>
    public void ChangeHP(int changeAmount)
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

        req.MaxHp = stats.MaxHp;
        req.CurrentHP = stats.Hp;
        req.HPChangeAmount = changeAmount;

        //서버에 자신의 ID 와 입은 피해량 정보를 전달하는 내용
        SendHPReq();

        stats.SetHP(remain);
    }

    /// <summary>
    /// NetworkManager 의 Send 를 이용해야 함. 
    /// </summary>
    private void SendHPReq()
    { 
    }

}

public class PlayerHPReq
{
    public int MaxHp { get; set; }
    public int CurrentHP { get; set; }
    public int HPChangeAmount { get; set; }
}



