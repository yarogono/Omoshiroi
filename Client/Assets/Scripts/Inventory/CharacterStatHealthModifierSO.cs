using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
       CharacterStats Stat = character.GetComponent<DataContainer>().Stats;
       DataContainer data = character.GetComponent<DataContainer>();

        if (Stat != null)
        {
            character.GetComponent<MonoBehaviour>().StartCoroutine(RecoverHealthOverTime(data, val, 10f));
        }
        else
        {
            Debug.LogWarning("HealthSystem component not found on the character");
        }

    }

    private IEnumerator RecoverHealthOverTime(DataContainer health, float totalRecovery, float duration)
    {
        float elapsed = 0f;
        float recoveryRate = totalRecovery / duration;

        while (elapsed < duration)
        {
            health.Health.TakeRecovery((int)(recoveryRate * Time.deltaTime));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}