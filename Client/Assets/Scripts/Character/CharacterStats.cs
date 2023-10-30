using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : ScriptableObject
{
    [Header("Character's Stat")]
    [SerializeField] private int baseHP;
    [SerializeField] private int baseDEF;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int attackPoint;
    [SerializeField] private int criticalRate;
    [SerializeField] private float criticalPower;


    public int BaseHP { get { return baseHP; } }
    public int BaseDEF { get { return baseDEF; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public int AttackPoint { get { return attackPoint; } }
    public int CriticalRate { get { return criticalRate; } }
    public float CriticalPower { get { return criticalPower; } }
}
