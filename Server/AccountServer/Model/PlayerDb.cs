using AccountServer.Model.Item;
using AccountServer.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model
{
    [Table("Player")]
    public class PlayerDb
    {
        [Key]
        public int PlayerId { get; set; }

        public string Nickname { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
