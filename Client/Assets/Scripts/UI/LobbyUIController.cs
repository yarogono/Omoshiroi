using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{

    private void Awake()
    {

    }
    void Start()
    {

    }

    public void OpenOption()
    {
        var ui = UIManager.Instance.ShowUI<UIOption>();
    }

}
