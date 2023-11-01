using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour
{
    [SerializeField] public Inventory Inven { get; }
    [SerializeField] public CharacterStats Stats { get; }
    public EquipSystem Equipments { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        Equipments = new EquipSystem(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
