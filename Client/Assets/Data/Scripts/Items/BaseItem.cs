using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item/ItemSO")]
public class BaseItem : ScriptableObject
{
    [Header("ItemData")]
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private eItemType itemType;

    public int ItemID { get;}
    public string ItemName { get; }
    public string Description { get; }
    public eItemType ItemType { get; }
}
