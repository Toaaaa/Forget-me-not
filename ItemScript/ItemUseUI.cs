using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUseUI : MonoBehaviour
{
    public MenuManager menuManager;
    public DisplayInventory displayInventory;
    Item selecteditem;
    TextMeshProUGUI text;

    private void OnDisable()
    {
        displayInventory.isp_SlotOn= false;
        menuManager.isItemUsing = false;
    }


    private void Update()
    {       
        selecteditem = displayInventory.selectedItem ==null ?  null : displayInventory.selectedItem; //displayinventory���� ���õ� �������� ������.
        if (selecteditem.itemType == ItemType.Consumable)//���� ������ �������� ��� ȭ��ǥ ǥ�ð� �ȳ������� �Ұ�.
        {
            ConsumeItem consumeItem = (ConsumeItem)selecteditem;
            if (consumeItem.consumeType == ConsumeItem.ConsumeType.Buff)
                displayInventory.isp_SlotOn = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(selecteditem.itemType == ItemType.Equipment)
                displayInventory.returnItem();

            displayInventory.useItem();
            gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            displayInventory.gameObject.SetActive(true);
        }

        if(selecteditem != null)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = ("use "+ selecteditem.name + " on this character");
        }
    }


}
