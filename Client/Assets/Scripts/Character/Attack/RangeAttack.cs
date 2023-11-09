using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangeAttack : BaseAttack
{
    [SerializeField] private float speed = 10f; // 공격의 속도
    [SerializeField] private float lifeTime = 5f; // 공격이 존재할 수 있는 시간
    
    
    Vector3 impact;
    private void Update()
    {
        // 매 프레임마다 공격을 전방으로 이동시킴
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public override void Initalize(AttackInfo attackInfo, DataContainer dataContainer, string tag)
    {
        int ap = dataContainer.Stats.AtkPower;
        base.Initalize(attackInfo, dataContainer, tag);
        Damage = dataContainer.Stats.AtkPower;
        Launch();
    }

    public void Launch()
    {


        // 공격생명주기
        Invoke(nameof(Deactivate), lifeTime);

    }

    private void Deactivate()
    {
        // 오브젝트를 비활성화하고 오브젝트 풀로 반환
        //AttackManager.Instance.ReturnAttackToPool(this.gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 다른 콜라이더와 충돌 시 호출됨
        // 이곳에 충돌한 대상에 대한 처리 로직을 구현
        Debug.Log(other.name);
        
        if (other.tag == "Pilot")
        {

            var Data = other.GetComponent<HealthSystem>();
            CharacterMovement movement = other.GetComponent<CharacterMovement>();
            if (Data != null)
            {
                //넉백을위한 Vector3계산
                impact = -(other.gameObject.transform.position - transform.position).normalized*5;  
                ApplyDamage(Data);
                //넉백             
                movement.AddImpact(impact); 
            }
            else
            {
                Debug.LogError("Component null");
            }
        }
        this.gameObject.SetActive(false);
    }


    public override void ApplyDamage(HealthSystem healthSystem)
    {

        healthSystem.TakeDamage(Damage);
        //Deactivate();
        gameObject.SetActive(false);
    }
 
}

