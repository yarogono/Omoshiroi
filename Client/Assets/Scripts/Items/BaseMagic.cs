using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseMagicSO", menuName = "Item/BaseMagicSO")]
public class BaseMagic : BaseItem, IEquippable
{
    [Header("MagicData")]
    [SerializeField] private GameObject magicObject;
    [SerializeField] private float range;
    [SerializeField] private Animation anime;

    public void Equip(CharacterDataContainer cdc)
    {
        cdc.Equipments.Magic = this;
    }

    public void Dequip(CharacterDataContainer cdc)
    {
        cdc.Equipments.Magic = null;
    }

    public GameObject MagicObject { get { return magicObject; } }
    public float Range { get { return range; } }
    public Animation Anime { get { return anime; } }
}
