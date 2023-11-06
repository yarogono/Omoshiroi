using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EquippableItem : BaseItem, IDestroyableItem, IItemAction
{
    public string ActionName => "Equip";

  
    public bool PerformAction(GameObject character)
    {
        throw new System.NotImplementedException();
    }
}
