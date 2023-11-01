using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] CharacterBaseStats cbs;


    public int maxHp;
    public int hp;
    public int def;
    public float atkSpeed;
    public int atkPower;
    public int critRate;
    public float critPower;

    // Start is called before the first frame update
    private void Start()
    {
        maxHp = cbs.BaseHP; hp = maxHp;
        def = cbs.BaseDEF;
        atkSpeed = cbs.BaseAttackSpeed;
        atkPower = cbs.BaseAttackPower;
        critRate = cbs.BaseCriticalRate;
        critPower = cbs.BaseCriticalPower;
    }

}
