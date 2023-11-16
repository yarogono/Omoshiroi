using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class NPCDataContainer : DataContainer
{
    public CharacterController Controller { get; private set; }
    [Header("테스트용 몬스터 스탯 및 장비")]
    [SerializeField] private CharacterBaseStats _testMonsterStats;
    [SerializeField] private List<BaseItem> _testMonsterEquipments;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Health = GetComponent<HealthSystem>();
        Controller = GetComponent<CharacterController>();
        // 추후에 서버로 AI를 옮기면 SyncModule에 대한 참조도 필요함.

        if (_testMonsterStats != null)
            Stats.SetCharacterStats(_testMonsterStats.BaseHP,
                _testMonsterStats.BaseHP,
                _testMonsterStats.BaseDEF,
                _testMonsterStats.BaseAttackSpeed,
                _testMonsterStats.BaseAttackPower,
                _testMonsterStats.BaseCriticalRate,
                _testMonsterStats.BaseCriticalPower,
                _testMonsterStats.BaseMoveSpeed,
                _testMonsterStats.BaseRunMultiplier);

        Equipments = new EquipSystem(this);
        if (_testMonsterEquipments != null)
        {
            foreach (var item in _testMonsterEquipments)
                Equipments.Equip(item);
        }

        AnimationData.Initialize();

        SpriteRotator.Register(this);
    }
}
