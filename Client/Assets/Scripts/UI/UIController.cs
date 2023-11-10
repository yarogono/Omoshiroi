using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Slider HpBar;

    [SerializeField] private float MaxHp;
    [SerializeField] private float CurHp;

    private void Start()
    {
        HpBar.value = CurHp / MaxHp;

    }

    private void Update()
    {
        
    }

    private void HandlerHp()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, CurHp / MaxHp, Time.deltaTime * 10);
    }
}
