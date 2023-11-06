using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;


    public class UIInventoryPage_FB : UIBase
    {
        [SerializeField]
        private UIInventoryItem itemPrefab_player;

        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel_player;

        [SerializeField]
        private RectTransform contentPanel_fb;

        [SerializeField]
        private UIInventoryDiscription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        //플레이어 인벤토리와 보관함 인벤토리를 하나로 합친 형태.
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

        public void InitializeinventoryUI(int inventorysize_player, int inventorysize_fb)
        {
            int listSize = inventorysize_player + inventorysize_fb;

            UIInventoryItem uiItem;
            for (int i = 0; i < listSize; i++)
            {
                //플레이어 인벤토리에 해당하는 인덱스라면
                if(i < inventorysize_player)
                {
                    uiItem = Instantiate(itemPrefab_player, Vector3.zero, Quaternion.identity);
                    uiItem.transform.SetParent(contentPanel_player);

                }
                //보관함 인벤토리에 해당하는 인덱스라면
                else{
                    uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                    uiItem.transform.SetParent(contentPanel_fb);
                }

                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnleftMouseBtnClick += HandleShowItemActions;

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

    /// <summary>
    /// 드래그가 끝났을 때, 드래그 시작 위치와 끝 위치의 인덱스에 있는 아이템 값을 스왑한다.
    /// 
    /// 끝 위치가 시작 위치에 해당하는 인벤토리를 벗어났다면, 반대편 인벤토리인지 확인하도록 해야 한다.
    /// 그리고 반대편 인벤토리라면 해당 인덱스 값을 가져와 바꾸도록 해야 한다.
    /// </summary>
    /// <param name="inventoryItemUI"> 플레이어 인벤토리 UI 또는 보관함 인벤토리 UI 를 입력</param>
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
            Debug.Log(sprite.name);
        }

        /// <summary>
        /// 여길 뜯어고쳐야 할 듯 하다. 
        /// </summary>
        /// <param name="inventoryItemUI"></param>
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
