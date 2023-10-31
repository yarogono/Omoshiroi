using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseSkinSO", menuName = "Item/BaseSkinSO")]
public class BaseSkin : BaseItem, IEquippable
{
    public void Equip(CharacterDataContainer cdc)
    {
        cdc.Equipments.Skin = this;
    }

    public void Dequip(CharacterDataContainer cdc)
    {
        cdc.Equipments.Skin = null;
    }
}
