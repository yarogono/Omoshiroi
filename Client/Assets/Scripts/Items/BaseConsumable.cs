using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseConsumableSO", menuName = "Item/BaseConsumableSO")]
public class BaseConsumable : BaseItem, IDroppable, IConsumable, IStackable
{
    [SerializeField] private int maxStack;

    public int MaxStack { get; }

   public void Drop()
    {

    }

    public void Consume()
    {

    }

    public void StackItem()
    {

    }
}
