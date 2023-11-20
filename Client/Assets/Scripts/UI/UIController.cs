using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: CustomSingleton<UIController>
{
    public Slider HpBar;

    LeaveGame leaveGame;
    public  Button BtnLeavGame;
    public RectTransform UIRoot;
    public Button BtnInventory;
    public Button BtnCancel;

    public GameObject InventoryUI;



    private void Start()
    {
        

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


    public  void HandlerHp(float MaxHp ,float CurHp)
    {
        Debug.Log(MaxHp);
        HpBar.value = Mathf.Lerp(HpBar.value, (float)CurHp / (float)MaxHp, Time.deltaTime * 10);
        Debug.Log(CurHp);
    }

    public void InitHpbar(float MaxHp, float CurHp)
    {

        HpBar.value = CurHp / MaxHp;
    }
   
}
