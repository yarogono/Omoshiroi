using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model.Item
{
    [Table("Rune_Item")]
    public class RuneItemDb
    {
        [Key]
        public int RuneItemId { get; set; }
        
        public int TemplateId { get; set; }

        public bool Equipped { get; set; }

        [ForeignKey("inventoryId")]
        public InventoryDb InventoryId { get; set; }
    }
}
