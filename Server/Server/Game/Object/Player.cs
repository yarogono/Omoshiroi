using Google.Protobuf.Protocol;

namespace Server.Game.Object
{
    public class Player : GameObject
    {

        public ClientSession Session { get; set; }

        public Player()
        {
            ObjectType = GameObjectType.Player;
        }

        public override void HpDamage(C_HpDamage hpDamagePacket)
        {
            base.HpDamage(hpDamagePacket);
        }

        public override void OnDead(GameObject attacker)
        {
            base.OnDead(attacker);
        }
    }
}
