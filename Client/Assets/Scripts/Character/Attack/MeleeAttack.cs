using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public override void Initalize(DataContainer dataContainer, string tag)
    {
        base.Initalize(dataContainer, tag);
        // 추가적으로 해야되는 작업
        var Magic = dataContainer.Equipments.GetEquippedItem(eItemType.Magic) as BaseMagic;
    }

    public override void ApplyDamage(HealthSystem health)
    {
        Damage = _data.Stats.AtkPower;
        // 구체적인 대미지 계산을 맡기기
        // Stats에 대미지 계산 메소드를 만든다.
        // Damage = dataContainer.Stats.GetDamage(health);
        // base.ApplyDamage(health);
        health.TakeDamage(Damage);
        // TakeDamage에서 방어력에 따라서 입는 대미지를 줄인다.
        gameObject.SetActive(false);
        // TODO
        // 피격음 재생
    }

    /// <summary>
    /// 플래이어, 벽, 물체
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_makerTag))
        {
            if (other.gameObject.layer == (other.gameObject.layer & AttackManager.Instance.TargetLayer))
            {
                var data = other.GetComponent<DataContainer>();
                ApplyDamage(data.Health);
            }
            // TODO
            // NPC는 어떻게 감지해야되는지 고민
        }
    }
}
