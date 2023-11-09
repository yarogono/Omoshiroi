using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseAttack : MonoBehaviour
{
    protected string _makerTag;
    public int Damage { get; protected set; }
    protected Action _actAtDisable;
    protected DataContainer _data;

    public void AddActAtDisable(Action action) { _actAtDisable += action; }

    public virtual void Initalize(DataContainer dataContainer, string tag)
    {
        _makerTag = tag;
        _data = dataContainer;
        // Damage = ....
    }

    public virtual void ApplyDamage(DataContainer dataContainer)
    {

    }

    private void OnDisable()
    {
        _actAtDisable?.Invoke();
        _actAtDisable = null;
    }
}
