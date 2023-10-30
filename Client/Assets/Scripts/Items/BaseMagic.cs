using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
