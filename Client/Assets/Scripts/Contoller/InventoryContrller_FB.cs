
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    // 인벤토리 ui및 모델 과 통신 두 요소 모두에 종속된다  

    public class InventoryContrller_FB : MonoBehaviour
    {
        [SerializeField]
        private FarmingBox farmingBox;

        [SerializeField]
        private UIInventoryPage_FB inventoryUI;

        [SerializeField]
        private InventorySO inventoryData_player;

        [SerializeField]
        private InventorySO inventoryData_merged;

        [SerializeField]
        private Button BtnCancel;

        private Action OnOpened;
        private Action OnClosed;

        UIInventoryPage_FB _inventorypage_FB;
        PlayerInput playerInput;


        private void Start()
        {
            PrepareUI();
            playerInput = GetComponent<PlayerInput>();
            OnOpened = farmingBox.OnOpened;
            OnClosed = farmingBox.OnClosed;

            BtnCancel.onClick.AddListener(() => CloseInventoryUI());
        }

        /// <summary>
        /// 통합 인벤토리 데이터가 바뀔 때 마다 호출된다. 통합 인벤토리를 비우고, OnInventoryUpdated 이벤트에 UpdateInventoryUI 를 붙인다.
        /// </summary>
        private void PrepareInventoryData()//인벤토리 데이터가 바뀔 때 호출 
        {
            inventoryData_merged.OnInventoryUpdated += UpdateInventoryUI;

            //foreach (InventoryItem item in initialItems)
            //{
            //    if (item.IsEmpty)
            //        continue;
            //    inventoryData.AddItem(item);
            //}
        }

        /// <summary>
        /// 인벤토리 데이터 
        /// </summary>
        /// <param name="inventoryState"></param>
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            //inventoryUI.ResetAllItems();//인벤토리 데이터를 업데이트 할때 한번초기화 
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemIcon,item.Value.quantity);
            }
        }

        /// <summary>
        /// 인벤토리 칸 세팅. 반드시 한 번만 호출되어야 한다
        /// </summary>
        private void PrepareUI()
        {
            inventoryUI.InitializeinventoryUI(inventoryData_player.Size, farmingBox.InventorySize);
            Debug.Log($"player:{inventoryData_player.Size}, farmingBox:{farmingBox.InventorySize}");
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        /// <summary>
        /// 플레이어 인벤토리 + 보관함 인벤토리 형태의 임시 SO 세팅
        /// </summary>
        private void PrepareMergedSO()
        {
            inventoryData_merged.Size = inventoryData_player.Size + farmingBox.InventorySize;
            inventoryData_merged.Initialize();

            for(int i = 0; i < inventoryData_player.Size; i++)
            {
                inventoryData_merged.InsertItem(i, inventoryData_player.GetItemAt(i));
            }

            foreach(KeyValuePair<int, InventoryItem> item in farmingBox.ItemList){
                inventoryData_merged.InsertItem(item.Key, item.Value);
            }
        }

        private void HandleItemActionRequest(int itemIndex)
        {
        }

        /// <summary>
        /// 인덱스를 입력받고, 해당 인덱스의 아이템을 드래그 중인 아이템으로 지정한다.
        /// </summary>
        /// <param name="itemIndex"></param>
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData_merged.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }
                
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemIcon, inventoryItem.quantity);
        }

        /// <summary>
        /// 드래그 시작 지점의 인덱스와 드래그 끝 지점의 인덱스를 입력받아 서로의 아이템 값을 교체한다.
        /// </summary>
        /// <param name="itemIndex_1"></param>
        /// <param name="itemIndex_2"></param>
        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData_merged.SwapItems(itemIndex_1, itemIndex_2);
        }

        /// <summary>
        /// 실질적으로 아이템 설명 출력을 담당하는 부분
        /// </summary>
        /// <param name="itemIndex"></param>
        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData_merged.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            BaseItem item = inventoryItem.item;
            
            if(item.ItemType == eItemType.Weapon)
            {
                inventoryUI.UpdateDescription(itemIndex, item.ItemIcon, $"{item.name}\n{item.Description}", item.Description);
            }
            else
            {
                inventoryUI.UpdateDescription(itemIndex, item.ItemIcon, item.name, item.Description);           
            }
            
        }

        private void OnMouseDown()
        {
            if (/*Vector3.Distance(this.transform.position, 플레이어.transform.position) < 20*/ true)
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
                PrepareInventoryData();
                //playerInput.CanControl = false;
                foreach (var item in inventoryData_player.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemIcon, item.Value.quantity);
                }

                foreach (var item in farmingBox.ItemList)
                {
                    inventoryUI.UpdateData(item.Key + (inventoryData_player.Size - 1), item.Value.item.ItemIcon, item.Value.quantity);
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