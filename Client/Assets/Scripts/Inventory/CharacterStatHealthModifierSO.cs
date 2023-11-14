using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        HealthSystem health = character.GetComponent<HealthSystem>();
        if (health != null)
        {
            character.GetComponent<MonoBehaviour>().StartCoroutine(RecoverHealthOverTime(health, val, 3f));
        }
    }

    private IEnumerator RecoverHealthOverTime(HealthSystem health, float totalRecovery, float duration)
    {
        float elapsed = 0f;
        float recoveryRate = totalRecovery / duration; // 전체 회복량을 시간으로 나눠서 회복률 계산

        while (elapsed < duration)
        {
            health.TakeRecovery((int)(recoveryRate * Time.deltaTime));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}