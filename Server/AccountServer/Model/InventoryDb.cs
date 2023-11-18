using AccountServer.Model.Item;
using System.ComponentModel.DataAnnotations;

namespace AccountServer.Model
{
    public class InventoryDb
    {
        [Key]
        public int InventoryId { get; set; }

        public ICollection<MaterialItemDb> MaterialItems { get; set; }

        public ICollection<PotionItemDb> PotionItems { get; set; }

        public ICollection<RuneItemDb> RuneItems { get; set; }

        public ICollection<WeaponItemDb> WeaponItems { get; set; }

        public PlayerDb PlayerId { get; set; }
    }
}
