using System;
using System.Collections.Generic;

namespace Data
{
    #region WeaponItem
    [Serializable]
    public class WeaponItem
    {
        public int id;
        public string name;
        public int damage;
    }

    [Serializable]
    public class WeaponItemData : ILoader<int, WeaponItem>
    {
        public List<WeaponItem> WeaponItems = new List<WeaponItem>();


        public Dictionary<int, WeaponItem> MakeDict()
        {
            Dictionary<int, WeaponItem> dict = new Dictionary<int, WeaponItem>();
            foreach (WeaponItem skill in WeaponItems)
                dict.Add(skill.id, skill);
            return dict;
        }
    }
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
        public List<(int, int)> inventory;
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