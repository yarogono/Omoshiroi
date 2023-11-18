using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model.Item
{
    public class PotionItemDb
    {
        [Key]
        public int PotionItemId { get; set; }

        public int TemplateId { get; set; }

        public InventoryDb InventoryId { get; set; }
    }
}
