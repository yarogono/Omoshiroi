using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseStats : ScriptableObject
{
    [Header("Character's base stat")]
    [SerializeField] private int baseHP;
    [SerializeField] private int baseDEF;
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private int baseAttackPower;
    [SerializeField] private int baseCriticalRate;
    [SerializeField] private float baseCriticalPower;

    public int BaseHP { get; }
    public int BaseDEF { get; }
    public float BaseAttackSpeed { get; }
    public int BaseAttackPower { get; }
    public int BaseCriticalRate { get; }
    public float BaseCriticalPower { get; }
}
