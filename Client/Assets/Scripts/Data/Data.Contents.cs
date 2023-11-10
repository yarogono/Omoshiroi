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
    public class FarmingBoxInventoryItem
    {
        public int itemId;
        public int quantity;
    }


    #endregion
}