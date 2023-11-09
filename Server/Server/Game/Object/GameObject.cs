using Google.Protobuf.Protocol;
using Server.Game.Room;

namespace Server.Game.Object
{
    public class GameObject
    {
        public GameObjectType ObjectType { get; protected set; } = GameObjectType.None;
        public int Id
        {
            get { return Info.ObjectId; }
            set { Info.ObjectId = value; }
        }

        public GameRoom Room { get; set; }

        public ObjectInfo Info { get; set; } = new ObjectInfo();
        public StatInfo Stat { get; private set; } = new StatInfo();

        public float Speed
        {
            get { return Stat.Speed; }
            set { Stat.Speed = value; }
        }

        public int Hp
        {
            get { return Stat.Hp; }
            set { Stat.Hp = Math.Clamp(value, 0, Stat.MaxHp); }
        }

        public GameObject()
        {
            Info.StatInfo = Stat;
        }

        public virtual void HpDamage(C_HpDamage hpDamagePacket)
        {
            if (Room == null)
                return;

            int maxHp = hpDamagePacket.MaxHp;
            int currentHp = hpDamagePacket.CurrentHp;
            int objectId = hpDamagePacket.ObjectId;
            int changeAmount = hpDamagePacket.ChangeAmount;

            int damagedHp = Math.Max(Stat.Hp -= changeAmount, 0);

            if (damagedHp != currentHp)
                return;

            Stat.Hp = currentHp;
            Info.StatInfo.MaxHp = maxHp;

            S_HpDamage resHpDamagePacket = new S_HpDamage();
            resHpDamagePacket.ObjectId = objectId;
            resHpDamagePacket.CurrentHp = Stat.Hp;

            Room.Broadcast(resHpDamagePacket, objectId);
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
            Info.Position.X = 0;
            Info.Position.Y = 0;
            Info.Position.Z = 0;

            Room.EnterGame(this);
        }
    }
}
