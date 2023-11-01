using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPage : UIBase
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIInventoryDiscription itemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private int currentlyDraggedItemIndex = -1; // 드래그해서 놓을때  어떤 인덱스와 바꿔줘야할지 알기위해  하나의 개인변수에 저장 


    public event Action<int> OnDescriptionRequested,
            OnItemActionRequested,
            OnStartDragging;

    public event Action<int, int> OnSwapItems; // 두아이템 스왑 



    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }
    public void InitializeinventoryUI(int inventorysize)
    {
      
        for (int i = 0; i < inventorysize; i++)
        {
           
            UIInventoryItem uiItem = Instantiate(itemPrefab,Vector3.zero,Quaternion.identity);      
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnleftMouseBtnClick += HandleShowItemActions;
        }
    }

    public void UpdateData(int itemIndex,Sprite itemImage, int itemQuantity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
     
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggtedItem();
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            ResetDraggtedItem();
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);

    }

    private void ResetDraggtedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
           
        
    }
    public void CreateDraggedItem(Sprite sprite, int quantity)//  mouseFollower 호출
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }
    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        ResetSelection();
    }
    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }


    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }

    }


    public void Hide()
    {
        gameObject.SetActive(false);
        listOfUIItems[0].Deselect();
        listOfUIItems[1].Deselect();
    }
}
