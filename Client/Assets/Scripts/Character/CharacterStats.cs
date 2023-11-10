using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterStats
{
    [field: SerializeField] public CharacterBaseStats cbs { get; private set; }

    private int maxHp;
    private int hp;
    private int def;
    private float atkSpeed;
    private int atkPower;
    private int critRate;
    private float critPower;
    private float moveSpeed;
    private float runMultipiler;
    private float dodgeTime;

    public int MaxHp { get; set; }
    public int Hp 
    { 
        get { return hp; } 
        set 
        {
            if (value > MaxHp) { hp = MaxHp; return; }
            hp = value;
        } 
    }
    public int Def { get; private set; }
    public float AtkSpeed { get; private set; }
    public int AtkPower { get; private set; }
    public int CritRate { get; private set; }
    public float CritPower { get; private set; }
    public float MoveSpeed { get; private set; }
    public float RunMultipiler { get; private set; }
    public float DodgeTime { get; private set; }

    public void Initialize()
    {
        MaxHp = cbs.BaseHP; Hp = MaxHp;
        Def = cbs.BaseDEF;
        AtkSpeed = cbs.BaseAttackSpeed;
        AtkPower = cbs.BaseAttackPower;
        CritRate = cbs.BaseCriticalRate;
        CritPower = cbs.BaseCriticalPower;
        MoveSpeed = cbs.BaseMoveSpeed;
        RunMultipiler = cbs.BaseRunMultiplier;
    }

    public void SetCharacterStats(int maxHp, int hp, int def, float atkSpeed, int atkPower, int critRate, float critPower, float moveSpeed, float runMulipiler)
    {
        MaxHp = maxHp;
        Hp = hp;
        Def = def;
        AtkSpeed = atkSpeed;
        AtkPower = atkPower;
        CritRate = critRate;
        CritPower = critPower;
        MoveSpeed = moveSpeed;
        RunMultipiler = runMulipiler;
    }
}
