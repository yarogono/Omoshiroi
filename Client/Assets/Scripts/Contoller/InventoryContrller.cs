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


    private void Start()
    {

        inventoryUI.InitializeinventoryUI(inventorySize);
    
  


    BtnInventory.onClick.AddListener(() =>
        {

     
            if (inventoryUI.isActiveAndEnabled == false)
            {
                //_inventorypage = UIManager.Instance.ShowUI<UIInventoryPage>();
              inventoryUI.Show();
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





}
