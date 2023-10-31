using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContrller : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    public int inventorySize = 10;
    [SerializeField]
    private Button BtnInventory;
    [SerializeField]
    private Button BtnCancel;

    UIInventoryPage _inventorypage;

    private void Awake()
    {
        inventoryUI.InitializeinventoryUI(inventorySize);
    }

    private void Start()
    {

        //inventoryUI.InitializeinventoryUI(inventorySize);


        BtnInventory.onClick.AddListener(() =>
        {
           if(inventoryUI.isActiveAndEnabled==false)
            {
                _inventorypage = UIManager.Instance.ShowUI<UIInventoryPage>();
            }
           else
            {
                UIManager.Instance.GetOpenUI<UIInventoryPage>().HideUI();
            }
        });

        BtnCancel.onClick.AddListener(() =>
        {
            UIManager.Instance.GetOpenUI<UIInventoryPage>().HideUI();
        });
    }





}
