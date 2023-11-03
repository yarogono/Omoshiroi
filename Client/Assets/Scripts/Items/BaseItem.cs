using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : ScriptableObject
{
    [Header("BaseItemData")]
    [SerializeField] protected Sprite itemIcon;
    [SerializeField] protected GameObject objectPrefab;
    [SerializeField] protected int itemID;
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;
    [SerializeField] protected eItemType itemType;
    [SerializeField] protected int maxStack;
    [SerializeField] protected bool isStackable;

    public Sprite ItemIcon { get { return itemIcon; } }
    public GameObject ObjectPrefab { get { return objectPrefab; } }
    public int ItemID { get { return itemID; } }
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public eItemType ItemType { get { return itemType; } }
    public int MaxStack { get { return maxStack; } }
    public bool IsStackable { get { return isStackable; } }
}
