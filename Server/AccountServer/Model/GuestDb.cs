using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model
{
    [Table("Guest")]
    public class GuestDb
    {
        [Key]
        public int GuestId { get; set; }

        public string GuestUid { get; set; }

        [ForeignKey("playerId")]
        public PlayerDb PlayerId { get; set; }
    }
}
