using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    // 인벤토리 ui및 모델 과 통신 두 요소 모두에 종속된다

    public class InventoryController_FB : MonoBehaviour
    {
        [SerializeField]
        private FarmingBox farmingBox;

        [SerializeField]
        private UIInventoryPage_FB inventoryUI;

        [SerializeField]
        private InventorySO inventoryData_player;

        [SerializeField]
        private Button BtnCancel;

        private Action OnOpened;
        private Action OnClosed;

        private InventorySO inventoryData_merged;

        UIInventoryPage_FB _inventorypage_FB;
        PlayerInput playerInput;

        private void Start()
        {
            inventoryData_merged = new InventorySO();

            playerInput = GetComponent<PlayerInput>();
            OnOpened = farmingBox.OnOpened;
            OnClosed = farmingBox.OnClosed;

            BtnCancel.onClick.AddListener(() => CloseInventoryUI());
        }

        private void PrepareInventoryData() //인벤토리 데이터가 바뀔 때 호출
        {
            inventoryData_merged.Initialize();
            inventoryData_merged.OnInventoryUpdated += UpdateInventoryUI;

            //foreach (InventoryItem item in initialItems)
            //{
            //    if (item.IsEmpty)
            //        continue;
            //    inventoryData.AddItem(item);
            //}
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            //inventoryUI.ResetAllItems();//인벤토리 데이터를 업데이트 할때 한번초기화
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemIcon, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeinventoryUI(inventoryData_player.Size, farmingBox.InventorySize);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void PrepareMergedSO()
        {
            inventoryData_merged.Size = inventoryData_player.Size + farmingBox.InventorySize;
            inventoryData_merged.Initialize();
        }

        private void HandleItemActionRequest(int itemIndex) { }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData_merged.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }

            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemIcon, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData_merged.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData_merged.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            BaseItem item = inventoryItem.item;

            if (item.ItemType == eItemType.Weapon)
            {
                inventoryUI.UpdateDescription(
                    itemIndex,
                    item.ItemIcon,
                    $"{item.name}\n{item.Description}",
                    item.Description
                );
            }
            else
            {
                inventoryUI.UpdateDescription(
                    itemIndex,
                    item.ItemIcon,
                    item.name,
                    item.Description
                );
            }
        }

        private void OnMouseDown()
        {
            //플레이어 캐릭터와 일정 거리 이하라면 플레이어의 인벤토리와 보관함 인벤토리를 동시에 연다.
            if ( /*Vector3.Distance(this.transform.position, 플레이어.transform.position) < 20*/
                true)
            {
                OpenInventoryUI();
            }
        }

        /// <summary>
        /// 플레이어 인벤토리 정보에 보관함 인벤토리 정보를 덧붙여서 관리하도록 해야 한다.
        /// </summary>
        private void OpenInventoryUI()
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                PrepareMergedSO();
                PrepareUI();
                PrepareInventoryData();
                //playerInput.CanControl = false;
                foreach (var item in inventoryData_player.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemIcon, item.Value.quantity);
                }

                foreach (var item in farmingBox.ItemList)
                {
                    inventoryUI.UpdateData(
                        item.Key + (inventoryData_player.Size - 1),
                        item.Value.item.ItemIcon,
                        item.Value.quantity
                    );
                }

                inventoryUI.Show();
                OnOpened?.Invoke();
            }
        }

        /// <summary>
        /// 아이템 리스트에서 플레이어 인벤토리 구간과 보관함 인벤토리 구간을 구분하여, 서로 원상복구 해 주어야 한다.
        /// </summary>
        private void CloseInventoryUI()
        {
            if (inventoryUI.isActiveAndEnabled == true)
            {
                inventoryUI.Hide();

                OnClosed?.Invoke();
                //playerInput.CanControl = true;
            }
        }
    }
}
