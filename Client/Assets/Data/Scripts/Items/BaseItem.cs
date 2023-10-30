using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    [Header("BaseData")]
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private eItemType itemType;

    public int ItemID { get;}
    public string ItemName { get; }
    public string Description { get; }
    public eItemType ItemType { get; }
}
