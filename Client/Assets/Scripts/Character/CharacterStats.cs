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

    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int Hp 
    { 
        get { return hp; } 
        set 
        {
            if (value > MaxHp) { hp = MaxHp; return; }
            hp = value;
        } 
    }
    public int Def { get { return maxHp; } set { maxHp = value; } }
    public float AtkSpeed { get { return atkSpeed; } set { atkSpeed = value; } }
    public int AtkPower { get { return atkPower; } set { atkPower = value; } }
    public int CritRate { get { return critRate; } set { critRate = value; } }
    public float CritPower { get { return critPower; } set { critPower = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float RunMultipiler { get { return runMultipiler; } set { runMultipiler = value; } }
    public float DodgeTime { get { return dodgeTime; } set { dodgeTime = value; } }

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
