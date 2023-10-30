using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseMagicSO", menuName = "Item/BaseMagicSO")]
public class BaseMagic : BaseItem, IEquipable
{
    [SerializeField] private GameObject magicObject;
    [SerializeField] private float range;
    [SerializeField] private Animation anime;

    public void Equip()
    {

    }

    public void Dequip()
    {

    }

    public GameObject MagicObject { get { return magicObject; } }
    public float Range { get { return range; } }
    public Animation Anime { get { return anime; } }
}
