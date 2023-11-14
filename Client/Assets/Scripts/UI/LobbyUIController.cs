using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    [SerializeField] private Button BtnReadyToBattle;
    [SerializeField] private Button BtnEnterBattle;
    [SerializeField] private GameObject UIReadyToBattle;
    [SerializeField] private string sceneName;

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
            SoundManager.Instance.Clear();
            LoadingScenController.LoadScene(sceneName);
        });

    }
    void init()
    {
        UIReadyToBattle.SetActive(false);    
    }

    public void OpenOption()
    {
        var ui = UIManager.Instance.ShowUI<UIOption>();
    }

}
