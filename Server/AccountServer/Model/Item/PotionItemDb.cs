using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model.Item
{
    [Table("Potion_Item")]
    public class PotionItemDb
    {
        [Key]
        public int PotionItemId { get; set; }

        public int TemplateId { get; set; }

        [ForeignKey("inventoryId")]
        public InventoryDb InventoryId { get; set; }
    }
}
