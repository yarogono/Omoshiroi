using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterDataContainer : DataContainer
{
    public CharacterController Controller { get; private set; }
    public CharacterMovement Movement { get; private set; }
    public BaseInput InputActions { get; private set; }
    public PilotSync Sync { get; private set; }

    private CombineStateMachine stateMachine;

    [Header("테스트용 착용아이템")]
    [SerializeField]
    private BaseItem[] TestEquipItem;
    [Header("테스트용 스탯")]
    [SerializeField]
    private bool isApply;
    [SerializeField]
    private CharacterBaseStats TestStats;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Movement = GetComponent<CharacterMovement>();
        InputActions = GetComponent<BaseInput>();
        Animator = GetComponent<Animator>();
        Sync = GetComponent<PilotSync>();

        AnimationData.Initialize();
    }

    private void Start()
    {
        if (Equipments == null)
            Equipments = new EquipSystem(this);

        foreach (var item in TestEquipItem)
            Equipments.Equip(item);

        stateMachine = new CombineStateMachine(this);
        SpriteRotator.Register(this);

        if (isApply && TestStats != null)
        {
            Stats.SetCharacterStats(TestStats.BaseHP, TestStats.BaseHP, TestStats.BaseDEF, TestStats.BaseAttackSpeed, TestStats.BaseAttackPower,
                TestStats.BaseCriticalRate, TestStats.BaseCriticalPower, TestStats.BaseMoveSpeed, TestStats.BaseRunMultiplier);
        }
    }

    void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    private void OnDrawGizmos()
    {
        Ray downRay = new Ray(transform.position, Vector3.down);

        Gizmos.color = Color.red;

        Gizmos.DrawRay(downRay);
    }
}
