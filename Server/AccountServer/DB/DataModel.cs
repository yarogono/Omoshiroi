using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.DB
{
    [Table("Account")]
    public class AccountDb
    {
        [Key]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "AccountName를 입력하세요.")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "AccountPassword를 입력하세요.")]
        public string AccountPassword { get; set; }

        public ICollection<ItemDb> Items { get; set; }
    }


    [Table("Item")]
    public class ItemDb
    {
        [Key]
        public int ItemId { get; set; }
        
        public int TemplateId { get; set; }

        public int Quantity { get; set; }
    }
}
