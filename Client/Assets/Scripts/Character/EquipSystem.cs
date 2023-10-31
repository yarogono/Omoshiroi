using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem
{
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private BaseMagic magic;
    [SerializeField] private BaseRune rune;
    [SerializeField] private BaseSkin skin;
    private CharacterDataContainer cdc;

    public EquipSystem(CharacterDataContainer characterDataContainer)
    {
        cdc = characterDataContainer;
    }

    public BaseWeapon Weapon { get; set; }
    public BaseMagic Magic { get; set; }
    public BaseRune Rune { get; set; }
    public BaseSkin Skin { get; set; }

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
