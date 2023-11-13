using Server.Data;


public class Data
{
    #region FarmingBoxInventory
    [Serializable]
    public class FBInventory
    {
        public int inventoryId;
        public List<FBInventoryItem> inventory;
    }

    public class FBInventoryItem
    {
        public int itemId;
        public int quantity;
    }

    public class FarmingBoxInventoryData : ILoader<int, FBInventory>
    {
        public List<FBInventory> FarmingBoxInventory = new List<FBInventory>();

        public Dictionary<int, FBInventory> MakeDict()
        {
            Dictionary<int, FBInventory> dict = new Dictionary<int, FBInventory>();
            foreach (FBInventory inventory in FarmingBoxInventory)
            {
                dict.Add(inventory.inventoryId, inventory);
            }
            return dict;
        }
    }
    #endregion
}
