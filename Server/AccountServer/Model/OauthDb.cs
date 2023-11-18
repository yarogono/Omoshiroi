using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AccountServer.Utils.Define;

namespace AccountServer.Model
{
    public class OauthDb
    {
        [Key]
        public int OauthId { get; set; }

        public string OauthToken { get; set; }

        public OauthType oatuhType { get; set; }

        public DateTime CreatedAt { get; set; }

        public PlayerDb PlayerId { get; set; }
    }
}
