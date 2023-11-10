using Google.Protobuf.Protocol;
using Org.BouncyCastle.Asn1.Mozilla;

namespace Server.Game.Object
{
    public class FarmingBox : GameObject
    {

        public FarmingBox()
        {
            ObjectType = GameObjectType.FarmingBox;
        }

        public FarmingBoxInfo BoxInfo { get; set; } = new FarmingBoxInfo();

    }
}
