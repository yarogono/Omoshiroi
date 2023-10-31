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

    public int BaseHP { get { return baseHP; } set { baseHP = value; } }
    public int BaseDEF { get { return baseDEF; } set { baseDEF = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public int AttackPoint { get { return attackPoint; } set { attackPoint = value; } }
    public int CriticalRate { get { return criticalRate; } set { criticalRate = value; } }
    public float CriticalPower { get { return criticalPower; } set { criticalPower = value; } }
}
