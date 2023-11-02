using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeaponSO", menuName = "Item/BaseWeaponSO")]
public class BaseWeapon : BaseItem, IEquippable, IDroppable
{
    [Header("WeaponData")]

    [SerializeField] [Tooltip("체력")] protected int weaponHP;
    [SerializeField] [Tooltip("방어력")] protected int weaponDEF;
    [SerializeField] [Tooltip("공격 속도")] protected float weaponAS;
    [SerializeField] [Tooltip("공격력")] protected int weaponAP;
    [SerializeField] [Tooltip("크리티컬 확률")] protected int weaponCR;
    [SerializeField] [Tooltip("크리티컬 피해 증가량")] protected float weaponCP;

    public void Equip(CharacterDataContainer cdc)
    {
        cdc.Stats.maxHp += weaponHP; cdc.Stats.hp += weaponHP;
        cdc.Stats.def += weaponDEF; cdc.Stats.atkSpeed += weaponAS;
        cdc.Stats.atkPower += weaponAP; cdc.Stats.critRate += weaponCR;
        cdc.Stats.critPower += weaponCP;

    }

    public void Dequip(CharacterDataContainer cdc)
    {
        cdc.Stats.maxHp -= weaponHP; cdc.Stats.hp -= weaponHP;
        cdc.Stats.def -= weaponDEF; cdc.Stats.atkSpeed -= weaponAS;
        cdc.Stats.atkPower -= weaponAP; cdc.Stats.critRate -= weaponCR;
        cdc.Stats.critPower -= weaponCP;
    }

    public void Drop()
    {

    }

    public float WeaponAS { get { return weaponAS; } }
    public int WeaponAP { get { return weaponAP; } }
    public int WeaponCR { get { return weaponCR; } }
    public float WeaponCP { get { return weaponCP; } }
    public int WeaponHP { get { return weaponHP; } }
    public int WeaponDEF { get { return weaponDEF; } }
}
