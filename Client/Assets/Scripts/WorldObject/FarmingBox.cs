using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Inventory;

//인벤토리 정보는 InventoryController 를 가짐.
//BtnCancel 은 플레이어 인벤토리 UI 의 창 닫기 버튼으로 지정한다.
//클릭 시 현재 오브젝트의 인벤토리 창이 열리도록 해야 함.
//인벤토리 부분은 사실상 플레이어의 내용과 비슷할 듯 하다.

public class FarmingBox : BattleFieldObject, ILootable, IInteractable
{
    [SerializeField] private int inventorySize;
    
    private InventoryController_FB inventoryController;
    private Collider farminBoxCollider;

    public Dictionary<int, InventoryItem> ItemList { get; private set; }
    public int InventorySize { get { return inventorySize; } private set { inventorySize = value; } }

    public Action OnOpened;
    public Action OnClosed;

    // Start is called before the first frame update
    private void Start()
    {
        ItemList = new Dictionary<int, InventoryItem>();

        inventoryController = GetComponent<InventoryController_FB>();
        farminBoxCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() { }

    /// <summary>
    /// 이 보관함의 인벤토리에서 아이템을 가져갈 때 마다 호출 될 예정.
    /// </summary>
    public void Loot() { }

    public void Interact() { }

    public void SetItemList(List<BaseItem> itemList)
    {
        //대충 받아온 아이템 데이터들로 인벤토리를 채운다는 내용.
    }

    public void SetItemList(Dictionary<int, InventoryItem> items)
    {
        ItemList = items;
    }
}
