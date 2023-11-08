using Google.Protobuf.Protocol;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : CustomSingleton<ObjectManager>
{
    public PilotSync pilotSync { get; set; }
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
            UnityEngine.Vector3 S_Position = new UnityEngine.Vector3(
                info.Position.X,
                info.Position.Y,
                info.Position.Z
            );

            if (pilotPlayer)
            {
                GameObject gameObject = Instantiate(
                    Resources.Load<GameObject>("Prefabs/PilotPlayer")
                );
                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                pilotSync = gameObject.GetComponent<PilotSync>();
                pilotSync.Id = info.ObjectId;
                pilotSync.position = S_Position;
                // pilotSync.Stat = info.StatInfo;
            }
            else
            {
                UnityEngine.Vector3 SpawnPos = S_Position;

                GameObject gameObject = Instantiate(
                    Resources.Load<GameObject>("Prefabs/ClonePlayer"),
                    SpawnPos,
                    Quaternion.identity
                );

                gameObject.name = info.Name;
                _objects.Add(info.ObjectId, gameObject);

                CloneSync cloneSync = gameObject.GetComponent<CloneSync>();
                cloneSync.Id = info.ObjectId;
                cloneSync.position = S_Position;
                // cloneSync.Stat = info.StatInfo;
                cloneSync.SyncPosition();
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
        pilotSync = null;
    }
}
