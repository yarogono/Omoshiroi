using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseAttack : MonoBehaviour
{
    protected string _makerTag;
    public int Damage { get; protected set; }
    protected Action _actAtDisable;
    protected DataContainer _data;
    protected AttackInfo _attackInfo;

    public void AddActAtDisable(Action action)
    {
        _actAtDisable += action;
    }

    public virtual void Initalize(AttackInfo attackInfo, DataContainer dataContainer, string tag)
    {
        _makerTag = tag;
        _data = dataContainer;
        // Damage = ....
    }

    public virtual void ApplyDamage(HealthSystem healthSystem) { }

    private void OnDisable()
    {
        _actAtDisable?.Invoke();
        _actAtDisable = null;
    }
}
