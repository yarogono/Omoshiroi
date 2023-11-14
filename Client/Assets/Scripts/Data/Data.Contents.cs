using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region ItemData
    public class ItemData
    {
        public Sprite icon;
        public GameObject objectPrefab;
        public int id;
        public string name;
        public string desciption;
        public eItemType itemType;
        public int maxStack;
        public bool isStackable;
    }

    #region WeaponItem
    [Serializable]
    public class WeaponItem : ItemData
    {
        public int hp;
        public int def;
        public float atkSpeed;
        public int atk;
        public int critRate;
        public float critPower;
    }

    [Serializable]
    public class WeaponItemData : ILoader<int, WeaponItem>
    {
        public List<WeaponItem> items = new List<WeaponItem>();


        public Dictionary<int, WeaponItem> MakeDict()
        {
            Dictionary<int, WeaponItem> dict = new Dictionary<int, WeaponItem>();
            foreach (WeaponItem item in items)
                dict.Add(item.id, item);
            return dict;
        }
    }
    #endregion

    #region MagicItem
    [Serializable]
    public class MagicItem : ItemData
    {
    }

    [Serializable]
    public class MagicItemData : ILoader<int, MagicItem>
    {
        public List<MagicItem> items = new List<MagicItem>();


        public Dictionary<int, MagicItem> MakeDict()
        {
            Dictionary<int, MagicItem> dict = new Dictionary<int, MagicItem>();
            foreach (MagicItem item in items)
                dict.Add(item.id, item);
            return dict;
        }
    }
    #endregion

    #endregion



    #region FarmingBoxInventory
    [Serializable]
    public class FBInventoryItem
    {
        public int itemId;
        public int quantity;
    }

    public class FBInventory
    {
        public int inventoryId;
        public List<FBInventoryItem> inventory;
    }

    public class FarmingBoxInventoryData : ILoader<int, FBInventory>
    {
        public List<FBInventory> fBInventories = new List<FBInventory>();

        public Dictionary<int, FBInventory> MakeDict()
        {
            Dictionary<int, FBInventory> dict = new Dictionary<int, FBInventory>();
            foreach (FBInventory inventory in fBInventories)
            {
                dict.Add(inventory.inventoryId, inventory);
            }
            return dict;
        }
    }

    #endregion
}