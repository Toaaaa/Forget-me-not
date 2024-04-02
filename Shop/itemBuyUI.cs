using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class itemBuyUI : MonoBehaviour
{
    public ShopDisplay displayShop;
    Item selecteditem;
    TextMeshProUGUI text;
    int amountSelect;
    int maxAmount;

    private void OnEnable()
    {
        amountSelect = 1;
    }

    private void Update()
    {

        selecteditem = displayShop.selectedItem == null ? null : displayShop.selectedItem; //displayinventory���� ���õ� �������� ������.
        if (Input.GetKeyDown(KeyCode.Return))//�̰Ÿ� ���� �������� �� ���� �ڿ� �����ϴ� �������// (â�߸� �⺻ 1������ �ؼ� ~~ �ִ밹������ + ���ͷ� ����)
        {
            if(selecteditem != null)
                displayShop.buyItem(displayShop.selectedItem,amountSelect);

            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int temp = displayShop.shopNumber;
            gameObject.SetActive(false);
            displayShop.gameObject.SetActive(true);
            displayShop.shopNumber = temp; //shopNumber�� �����ǵ��� �ӽ������� ���� �ٽ� �־���.
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (amountSelect < maxAmount)
            {
                amountSelect++;
            }
            else
            {
                amountSelect = maxAmount;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (amountSelect > 1)
            {
                amountSelect--;
            }
            else
            {
                amountSelect = 1;
            }
        }//������ ������ ���� ����


        if (selecteditem != null)
        {
            maxAmount = displayShop.container[displayShop.shopNumber].amount;
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = ("buy "+amountSelect+" of "+selecteditem.name);
        }
    }
}
