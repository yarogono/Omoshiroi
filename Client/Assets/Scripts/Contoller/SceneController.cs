using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "PlayerTestScene";
    [SerializeField]
    private Button BtnChangeScene;
    private void Start()
    {
        BtnChangeScene.onClick.AddListener(() =>
        {          
            //login테스트
            AccountLoginReq req = new AccountLoginReq() { AccountName = "qwer", AccountPassword = "qwer" };
            AccountLoginRes newRes = null;
            WebManager.Instance.SendPostRequest<AccountLoginRes>("account/login", req, res =>
            {                
                newRes = res;        
            });
            LoadingScenController.LoadScene(sceneName);
        });
    }
  
}
