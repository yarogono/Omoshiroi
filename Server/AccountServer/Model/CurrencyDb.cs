using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model
{
    public class CurrencyDb
    {
        [Key]
        public int CurrencyId { get; set; }

        public  int Gold { get; set; }

        public int Diamond { get; set; }

        public PlayerDb PlayerId { get; set; }
    }
}
