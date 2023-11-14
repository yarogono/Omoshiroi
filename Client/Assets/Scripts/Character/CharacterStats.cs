using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [field: SerializeField]
    public SyncModule SyncModule { get; private set; }

    private int level;
    private int hp;
    private int maxHp;
    private int def;
    private float atkSpeed;
    private int atkPower;
    private int critRate;
    private float critPower;
    private float runMultipiler;

    private float dodgeTime;
    private float moveSpeed;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    public int Hp
    {
        get { return hp; }
        set
        {
            if (value > MaxHp)
            {
                hp = MaxHp;
                return;
            }
            hp = value;
        }
    }
    public int Def
    {
        get { return def; }
        set { def = value; }
    }
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }
    public int AtkPower
    {
        get { return atkPower; }
        set { atkPower = value; }
    }
    public int CritRate
    {
        get { return critRate; }
        set { critRate = value; }
    }
    public float CritPower
    {
        get { return critPower; }
        set { critPower = value; }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public float RunMultipiler
    {
        get { return runMultipiler; }
        set { runMultipiler = value; }
    }
    public float DodgeTime
    {
        get { return dodgeTime; }
        set { dodgeTime = value; }
    }

    public void UpdateStats()
    {
        Level = SyncModule.StatInfo.Level;
        MaxHp = SyncModule.StatInfo.MaxHp;
        Hp = SyncModule.StatInfo.Hp;
        Def = 1;
        AtkSpeed = 1;
        AtkPower = SyncModule.StatInfo.Attack;
        CritRate = 1;
        CritPower = 1;
        MoveSpeed = SyncModule.StatInfo.Speed;
        RunMultipiler = 2;
    }

    public void SetCharacterStats(
        int maxHp,
        int hp,
        int def,
        float atkSpeed,
        int atkPower,
        int critRate,
        float critPower,
        float moveSpeed,
        float runMulipiler
    )
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
