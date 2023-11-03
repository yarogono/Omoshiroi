using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//인벤토리 정보가 있어야 함.
//클릭 시 현재 오브젝트의 인벤토리 창이 열리도록 해야 함.
//인벤토리 부분은 사실상 플레이어의 내용과 비슷할 듯 하다.

public class FarmingBox : BattleFieldObject, ILootable, IInteractable
{
    //[SerializeField] private 인벤토리클래스 Inventory;

    public event Action OnOpened;
    public event Action OnClosed;

    // Start is called before the first frame update
    private void Start()
    {
        //파밍박스 여는 이벤트.AddListener(() => OnOpened?.Invoke());
        //파밍박스 닫는 이벤트.AddListener(() => OnClosed?.Invoke());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 이 보관함의 인벤토리에서 아이템을 가져갈 때 마다 호출 될 예정.
    /// </summary>
    public void Loot()
    {

    }

    public void Interact()
    {

    }

    public void SetItemList(List<BaseItem> itemList)
    {
        //대충 받아온 아이템 데이터들로 인벤토리를 채운다는 내용.
    }

    public void LootSelectedItem(BaseItem selectedItem)
    {
        //대충 선택한 아이템을 플레이어의 인벤토리로 옮기고 현재 인벤토리에서 제거한다는 내용
    }


}
