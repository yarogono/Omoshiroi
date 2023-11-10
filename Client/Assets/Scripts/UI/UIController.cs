using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: MonoBehaviour
{
    public Slider HpBar;

    private float MaxHp;
     private float CurHp;
    LeaveGame leaveGame;
    public  Button BtnLeavGame;
    [SerializeField] private DataContainer dataContainer;


   
    private void Start()
    {
        InitHpbar(dataContainer);

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


    private void Update()
    {
       // HandlerHp();
    }

    public  void HandlerHp()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, (float)CurHp / (float)MaxHp, Time.deltaTime * 10);
    }

    public void InitHpbar(DataContainer dataContainer)
    {
        
        MaxHp = (float)dataContainer.Health.stats.MaxHp;
        CurHp = (float)dataContainer.Health.stats.Hp;
        HpBar.value = CurHp / MaxHp;
    }
}
