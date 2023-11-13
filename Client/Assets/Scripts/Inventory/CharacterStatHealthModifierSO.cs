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
            health.TakeDamage((int)val);
        }
              
    }
}