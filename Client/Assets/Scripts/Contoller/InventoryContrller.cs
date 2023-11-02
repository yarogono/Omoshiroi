
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{

    // 인벤토리 ui및 모델 과 통신 두요소모두에 종속된다  

    public class InventoryContrller : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();
        [SerializeField]
        private Button BtnInventory;
        [SerializeField]
        private Button BtnCancel;

        UIInventoryPage _inventorypage;


        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();

            BtnInventory.onClick.AddListener(() =>
            {


                if (inventoryUI.isActiveAndEnabled == false)
                {
                    //_inventorypage = UIManager.Instance.ShowUI<UIInventoryPage>();  //ui매니져 사용해서 동적으로 껐다키기 
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemIcon, item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            });

            BtnCancel.onClick.AddListener(() =>
            {
                inventoryUI.Hide();
            });
        }

        private void PrepareInventoryData()//인베토리 데이터가 바뀔때 호출 
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;

            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();//인벤토리데이터 업데이트 할때 한번초기화 
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemIcon,item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeinventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }
                
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemIcon, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            BaseWeapon item = inventoryItem.item;
            
            if(item.ItemType == eItemType.Weapon)
            {
                inventoryUI.UpdateDescription(itemIndex, item.ItemIcon, $"{item.name}\n{item.Description}",
                $"AP:{item.WeaponAP}\nAS:{item.WeaponAS}\nCP:{item.WeaponCP}\nCR:{item.WeaponCR}\nCR:{item.WeaponDEF}\nHP:{item.WeaponHP}\n");
            }
            else
            {
                inventoryUI.UpdateDescription(itemIndex, item.ItemIcon, item.name, item.Description);           
            }
            
        }

    

    }
}