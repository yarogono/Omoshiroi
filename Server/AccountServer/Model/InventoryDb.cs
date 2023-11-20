using AccountServer.Model.Item;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model
{
    [Table("Inventory")]
    public class InventoryDb
    {
        [Key]
        public int InventoryId { get; set; }

        public ICollection<MaterialItemDb>? MaterialItems { get; set; }

        public ICollection<PotionItemDb>? PotionItems { get; set; }

        public ICollection<RuneItemDb>? RuneItems { get; set; }

        public ICollection<WeaponItemDb>? WeaponItems { get; set; }

        [ForeignKey("playerId")]
        public PlayerDb PlayerId { get; set; }
    }
}
