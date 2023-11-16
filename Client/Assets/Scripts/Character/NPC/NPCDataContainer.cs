using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
}

public class NPCDataContainer : DataContainer
{
    public CharacterController Controller { get; private set; }
    [Header("테스트용 몬스터 스탯 및 장비")]
    [SerializeField] private CharacterBaseStats _testMonsterStats;
    [SerializeField] private List<BaseItem> _testMonsterEquipments;
    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
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

    //Vector3 GetFleeLocation()
    //{
    //    NavMeshHit hit;

    //    NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

    //    int i = 0;
    //    while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
    //    {

    //        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
    //        i++;
    //        if (i == 30)
    //            break;
    //    }

    //    return hit.position;
    //}

    //float GetDestinationAngle(Vector3 targetPos)
    //{
    //    return Vector3.Angle(transform.position - PlayerController.instance.transform.position, transform.position + targetPos);
    //}
}
