using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    // 플래이어가 요청할 때 사용
    public override void Initalize(CharacterDataContainer dataContainer, string tag)
    {
        base.Initalize(dataContainer, tag);
        // 추가적으로 해야되는 작업
    }

    // NPC, Clone이 요청할 때 사용
    public override void Initalize(CloneDataContainer dataContainer, string tag)
    {
        base.Initalize(dataContainer, tag);
        // 추가적으로 해야되는 작업
    }

    // NPC에게 부딪혔을 때 사용
    public override void ApplyDamage(CloneDataContainer dataContainer)
    {
        base.ApplyDamage(dataContainer);
        // 추가적으로 해야되는 작업
    }

    // 플래이어에게 부딪혔을 때 사용
    public override void ApplyDamage(CharacterDataContainer dataContainer)
    {
        base.ApplyDamage(dataContainer);
        // dataContainer.Health.ChangeHP(...);
        // 추가적으로 해야되는 작업
    }

    // 플래이어, 벽, 물체
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_makerTag))
        {
            ApplyDamage(other.GetComponent<CharacterDataContainer>());
        }
    }

    // TODO
    // NPC는 어떻게 감지해야되는지 고민
}
