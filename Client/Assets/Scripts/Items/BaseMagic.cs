using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseMagicSO", menuName = "Item/BaseMagicSO")]
public class BaseMagic : BaseItem, IEquippable
{
    [Header("MagicData")]
    [SerializeField] protected GameObject magicObject;
    [SerializeField] protected float range;
    [SerializeField] protected Animation anime;

    public void Equip(CharacterDataContainer cdc)
    {
    }

    public void Dequip(CharacterDataContainer cdc)
    {
    }

    public GameObject MagicObject { get { return magicObject; } }
    public float Range { get { return range; } }
    public Animation Anime { get { return anime; } }
}
