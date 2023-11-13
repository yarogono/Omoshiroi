using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: CustomSingleton<UIController>
{
    public Slider HpBar;

    LeaveGame leaveGame;
    public  Button BtnLeavGame;

  

    private void Start()
    {
        init();

        leaveGame = GetComponent<LeaveGame>();

        BtnLeavGame.onClick.AddListener(() =>
        {
            if (leaveGame)
            {
                leaveGame.LeaveGameRoom();
            }
            else
            {
                Debug.LogError("Component null");
            }
        });
       
    }

    void init()
    {
        Instantiate(HpBar);

    }


    public  void HandlerHp(float MaxHp ,float CurHp)
    {
        HpBar.value = Mathf.Lerp(HpBar.value, (float)CurHp / (float)MaxHp, Time.deltaTime * 10);
        Debug.Log(CurHp);
    }

    public void InitHpbar(float MaxHp, float CurHp)
    {

        HpBar.value = CurHp / MaxHp;
    }
   
}
