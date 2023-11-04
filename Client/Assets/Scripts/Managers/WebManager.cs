using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : CustomSingleton<WebManager>
{
    private string _restApiUrl;

    private void Start()
    {
        ConfigManager.LoadConfig();
        _restApiUrl = ConfigManager.Config.restApiUrl;


        AccountLoginReq req = new AccountLoginReq() { AccountName = "1234", AccountPassword = "1234" };
        SendPostRequest<AccountLoginRes>("account/login", req, res =>
        {
            if (res.LoginOk == 0)
            {
                Debug.Log("login failed");
            }
            else if (res.LoginOk == 1)
            {
                Debug.Log("login succed");
            }
        });

    }

    public void SendPostRequest<T>(string url, object obj, Action<T> res)
    {
        StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbPOST, obj, res));
    }

    public void SendGetRequest<T>(string url, object obj, Action<T> res)
    {
        StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbGET, obj, res));
    }

    public void SendDeleteRequest<T>(string url, object obj, Action<T> res)
    {
        StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbDELETE, obj, res));
    }

    public void SendUpdateRequest<T>(string url, object obj, Action<T> res)
    {
        StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbPUT, obj, res));
    }

    IEnumerator CoSendWebRequest<T>(string url, string method, object obj, Action<T> res)
    {
        string sendUrl = $"{_restApiUrl}/{url}";

        byte[] jsonBytes = null;
        if (obj != null)
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        using (var uwr = new UnityWebRequest(sendUrl, method))
        {
            uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                T resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(uwr.downloadHandler.text);
                res.Invoke(resObj);
            }
        }
    }
}
