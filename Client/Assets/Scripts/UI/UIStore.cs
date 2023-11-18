using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtContent;

    [SerializeField] private Button btnConfirm;
    [SerializeField] private Button btnCancel;


    private Action OnConfirm;




    public void setUP(string Title , string context , Action Confirm = null)
    {
        txtTitle.text =  Title;
        txtContent.text = context;

        OnConfirm = Confirm;
    }




}
