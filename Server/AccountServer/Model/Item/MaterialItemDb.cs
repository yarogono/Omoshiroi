using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model.Item
{
    [Table("Material_Item")]
    public class MaterialItemDb
    {
        [Key]
        public int MaterialItemId { get; set; }

        public int TemplateId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("inventoryId")]
        public InventoryDb InventoryId { get; set; }
    }
}
