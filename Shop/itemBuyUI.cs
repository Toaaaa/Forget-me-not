using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class itemBuyUI : MonoBehaviour
{
    public ShopDisplay displayShop;
    Item selecteditem;
    TextMeshProUGUI text;

    private void Update()
    {
        selecteditem = displayShop.selectedItem == null ? null : displayShop.selectedItem; //displayinventory���� ���õ� �������� ������.
        if (Input.GetKeyDown(KeyCode.Return))
        {
            displayShop.buyItem();
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            displayShop.gameObject.SetActive(true);
        }

        if (selecteditem != null)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = ("use " + selecteditem.name + " on this character");
        }
    }
}
