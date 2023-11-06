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
                GameObject gameObject = Instantiate(
                    Resources.Load<GameObject>("Prefabs/PilotPlayer")
                );
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                pilotPlayerController = gameObject.GetComponent<t_PilotPlayerController>();
                pilotPlayerController.Id = info.ObjectId;
                pilotPlayerController.PosInfo = info.PosInfo;
                pilotPlayerController.Stat = info.StatInfo;
            }
            else
            {
                Vector3 SpawnPos = new Vector3(
                    info.PosInfo.PosX,
                    info.PosInfo.PosY,
                    info.PosInfo.PosZ
                );

                GameObject gameObject = Instantiate(
                    Resources.Load<GameObject>("Prefabs/ClonePlayer"),
                    SpawnPos,
                    Quaternion.identity
                );

                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                t_ClonePlayerController clonePlayerController =
                    gameObject.GetComponent<t_ClonePlayerController>();
                clonePlayerController.Id = info.ObjectId;
                clonePlayerController.PosInfo = info.PosInfo;
                clonePlayerController.Stat = info.StatInfo;
                clonePlayerController.SyncPos();
            }
        }
    }

    public void Remove(int id)
    {
        GameObject gameObject = FindById(id);
        if (gameObject == null)
            return;

        _objects.Remove(id);
        Destroy(gameObject);
    }

    public GameObject FindById(int id)
    {
        _objects.TryGetValue(id, out GameObject gameObject);
        return gameObject;
    }

    public void Clear()
    {
        foreach (GameObject gameObject in _objects.Values)
        {
            Destroy(gameObject);
        }

        _objects.Clear();
        pilotPlayerController = null;
    }
}
