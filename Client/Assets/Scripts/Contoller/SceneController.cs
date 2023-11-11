using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private string sceneName ;
    [SerializeField]
    private Button BtnChangeScene;

    public static List<PlayerItemRes> items = new List<PlayerItemRes>();
    private void Start()
    {
        SoundManager.Instance.Play("BGMStartScene", eSoundType.Bgm);

        BtnChangeScene.onClick.AddListener(() =>
        {
            //login테스트
            AccountLoginReq req = new AccountLoginReq() { AccountPassword = "12345", AccountName = "12345" };
            AccountLoginRes newRes = null;
            WebManager.Instance.SendPostRequest<AccountLoginRes>("account/login", req, res =>
            {

                newRes = res;
                items = newRes.Items;
                Debug.Log(newRes);
            });
            LoadingScenController.LoadScene( sceneName);
        });
    }
  
}
