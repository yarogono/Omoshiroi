using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가


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
           LoadingScenController.LoadScene(sceneName);
        });
    }
  
}
