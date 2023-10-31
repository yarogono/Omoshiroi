using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour
{
    [SerializeField] private Inventory inven;
    [SerializeField] private CharacterStats stats;
    [SerializeField] private EquipSystem equipments;

    public Inventory Inven { get; set; }
    public CharacterStats Stats { get; set; }
    public EquipSystem Equipments { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
