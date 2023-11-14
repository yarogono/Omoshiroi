using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldObjectSpawner : MonoBehaviour
{
    public static BattleFieldObjectSpawner instance;
    public GameObject farmingBox;

    private void Awake()
    {
        if (instance == null) { instance = this; }    
    }

    public void SpawnFarmingBox(Vector3 pos)
    {
        GameObject fb = Instantiate(farmingBox, pos, Quaternion.Euler(0,0,0));
        fb.SetActive(true);
    }
}
