using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : BaseItem, IEquipable, IDroppable
{
    [SerializeField] [Tooltip("공격 속도")] private float weaponAS;
    [SerializeField] [Tooltip("공격력")] private int weaponAP;
    [SerializeField] [Tooltip("크리티컬 피해량")] private int weaponCH;
    [SerializeField] [Tooltip("크리티컬 확률")] private int weaponCP;
    [SerializeField] [Tooltip("체력")] private int weaponHP;
    [SerializeField] [Tooltip("방어력")] private int weaponDP;

    public void Equip()
    {

    }

    public void Dequip()
    {

    }

    public void Drop()
    {

    }
}
