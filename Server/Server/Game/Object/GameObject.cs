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

        public virtual void OnDamaged(GameObject attacker, int damage)
        {
            if (Room == null)
                return;

            Stat.Hp = Math.Max(Stat.Hp -= damage, 0);

            S_HpDamage hpDamagePacket = new S_HpDamage();
            hpDamagePacket.ObjectId = Id;
            hpDamagePacket.CurrentHp = Stat.Hp;
            Room.Broadcast(hpDamagePacket);

            if (Stat.Hp <= 0)
            {
                OnDead(attacker);
            }
        }

        public virtual void OnDead(GameObject attacker)
        {
            if (Room == null)
                return;

            S_Die diePacket = new S_Die();
            diePacket.ObjectId = Id;
            diePacket.AttackerId = attacker.Id;
            Room.Broadcast(diePacket);

            GameRoom room = Room;
            room.LeaveGame(Id);

            Stat.Hp = Stat.MaxHp;
            Info.Position.X = 0;
            Info.Position.Y = 0;
            Info.Position.Z = 0;

            room.EnterGame(this);
        }
    }
}
