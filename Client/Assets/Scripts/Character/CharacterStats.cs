using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    private int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    private int maxHp;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    private int hp;
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

    private int def;
    public int Def
    {
        get { return def; }
        set { def = value; }
    }

    private float atkSpeed;
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }

    private int atkPower;
    public int AtkPower
    {
        get { return atkPower; }
        set { atkPower = value; }
    }

    private int critRate;
    public int CritRate
    {
        get { return critRate; }
        set { critRate = value; }
    }

    private float critPower;
    public float CritPower
    {
        get { return critPower; }
        set { critPower = value; }
    }

    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    private float runMultipiler;
    public float RunMultipiler
    {
        get { return runMultipiler; }
        set { runMultipiler = value; }
    }

    private float dodgeTime;
    public float DodgeTime
    {
        get { return dodgeTime; }
        set { dodgeTime = value; }
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
