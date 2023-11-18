using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : CustomSingleton<WebManager>
{
    private string _restApiUrl;

    // 각 요청 메서드에 대한 독립적인 변수 추가
    private bool IsSendingPostRequest = false;
    private bool IsSendingGetRequest = false;
    private bool IsSendingDeleteRequest = false;
    private bool IsSendingUpdateRequest = false;

    private void Awake()
    {
        ConfigManager.LoadConfig();
        _restApiUrl = ConfigManager.Config.restApiUrl;
    }

    public void SendPostRequest<T>(string url, object obj, Action<T> res)
    {
        if (!IsSendingPostRequest)
        {
            IsSendingPostRequest = true;
            StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbPOST, obj, res, () => IsSendingPostRequest = false));
        }
    }

    public void SendGetRequest<T>(string url, object obj, Action<T> res)
    {
        if (!IsSendingGetRequest)
        {
            IsSendingGetRequest = true;
            StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbGET, obj, res, () => IsSendingGetRequest = false));
        }
    }

    public void SendDeleteRequest<T>(string url, object obj, Action<T> res)
    {
        if (!IsSendingDeleteRequest)
        {
            IsSendingDeleteRequest = true;
            StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbDELETE, obj, res, () => IsSendingDeleteRequest = false));
        }
    }

    public void SendUpdateRequest<T>(string url, object obj, Action<T> res)
    {
        if (!IsSendingUpdateRequest)
        {
            IsSendingUpdateRequest = true;
            StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbPUT, obj, res, () => IsSendingUpdateRequest = false));
        }
    }

    IEnumerator CoSendWebRequest<T>(string url, string method, object obj, Action<T> res, Action onComplete)
    {
        string sendUrl = $"{_restApiUrl}/{url}";

        byte[] jsonBytes = null;
        if (obj != null)
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        using var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"Request Error : {uwr.error}");
        }
        else
        {
            string responseText = uwr.downloadHandler.text;
            Debug.Log($"Server Response: {responseText}");

            if (string.IsNullOrEmpty(responseText))
            {
                throw new Exception("Empty server response.");
            }

            try
            {
                T resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText);

                if (resObj != null)
                {
                    Debug.Log($"PlayerStatRes: {resObj.ToString()}");
                    res.Invoke(resObj);
                }
                else
                {
                    Debug.LogError("resObj 객체가 null입니다.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception during deserialization: {e.Message}");
            }
        }

        // 요청이 완료되면 onComplete 실행
        onComplete?.Invoke();
    }
}
