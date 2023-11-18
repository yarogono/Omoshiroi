using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model.Item
{
    public class WeaponItemDb
    {
        [Key]
        public int WeaponItemId { get; set; }

        public int TemplateId { get; set; }

        public int Quantity { get; set; }

        public bool Equipped { get; set; }

        public InventoryDb InventoryId { get; set; }
    }
}
