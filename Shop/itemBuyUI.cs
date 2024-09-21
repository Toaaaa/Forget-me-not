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
        selecteditem = displayShop.selectedItem == null ? null : displayShop.selectedItem; //displayinventory에서 선택된 아이템을 가져옴.
        amountText.text = amountSelect.ToString(); //총 구매할 갯수 표시
        totalPrice = displayShop.GetItemCost(displayShop.selectedItem, amountSelect);
        TotalGold.text = totalPrice.ToString(); //총 가격 표시
        if (selecteditem != null)
        {
            maxAmount = displayShop.container[displayShop.shopNumber].amount;
            text.text = ("buy " + selecteditem.name);
        }
    }

    private void Update()
    {
        selecteditem = displayShop.selectedItem == null ? null : displayShop.selectedItem; //displayinventory에서 선택된 아이템을 가져옴.
        amountText.text = amountSelect.ToString(); //총 구매할 갯수 표시
        totalPrice = displayShop.GetItemCost(displayShop.selectedItem, amountSelect);
        TotalGold.text = totalPrice.ToString(); //총 가격 표시

        if (Input.GetKeyDown(KeyCode.Space))//이거를 이제 갯수까지 다 정한 뒤에 구매하는 방식으로// (창뜨면 기본 1개부터 해서 ~~ 최대갯수까지 + 엔터로 구매)
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
            displayShop.shopNumber = temp; //shopNumber가 유지되도록 임시저장한 값을 다시 넣어줌.
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
        }//구매할 아이템 갯수 조절


        if (selecteditem != null)
        {
            maxAmount = displayShop.container[displayShop.shopNumber].amount;
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = ("buy "+selecteditem.name);
        }
    }
}
