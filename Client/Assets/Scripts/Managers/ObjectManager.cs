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

    public void Update()
    {
        Debug.Log($"_objects.Count : {_objects.Count}");
    }

    public void Add(ObjectInfo info, bool pilotPlayer = false)
    {
        GameObjectType objectType = GetObjectTypeById(info.ObjectId);

        if (objectType == GameObjectType.Player)
        {
            if (pilotPlayer)
            {
                GameObject gameObject = Instantiate(Resources.Load<GameObject>("t_PilotPlayer"));
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                pilotPlayerController = gameObject.GetComponent<t_PilotPlayerController>();
                pilotPlayerController.Id = info.ObjectId;
                Debug.Log($"info.Name : {info.Name}\n");
                Debug.Log($"info.ObjectId : {info.ObjectId}\n");
                Debug.Log($"pilotPlayerController.Id : {pilotPlayerController.Id}\n");
                Debug.Log(
                    $"gameObject.Id : {gameObject.GetComponent<t_PilotPlayerController>().Id}"
                );
                pilotPlayerController.PosInfo = info.PosInfo;
                pilotPlayerController.Stat = info.StatInfo;
            }
            else
            {
                GameObject original = Resources.Load<GameObject>("t_ClonePlayer");

                GameObject gameObject = Instantiate(original, null);
                gameObject.name = original.name;
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                t_ClonePlayerController clonePlayerController =
                    gameObject.GetComponent<t_ClonePlayerController>();
                clonePlayerController.Id = info.ObjectId;
                Debug.Log($"clonePlayerController.Id : {clonePlayerController.Id}");
                clonePlayerController.PosInfo = info.PosInfo;
                clonePlayerController.Stat = info.StatInfo;
                clonePlayerController.SyncPos();

                Instantiate(gameObject);
            }
        }
    }

    public GameObject FindById(int id)
    {
        _objects.TryGetValue(id, out GameObject gameObject);
        return gameObject;
    }
}
