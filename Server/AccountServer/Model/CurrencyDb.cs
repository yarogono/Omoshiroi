using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model
{
    [Table("Currency")]
    public class CurrencyDb
    {
        [Key]
        public int CurrencyId { get; set; }

        public  int Gold { get; set; }

        public int Diamond { get; set; }

        [ForeignKey("playerId")]
        public PlayerDb PlayerId { get; set; }
    }
}
