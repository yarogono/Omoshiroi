using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginSceneController : MonoBehaviour
{
    // 추후에 게스트 로그인 기능 구현 후 삭제 예정
    [SerializeField] private Button buttonBae;
    [SerializeField] private Button buttonIm;
    [SerializeField] private Button buttonSong;
    [SerializeField] private Button buttonJo;
    [SerializeField] private Button buttonChae;

    [SerializeField] private Button BtnChangeScene;

    private void Start()
    {
        SetTempGuestLogin();
    }

    private void SetTempGuestLogin()
    {
        buttonBae.onClick.AddListener(() => { RequestPlayerInfo("bae_123", 2); });
        buttonIm.onClick.AddListener(() => { RequestPlayerInfo("im_123", 0); });
        buttonSong.onClick.AddListener(() => { RequestPlayerInfo("song_123", 0); });
        buttonJo.onClick.AddListener(() => { RequestPlayerInfo("jo_123", 0); });
        buttonChae.onClick.AddListener(() => { RequestPlayerInfo("chae_123", 0); });
    }

    private void RequestPlayerInfo(string playerName, int playerId)
    {
        PlayerLoginReq req = new() { PlayerName = playerName };
        PlayerLoginRes newRes = null;

        WebManager.Instance.SendPostRequest<PlayerLoginRes>("player/login", req, res =>
            {
                newRes = res;

                DataManager.Instance.PlayerId = newRes.PlayerId;

                if (newRes.IsLoginSucceed == true)
                {
                    Debug.Log($"{req} Request Success");

                    SetIntialPlayerStats(playerId);
                }
                else
                {
                    Debug.Log($"{req} Request Failed");
                }
            });
    }

    private void SetIntialPlayerStats(int playerId)
    {
        // DataManager.Instance.Stats

        GetPlayerStatReq req = new() { PlayerId = playerId };
        PlayerStatRes newRes = null;

        WebManager.Instance.SendGetRequest<PlayerStatRes>("player/stat", req, res =>
        {
            newRes = res;
            Debug.Log($"{res.Atk}");

            // if (newRes == null)
            // {
            //     Debug.Log($"{req} Request Failed");
            // }
            // else
            // {
            //     Debug.Log($"{req} Request Success");
            // }
        });
    }
}