using Google.Protobuf.Protocol;

namespace Server.Game.Object
{
    public class Player : GameObject
    {

        public ClientSession Session { get; set; }

        public StatInfo Stat { get; private set; } = new StatInfo();

        public Player()
        {
            ObjectType = GameObjectType.Player;
            Info.StatInfo = Stat;
        }

        public virtual void HpDamage(C_ChangeHp changeHpPacket)
        {

            int currentHp = changeHpPacket.CurrentHp;

            if (currentHp > 0)
                return;

            Stat.Hp = currentHp;

            S_ChangeHp resChangeHpPacket = new S_ChangeHp();
            resChangeHpPacket.ObjectId = Id;
            resChangeHpPacket.CurrentHp = Stat.Hp;

            Room.Broadcast(resChangeHpPacket, Id);
        }

        public virtual void OnDead(GameObject attacker)
        {
            if (Room == null)
                return;

            S_Die diePacket = new S_Die();
            diePacket.ObjectId = Id;
            diePacket.AttackerId = attacker.Id;
            Room.Broadcast(diePacket);

            Room.LeaveGame(Id);

            Stat.Hp = Stat.MaxHp;
            Info.PosInfo.PosX = 0;
            Info.PosInfo.PosY = 0;
            Info.PosInfo.PosZ = 0;

            Room.EnterGame(this);
        }
    }
}
