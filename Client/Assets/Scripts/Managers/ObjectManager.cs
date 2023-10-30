using Google.Protobuf.Protocol;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : CustomSingleton<ObjectManager>
{
    //public MyPlayerController MyPlayer { get; set; }
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
            
            GameObject go = Resources.Load<GameObject>("TestPlayer");
            go.name = info.Name;
            _objects.Add(info.ObjectId, go);

            Debug.Log($"{info.Name} Crated");
            t_PlayerController pc = go.GetComponent<t_PlayerController>();
            go.transform.position = new Vector3(info.PosInfo.PosX, info.PosInfo.PosY, 0);
            Instantiate(go);
            //pc.Id = info.ObjectId;
            //pc.PosInfo = info.PosInfo;
            //pc.Stat = info.StatInfo;
        }
    }
}
