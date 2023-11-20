using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model.Item
{
    [Table("Weapon_Item")]
    public class WeaponItemDb
    {
        [Key]
        public int WeaponItemId { get; set; }

        public int TemplateId { get; set; }

        public int Quantity { get; set; }

        public bool Equipped { get; set; }

        [ForeignKey("inventoryId")]
        public InventoryDb InventoryId { get; set; }
    }
}
