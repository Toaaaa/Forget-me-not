using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUseUI : MonoBehaviour
{
    public DisplayInventory displayInventory;
    Item selecteditem;
    TextMeshProUGUI text;

    private void OnDisable()
    {
        displayInventory.isp_SlotOn= false;
    }

    private void Update()
    {
        selecteditem = displayInventory.selectedItem ==null ?  null : displayInventory.selectedItem; //displayinventory에서 선택된 아이템을 가져옴.
        if(Input.GetKeyDown(KeyCode.Return))
        {
            displayInventory.returnItem();
            displayInventory.useItem();
            gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }

        if(selecteditem != null)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = ("use "+ selecteditem.name + " on this character");
        }
    }


}
