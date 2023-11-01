using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour
{
    [SerializeField] public Inventory Inven { get;}
    [SerializeField] public CharacterStats Stats { get;}
    [SerializeField] public EquipSystem Equipments { get; private set; }


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
