using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContrller : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private InventorySO inventoryData;


    [SerializeField]
    private Button BtnInventory;
    [SerializeField]
    private Button BtnCancel;

    UIInventoryPage _inventorypage;


    private void Start()
    {
        PrepareUI();
        //inventoryData.Initialize();



        BtnInventory.onClick.AddListener(() =>
        {


            if (inventoryUI.isActiveAndEnabled == false)
            {
                //_inventorypage = UIManager.Instance.ShowUI<UIInventoryPage>();  //ui매니져 사용해서 동적으로 껐다키기 
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
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
        
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
      
        inventoryUI.UpdateDescription(itemIndex,item.ItemImage,item.name,item.Description);
    }



}
