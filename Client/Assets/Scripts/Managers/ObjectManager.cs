using Google.Protobuf.Protocol;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : CustomSingleton<ObjectManager>
{
    public t_PlayerController playerController { get; set; }
    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    public static GameObjectType GetObjectTypeById(int id)
    {
        int type = (id >> 24) & 0x7F;
        return (GameObjectType)type;
    }

    public void Add(ObjectInfo info)
    {
        GameObjectType objectType = GetObjectTypeById(info.ObjectId);

        if (objectType == GameObjectType.Player)
        {
            if (playerController)
            {
                GameObject gameObject = Resources.Load<GameObject>("Player");
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                playerController = GetComponent<t_PlayerController>();
                playerController.Id = info.ObjectId;
                playerController.PosInfo = info.PosInfo;
                playerController.Stat = info.StatInfo;
                playerController.SyncPos();
            }
            else
            {
                GameObject gameObject = Resources.Load<GameObject>("Creature/Player");
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                t_ClonePlayerController clonePlayerController =
                    gameObject.GetComponent<t_ClonePlayerController>();
                clonePlayerController.Id = info.ObjectId;
                clonePlayerController.PosInfo = info.PosInfo;
                clonePlayerController.Stat = info.StatInfo;
                playerController.SyncPos();
            }
        }
    }

    public GameObject FindById(int id)
    {
        _objects.TryGetValue(id, out GameObject gameObject);
        return gameObject;
    }
}
