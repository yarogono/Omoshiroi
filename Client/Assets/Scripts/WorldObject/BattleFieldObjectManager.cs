using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject fieldObject;

    public static BattleFieldObjectManager instance;

    private ObjectPool objectPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    public void SpawnFieldObject(Vector3 position, BattleFieldObject battleFieldObject)
    {
        GameObject obj = objectPool.SpawnFromPool(battleFieldObject.tag);

        obj.transform.position = position;

        //해당 오브젝트가 인벤토리를 가진 보관함이라면
        if(battleFieldObject is FarmingBox farmingBox)
        {
            //정해진 규칙에 따라 인벤토리 내에 아이템을 스폰시키는 내용
        }

        obj.SetActive(true);
    }
}
