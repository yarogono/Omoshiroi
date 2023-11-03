using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] CharacterBaseStats cbs;

    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int Def { get; private set; }
    public float AtkSpeed { get; private set; }
    public int AtkPower { get; private set; }
    public int CritRate { get; private set; }
    public float CritPower { get; private set; }
    public float MoveSpeed { get; private set; }
    public float RunMultipiler { get; private set; }

    // Start is called before the first frame update
    private void Start()
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

    public void SetHP(int hp)
    {
        Hp = hp;
    }
}
