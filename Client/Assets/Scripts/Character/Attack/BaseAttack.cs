using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseAttack : MonoBehaviour
{
    protected string _makerTag;
    public int Damage { get; private set; }

    /// <summary>
    /// 플래이어가 요청할 때 사용
    /// </summary>
    /// <param name="dataContainer"></param>
    /// <param name="tag"></param>
    public virtual void Initalize(CharacterDataContainer dataContainer, string tag)
    {
        _makerTag = tag;
        // Damage = ....
    }

    /// <summary>
    /// NPC, Clone이 요청할 때 사용
    /// </summary>
    /// <param name="dataContainer"></param>
    /// <param name="tag"></param>
    public virtual void Initalize(CloneDataContainer dataContainer, string tag)
    {
        _makerTag = tag;
        // Damage = ....
    }

    /// <summary>
    /// NPC에게 부딪혔을 때 사용
    /// </summary>
    /// <param name="dataContainer"></param>
    public virtual void ApplyDamage(CloneDataContainer dataContainer)
    {
        
    }

    /// <summary>
    /// 플래이어에게 부딪혔을 때 사용
    /// </summary>
    /// <param name="dataContainer"></param>
    public virtual void ApplyDamage(CharacterDataContainer dataContainer)
    {

    }
}
