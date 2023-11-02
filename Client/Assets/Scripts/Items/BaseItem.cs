using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : ScriptableObject
{
    [Header("BaseItemData")]
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private eItemType itemType;
    [SerializeField] private int maxStack;
    [SerializeField] private bool isStackable;

    public Sprite ItemIcon { get { return itemIcon; } }
    public GameObject ObjectPrefab { get { return objectPrefab; } }
    public int ItemID { get { return itemID; } }
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public eItemType ItemType { get { return itemType; } }
    public int MaxStack { get { return maxStack; } }
    public bool IsStackable { get { return isStackable; } }
}
