using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.Model
{
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

        public PlayerDb playerId { get; set; }

        public PlayerStatDb() { }

        public PlayerStatDb(InitPlayerStatReq req)
        {
            Level = req.Level;
            MaxHp = req.MaxHp;
            Hp = req.Hp;
            Atk = req.Atk;
            AtkSpeed = req.AtkSpeed;
            CritRate = req.CritRate;
            CritDamage = req.CritDamage;
            MoveSpeed = req.MoveSpeed;
            RunMultiplier = req.RunMultiplier;
            DodgeTime = req.DodgeTime;
        }
    }
}
