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

    public void SetCharcterStats(CharacterStats cs)
    {
        stats.AttackSpeed += cs.AttackSpeed;
        stats.AttackPoint += cs.AttackPoint;
        stats.CriticalRate += cs.CriticalRate;
        stats.CriticalPower += cs.CriticalPower;
        stats.BaseHP += cs.BaseHP;
        stats.BaseDEF += cs.BaseDEF;
    }
}
