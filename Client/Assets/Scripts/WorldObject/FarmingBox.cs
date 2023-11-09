using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Inventory;

/// <summary>
/// 테스트가 용이하도록 임의대로 아이템 스폰을 자체적인 SO 로 관리하도록 해 두었음.
/// </summary>
public class FarmingBox : BattleFieldObject, ILootable, IInteractable
{
    [SerializeField] private int inventorySize;
    [SerializeField] private InventorySO inventorySO;

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

        Dictionary<int, InventoryItem> items = inventorySO.GetCurrentInventoryState();

        foreach (KeyValuePair<int, InventoryItem> item in items)
        {
            ItemList.Add(item.Key, item.Value);
        }

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
