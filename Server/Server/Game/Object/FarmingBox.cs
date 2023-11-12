using Google.Protobuf.Protocol;

namespace Server.Game.Object
{
    public class FarmingBox : GameObject
    {
        public bool IsOpen { get; set; }

        public List<FarmingBoxItem> items { get; private set; } = new List<FarmingBoxItem>();

        public FarmingBox()
        {
            ObjectType = GameObjectType.FarmingBox;
            IsOpen = false;
        }
    }
}
