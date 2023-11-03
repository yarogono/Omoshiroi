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
    [SerializeField] private AttackData attackData;

    public void Equip(CharacterDataContainer cdc)
    {
    }

    public void Dequip(CharacterDataContainer cdc)
    {
    }

    public GameObject MagicObject { get { return magicObject; } }
    public float Range { get { return range; } }
    public Animation Anime { get { return anime; } }
    public AttackData AttackData { get {  return attackData; } }
}
