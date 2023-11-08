using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] private CharacterStats stats;

    PlayerHPReq req = new PlayerHPReq();
    PlayerHPRes res = new PlayerHPRes();

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
    /// 플레이어 체력 정보를 보낼 url 경로와 PlayerHPRes 타입에 담겨야 할 멤버변수를 정해야 한다.
    /// </summary>
    private void SendHPReq()
    {
        WebManager.Instance.SendPostRequest<PlayerHPRes>("url 경로", req, SetHPRes);    
    }

    private void SetHPRes(PlayerHPRes newRes){
        res = newRes;
    }
}

public class PlayerHPReq
{
    public int MaxHp { get; set; }
    public int CurrentHP { get; set; }
    public int HPChangeAmount { get; set; }
}

public class PlayerHPRes
{
    
}
