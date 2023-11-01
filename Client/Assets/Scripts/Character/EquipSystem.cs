using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem
{

    private List<BaseItem> equippedItems = new List<BaseItem>();

    [SerializeField] private CharacterDataContainer cdc;

    public void Equip(BaseItem item)
    {
        //동일한 타입의 장비를 장착 중이라면 기존 장비를 해제하고 장착한다.
        if (item is IEquippable equipment)
        {
            for (int i = 0; i < equippedItems.Count; i++)
            {
                if(item.ItemType == equippedItems[i].ItemType)
                {
                    Dequip(equippedItems[i]);
                }
            }

            equippedItems.Add(item);

            equipment.Equip(cdc);
        }
        else { return; }
    }

    public void Dequip(BaseItem item)
    {
        if (item is IEquippable equipment)
        {

            equippedItems.Remove(item);
            equipment.Dequip(cdc);
        }
        else { return; }
    }

    /// <summary>
    /// 장착한 장비 아이템을 제거한다. 캐릭터 사망 시 사용될 것 같음.
    /// </summary>
    public void RemoveEquipments()
    {
        //캐릭터 사망 시 시체 인벤토리에 들어갈 장비류를 처리하는 내용

        //장착한 장비 아이템 모두 제거
        while(equippedItems.Count <= 0)
        {
            Dequip(equippedItems[0]);
        }

    }
}
