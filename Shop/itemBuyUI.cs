using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class itemBuyUI : MonoBehaviour
{
    public ShopDisplay displayShop;
    Item selecteditem;
    public TextMeshProUGUI text;
    int amountSelect;
    int totalPrice;
    int maxAmount;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI TotalGold;

    private void OnEnable()
    {
        amountSelect = 1;
        selecteditem = displayShop.selectedItem == null ? null : displayShop.selectedItem; //displayinventory���� ���õ� �������� ������.
        amountText.text = amountSelect.ToString(); //�� ������ ���� ǥ��
        totalPrice = displayShop.GetItemCost(displayShop.selectedItem, amountSelect);
        TotalGold.text = totalPrice.ToString(); //�� ���� ǥ��
        if (selecteditem != null)
        {
            maxAmount = displayShop.container[displayShop.shopNumber].amount;
            text.text = ("buy " + selecteditem.name);
        }
    }

    private void Update()
    {
        selecteditem = displayShop.selectedItem == null ? null : displayShop.selectedItem; //displayinventory���� ���õ� �������� ������.
        amountText.text = amountSelect.ToString(); //�� ������ ���� ǥ��
        totalPrice = displayShop.GetItemCost(displayShop.selectedItem, amountSelect);
        TotalGold.text = totalPrice.ToString(); //�� ���� ǥ��

        if (Input.GetKeyDown(KeyCode.Space))//�̰Ÿ� ���� �������� �� ���� �ڿ� �����ϴ� �������// (â�߸� �⺻ 1������ �ؼ� ~~ �ִ밹������ + ���ͷ� ����)
        {
            if(selecteditem != null)
                displayShop.buyItem(displayShop.selectedItem,amountSelect);

            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            int temp = displayShop.shopNumber;
            gameObject.SetActive(false);
            displayShop.gameObject.SetActive(true);
            displayShop.shopNumber = temp; //shopNumber�� �����ǵ��� �ӽ������� ���� �ٽ� �־���.
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (amountSelect < maxAmount && displayShop.CheckEnoughGold(amountSelect+1))
            {
                amountSelect++;
            }
            else
            {
                return;
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
            text.text = ("buy "+selecteditem.name);
        }
    }
}
