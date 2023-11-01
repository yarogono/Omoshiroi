using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem
{
    [SerializeField] public BaseWeapon Weapon { get; set; }
    [SerializeField] public BaseMagic Magic { get; set; }
    [SerializeField] public BaseRune Rune { get; set; }
    [SerializeField] public BaseSkin Skin { get; set; }

    [SerializeField] private CharacterDataContainer cdc;

    public void Equip(BaseItem item)
    {
        if(item is IEquippable equipment)
        {
            equipment.Equip(cdc);
        }
        else { return; }
    }

    public void Dequip(BaseItem item)
    {
        if (item is IEquippable equipment)
        {
            equipment.Dequip(cdc);
        }
        else { return; }
    }
}
