using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Slider HpBar;

    private float MaxHp;
    private float CurHp;


    private void Start()
    {
        HpBar.value = CurHp / MaxHp;
    }

    private void Update()
    {
        HandleHp();
    }

    private void HandleHp()
    {
        HpBar.value = CurHp / MaxHp;
    }
}
