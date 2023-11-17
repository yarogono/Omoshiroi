using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public GameObject lobbyLoading; // 카멜 케이스 적용
    public GameObject uiMainLobby;
    public GameObject uiStore;
    public GameObject uiDdal;
    public GameObject uiStorage;
    public Button btnStore;

    void Start()
    {
        btnStore.onClick.AddListener(() =>
        {
            StartUILoading(uiStore, uiMainLobby); // 메소드 이름 변경 및 오타 수정
        });
    }

    public void OpenOption()
    {
        var ui = UIManager.Instance.ShowUI<UIOption>();
    }

    public void StartUILoading(GameObject openUI, GameObject closeUI) // 메소드 이름 변경 및 오타 수정
    {
        if (lobbyLoading != null)
        {
            lobbyLoading.SetActive(true);
            StartCoroutine(OpenUI(openUI, closeUI));
        }
    }

    public IEnumerator OpenUI(GameObject openUI, GameObject closeUI)
    {
        yield return new WaitForSeconds(1.5f);
        if (openUI != null) openUI.SetActive(true);
        if (closeUI != null) closeUI.SetActive(false);
        yield return new WaitForSeconds(1);
        if (lobbyLoading != null) lobbyLoading.SetActive(false);
    }
}