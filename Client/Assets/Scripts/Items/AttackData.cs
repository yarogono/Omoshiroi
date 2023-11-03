using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AttackInfo
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(0.0f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
}


[Serializable]
public class AttackData
{
    [field: SerializeField] public List<AttackInfo> AttackInfos { get; private set; }
    public int GetAttackInfoCount() { return AttackInfos.Count; }
    public AttackInfo GetAttackInfo(int index) { return AttackInfos[index]; }

}