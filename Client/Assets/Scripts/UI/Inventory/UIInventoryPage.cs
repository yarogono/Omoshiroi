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
    private MouseFollower mouseFollwer;

    List<UIInventoryItem> listOfUIitems = new List<UIInventoryItem>();

    public Sprite image;
    public int quantity;
    public string title;
    public string description;

    private void Awake()
    {
        Hide();
        mouseFollwer.Toggle(false);
        itemDescription.ResetDescription();
    }
    public void InitializeinventoryUI(int inventorysize)
    {
      
        for (int i = 0; i < inventorysize; i++)
        {
           
            UIInventoryItem uiItem = Instantiate(itemPrefab,Vector3.zero,Quaternion.identity);      
            uiItem.transform.SetParent(contentPanel);
            listOfUIitems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnleftMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem obj)
    {
     
    }

    private void HandleEndDrag(UIInventoryItem obj)
    {
        mouseFollwer.Toggle(false);
    }

    private void HandleSwap(UIInventoryItem obj)
    {
       
    }

    private void HandleBeginDrag(UIInventoryItem obj)
    {
        mouseFollwer.Toggle(true);
        mouseFollwer.SetData(image, quantity);
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        itemDescription.SetDescription(image, title, description);
        listOfUIitems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfUIitems[0].SetData(image, quantity);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        listOfUIitems[0].Deselect();
    }
}
