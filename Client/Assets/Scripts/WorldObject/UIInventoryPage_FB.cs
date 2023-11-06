using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;


    public class UIInventoryPage_FB : UIBase
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private RectTransform contentPanel_FB;

        [SerializeField]
        private UIInventoryDiscription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentlyDraggedItemIndex = -1; // 드래그해서 놓을때  어떤 인덱스와 바꿔줘야할지 알기위해  하나의 개인변수에 저장 


        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

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
                UIInventoryItem uiItem_player = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                UIInventoryItem uiItem_fb = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem_player.transform.SetParent(contentPanel);
                uiItem_fb.transform.SetParent(contentPanel_FB);

                listOfUIItems.Add(uiItem_player);
                uiItem_player.OnItemClicked += HandleItemSelection;
                uiItem_player.OnItemBeginDrag += HandleBeginDrag;
                uiItem_player.OnItemDroppedOn += HandleSwap;
                uiItem_player.OnItemEndDrag += HandleEndDrag;
                uiItem_player.OnleftMouseBtnClick += HandleShowItemActions;

                listOfUIItems.Add(uiItem_fb);
                uiItem_fb.OnItemClicked += HandleItemSelection;
                uiItem_fb.OnItemBeginDrag += HandleBeginDrag;
                uiItem_fb.OnItemDroppedOn += HandleSwap;
                uiItem_fb.OnItemEndDrag += HandleEndDrag;
                uiItem_fb.OnleftMouseBtnClick += HandleShowItemActions;
        }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            ResetDraggtedItem();
            return;
        }
        OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggtedItem();
        }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
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
            HandleItemSelection(inventoryItemUI);

        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
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

        }
    }
