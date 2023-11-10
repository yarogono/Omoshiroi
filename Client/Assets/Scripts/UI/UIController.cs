using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: MonoBehaviour
{
    public Slider HpBar;

    [SerializeField] private float MaxHp;
    [SerializeField] private float CurHp;
    LeaveGame leaveGame;
    public  Button BtnLeavGame;

    private void Start()
    {
        HpBar.value = CurHp / MaxHp;
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
         
        }

    private void HandlerHp()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, CurHp / MaxHp, Time.deltaTime * 10);
    }
}
