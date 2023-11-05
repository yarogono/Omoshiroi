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
           LoadingScenController.LoadScene(sceneName);
        });
    }
  
}
