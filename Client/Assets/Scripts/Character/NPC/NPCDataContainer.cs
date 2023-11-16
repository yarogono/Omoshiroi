using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDataContainer : DataContainer
{
    [Header("몬스터 스탯 및 장비")]
    [SerializeField] private CharacterBaseStats MonsterStats;
    [SerializeField] private List<BaseItem> MonsterEquipments;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        SpriteRotator = GetComponent<CharacterSpriteRotator>();
        Health = GetComponent<HealthSystem>();
        Equipments = new EquipSystem();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
