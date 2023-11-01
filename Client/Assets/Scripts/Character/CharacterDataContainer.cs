using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour
{
    [SerializeField] private Inventory inven;
    [SerializeField] private CharacterStats stats;
    [SerializeField] private EquipSystem equipments;

    public Inventory Inven { get; }
    public CharacterStats Stats { get; }
    public EquipSystem Equipments { get; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
