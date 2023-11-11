using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingBoxSpawner : MonoBehaviour
{
    public static FarmingBoxSpawner instance;
    public GameObject farmingBox;

    private void Awake()
    {
        if (instance == null) { instance = this; }    
    }

    public void SpawnFarmingBox(Vector3 pos)
    {
        Instantiate(farmingBox, pos, Quaternion.Euler(0,0,0));
    }
}
