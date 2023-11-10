using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    [SerializeField] private Button BtnReadyToBattle;
    [SerializeField] private Button BtnEnterBattle;
    [SerializeField] private GameObject UIReadyToBattle;


    private void Awake()
    {
        init();
    }
    void Start()
    {
        BtnReadyToBattle.onClick.AddListener(() =>
        {
            UIReadyToBattle.SetActive(true);
        });
        BtnEnterBattle.onClick.AddListener(() =>
        {
            // 전투 진입 관련 코드 
        });

    }
    void init()
    {
        UIReadyToBattle.SetActive(false);    
    }

}
