using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBaseStatsSO", menuName = "Character/CharacterBaseStatsSO")]
public class CharacterBaseStats : ScriptableObject
{
    [Header("Character's base stat")]
    [SerializeField] [Tooltip("기본 체력")] private int baseHP;
    [SerializeField] [Tooltip("기본 방어력")] private int baseDEF;
    [SerializeField] [Tooltip("기본 공격 속도")] private float baseAttackSpeed;
    [SerializeField] [Tooltip("기본 공격력")] private int baseAttackPower;
    [SerializeField] [Tooltip("기본 크리티컬 확률")] private int baseCriticalRate;
    [SerializeField] [Tooltip("기본 크리티컬 시 피해량 배율")] private float baseCriticalPower;
    [SerializeField] [Tooltip("기본 이동 속도")] private float baseMoveSpeed;
    [SerializeField] [Tooltip("달리기 시 기본 이동속도 배율")] private float baseRunMultiplier;

    public int BaseHP { get; }
    public int BaseDEF { get; }
    public float BaseAttackSpeed { get; }
    public int BaseAttackPower { get; }
    public int BaseCriticalRate { get; }
    public float BaseCriticalPower { get; }
    public float BaseMoveSpeed { get; }
    public float BaseRunMultiplier { get; }
}
