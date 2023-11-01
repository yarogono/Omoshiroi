using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseResourceSO", menuName = "Item/BaseResourceSO")]
public class BaseResource : BaseItem, IDroppable
{
    [SerializeField] private int maxStack;

    public int MaxStack { get; }

    public void Drop()
    {

    }
    public void StackItem()
    {

    }
}
