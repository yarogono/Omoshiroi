using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInput : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    public event Action<Vector3> OnRunEvent;
    public event Action<Vector3> OnDodgeEvent;
    public event Action<Vector3> OnAimEvent;
    public event Action<Vector3> OnAttackEvent;
    public event Action OpenInventory;
    public event Action CloseInventory;
    
    public void CallMoveEvent(Vector3 input)
    {
        OnMoveEvent?.Invoke(input);
    }

    public void CallRunEvent(Vector3 input)
    {
        OnRunEvent?.Invoke(input);
    }

    public void CallDodgeEvent(Vector3 input)
    {
        OnDodgeEvent?.Invoke(input);
    }

    public void CallAimEvent(Vector3 input)
    {
        OnAimEvent?.Invoke(input);
    }

    public void CallAttackEvent(Vector3 input)
    {
        OnAttackEvent?.Invoke(input);
    }

    public void CallOpenInven()
    {
        OpenInventory?.Invoke();
    }

    public void CallCloseInven()
    {
        CloseInventory?.Invoke();
    }
}
