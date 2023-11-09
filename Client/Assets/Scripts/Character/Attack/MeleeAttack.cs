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
        Damage = dataContainer.Stats.AtkPower;
    }

    public override void ApplyDamage(HealthSystem health)
    {
        base.ApplyDamage(health);
        health.TakeDamage(Damage);
        gameObject.SetActive(false);
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
            var data = other.GetComponent<DataContainer>();
            ApplyDamage(data.Health);
        }
    }

    // TODO
    // NPC는 어떻게 감지해야되는지 고민
}
