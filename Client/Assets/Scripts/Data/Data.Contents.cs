using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region ItemData

    #region WeaponItem
    [Serializable]
    public class WeaponItem : BaseWeapon
    {
    }

    [Serializable]
    public class WeaponItemData : ILoader<int, WeaponItem>
    {
        public List<WeaponItem> items = new List<WeaponItem>();

        public Dictionary<int, WeaponItem> MakeDict()
        {
            Dictionary<int, WeaponItem> dict = new Dictionary<int, WeaponItem>();
            foreach (WeaponItem item in items)
                dict.Add(item.ItemID, item);
            return dict;
        }
    }
    #endregion

    #region SkinItem
    [Serializable]
    public class MagicItem : BaseMagic
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
                dict.Add(item.ItemID, item);
            return dict;
        }
    }
    #endregion

    #region RuneItem
    [Serializable]
    public class RuneItem : BaseRune
    {
    }

    [Serializable]
    public class RuneItemData : ILoader<int, RuneItem>
    {
        public List<RuneItem> items = new List<RuneItem>();

        public Dictionary<int, RuneItem> MakeDict()
        {
            Dictionary<int, RuneItem> dict = new Dictionary<int, RuneItem>();
            foreach (RuneItem item in items)
                dict.Add(item.ItemID, item);
            return dict;
        }
    }
    #endregion

    #region ResourceItem
    [Serializable]
    public class ResourceItem : BaseResource
    {
    }

    [Serializable]
    public class ResourceItemData : ILoader<int, ResourceItem>
    {
        public List<ResourceItem> items = new List<ResourceItem>();

        public Dictionary<int, ResourceItem> MakeDict()
        {
            Dictionary<int, ResourceItem> dict = new Dictionary<int, ResourceItem>();
            foreach (ResourceItem item in items)
                dict.Add(item.ItemID, item);
            return dict;
        }
    }
    #endregion

    #region ConsumableItem
    [Serializable]
    public class ConsumableItem : BaseConsumable
    {
    }

    [Serializable]
    public class ConsumableItemData : ILoader<int, ConsumableItem>
    {
        public List<ConsumableItem> items = new List<ConsumableItem>();

        public Dictionary<int, ConsumableItem> MakeDict()
        {
            Dictionary<int, ConsumableItem> dict = new Dictionary<int, ConsumableItem>();
            foreach (ConsumableItem item in items)
                dict.Add(item.ItemID, item);
            return dict;
        }
    }
    #endregion

    #region SkinItem
    [Serializable]
    public class SkinItem : BaseSkin
    {
    }

    [Serializable]
    public class SkinItemData : ILoader<int, SkinItem>
    {
        public List<SkinItem> items = new List<SkinItem>();

        public Dictionary<int, SkinItem> MakeDict()
        {
            Dictionary<int, SkinItem> dict = new Dictionary<int, SkinItem>();
            foreach (SkinItem item in items)
                dict.Add(item.ItemID, item);
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