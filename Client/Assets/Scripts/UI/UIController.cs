using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public Slider HpBar;

    LeaveGame leaveGame;
    public  Button BtnLeavGame;
    public GameObject GameOver;

    public RectTransform UIRoot;
    public Button BtnInventory;
    public Button BtnCancel;

    public GameObject InventoryUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        init();

        leaveGame = GetComponent<LeaveGame>();

        BtnLeavGame.onClick.AddListener(() =>
        {           
            LoadingScenController.LoadScene("LobbyScene"); 
        });
       
    }
    void init()
    {
        if (GameOver) // GameOver가 할당되었는지 확인
        {
            GameOver.SetActive(false);
        }
        else
        {
            Debug.LogError("NullGameOver");
        }
    }
    public void UIDead()
    {
        GameOver.SetActive(true);
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
    private void OnDestroy()
    {
        Instance = null;
    }
}
