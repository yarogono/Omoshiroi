using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AttackInfo
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField][field: Range(0, 10)] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)][Tooltip("공격 중 공격 입력을 받지 않는 시간")] public float ComboTransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)][Tooltip("공격 중 공격으로 움직이는 시간")] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(0.0f, 10f)][Tooltip("공격 중 공격으로 움직이는 힘")] public float Force { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
}


[Serializable]
public class AttackData
{
    [field: SerializeField] public List<AttackInfo> AttackInfos { get; private set; }
    public int GetAttackInfoCount() { return AttackInfos.Count; }
    public AttackInfo GetAttackInfo(int index) { return AttackInfos[index]; }

}