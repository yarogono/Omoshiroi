using Google.Protobuf.Protocol;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : CustomSingleton<ObjectManager>
{
    public t_PilotPlayerController pilotPlayerController { get; set; }
    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    public static GameObjectType GetObjectTypeById(int id)
    {
        int type = (id >> 24) & 0x7F;
        return (GameObjectType)type;
    }

    public void Add(ObjectInfo info, bool pilotPlayer = false)
    {
        GameObjectType objectType = GetObjectTypeById(info.ObjectId);

        if (objectType == GameObjectType.Player)
        {
            if (pilotPlayer)
            {
                GameObject gameObject = Resources.Load<GameObject>("t_PilotPlayer");
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);
                Instantiate(gameObject);
                pilotPlayerController = gameObject.GetComponent<t_PilotPlayerController>();
                pilotPlayerController.Id = info.ObjectId;
                pilotPlayerController.PosInfo = info.PosInfo;
                pilotPlayerController.Stat = info.StatInfo;
            }
            else
            {
                GameObject gameObject = Resources.Load<GameObject>("t_ClonePlayer");
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);
                Instantiate(gameObject);
                t_ClonePlayerController clonePlayerController =
                    gameObject.GetComponent<t_ClonePlayerController>();
                clonePlayerController.Id = info.ObjectId;
                clonePlayerController.PosInfo = info.PosInfo;
                clonePlayerController.Stat = info.StatInfo;
                clonePlayerController.SyncPos();
            }
        }
    }

    public GameObject FindById(int id)
    {
        _objects.TryGetValue(id, out GameObject gameObject);
        return gameObject;
    }
}
