using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.DB
{
    [Table("Account")]
    public class AccountDb
    {
        [Key]
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string AccountPassword { get; set; }

        [ForeignKey("PlayerId")]
        public PlayerDb Player { get; set; }
    }

    [Table("Player")]
    public class PlayerDb
    {
        [Key]
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        [ForeignKey("PlayerStatId")]
        public PlayerStatDb PlayerStat { get; set; }

        public ICollection<ItemDb> Items { get; set; }
    }

    [Table("Player_Stat")]
    public class PlayerStatDb
    {
        [Key]
        public int PlayerStatId { get; set; }

        public int Level { get; set; }

        public int MaxHp { get; set; }
        
        public int Hp { get; set; }

        public int Atk { get; set; }

        public float AtkSpeed { get; set; }

        public int CritRate { get; set; }

        public float CritDamage { get; set; }

        public float MoveSpeed { get; set; }

        public float RunMultiplier { get; set; }

        public float DodgeTime { get; set; }
    }


    [Table("Item")]
    public class ItemDb
    {
        [Key]
        public int ItemId { get; set; }
        
        public int TemplateId { get; set; }

        public int Quantity { get; set; }

        public string ItemName { get; set; }

        [ForeignKey("PlayerId")]
        public PlayerDb Player { get; set; }
    }
}
