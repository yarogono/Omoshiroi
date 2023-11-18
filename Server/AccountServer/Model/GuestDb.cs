using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model
{
    public class GuestDb
    {
        [Key]
        public int GuestId { get; set; }

        public string GuestUid { get; set; }

        public PlayerDb PlayerId { get; set; }
    }
}
