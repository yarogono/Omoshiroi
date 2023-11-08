using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class CharacterDataContainer : MonoBehaviour
{
    

    [SerializeField] public Inventory1 Inven { get;}
    [field:SerializeField] public CharacterStats Stats { get; private set; }
    [field:SerializeField] public EquipSystem Equipments { get; private set; }

    public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterAnimationData AnimationData { get; private set; }
    public CharacterController Controller { get; private set; }
    public CharacterMovement Movement { get; private set; }
    public BaseInput InputActions { get; private set; }
    public PilotSync Sync { get; private set; }
    [field: SerializeField] public CharacterSpriteRotator SpriteRotator { get; private set; }
    public HealthSystem Health { get; private set; }

    private CombineStateMachine stateMachine;
   
    [Header("테스트용 착용아이템")]
    [SerializeField] private BaseItem[] TestEquipItem;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Movement = GetComponent<CharacterMovement>();
        InputActions = GetComponent<BaseInput>();
        Animator = GetComponent<Animator>();
        Sync = GetComponent<PilotSync>();
        Health = GetComponent<HealthSystem>();
        AnimationData.Initialize();
      
    }

    private void Start()
    {

       

        if (Stats.cbs != null)
            Stats.Initialize();

        if (Equipments == null)
            Equipments = new EquipSystem();

        foreach (var item in TestEquipItem)
            Equipments.Equip(item);

        stateMachine = new CombineStateMachine(this);
        SpriteRotator.Register(this);
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
