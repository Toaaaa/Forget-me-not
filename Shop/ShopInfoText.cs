using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoText : MonoBehaviour
{
    public GameObject[] itemInfos; //0:sprite, 1: name, 2: option, 3: description
    //public GameObject selectedItem;
    public Item selectedItem;
    EquipItem equipItem;
    ConsumeItem consumeItem;
    //public DefaultItem defaultItem;

    public string optionText;
    public int itemPrice;

    private void Update()
    {


        if (selectedItem != null)
        {
            switch (selectedItem.itemType)
            {
                case ItemType.Equipment:
                    equipItem = (EquipItem)selectedItem;
                    string TempText = new string("");
                    for (int i = 0; i < equipItem.options.Length; i++)
                    {
                        TempText += equipItem.options[i].ToString() + " "; //추후 아이콘으로 변경할것.
                    }
                    optionText = TempText;
                    break;
                case ItemType.Consumable:
                    consumeItem = (ConsumeItem)selectedItem;
                    optionText = consumeItem.consumeType.ToString();
                    break;
                case ItemType.Default:
                    optionText = "";
                    break;
            }

            itemInfos[0].GetComponent<Image>().sprite = selectedItem.sprite;
            itemInfos[1].GetComponentInChildren<TextMeshProUGUI>().text = selectedItem.name;
            //option은 itemtype에 따라 다르게 표시할것. 장비 아이템의 경우. 증가하는 주요 스탯을, 소비아이템의 경우 hp,mp,버프등의 스탯종류를. 기타아이템의 경우 설명x.
            //itemInfos[2].GetComponentInChildren<TextMeshProUGUI>().text = optionText;
            itemInfos[2].GetComponentInChildren<TextMeshProUGUI>().text = "Price:   "+itemPrice.ToString()+" Gold";
            itemInfos[3].GetComponentInChildren<TextMeshProUGUI>().text = selectedItem.itemDescription;

        }
        else //해당 인벤에 아이템이 없을때.
        {
            itemInfos[0].GetComponent<Image>().sprite = null; /////////////추후 null이 아닌 아이템이 없는것을 보여주는 이미지로 변경할것.
            itemInfos[1].GetComponentInChildren<TextMeshProUGUI>().text = "";
            itemInfos[2].GetComponentInChildren<TextMeshProUGUI>().text = "";
            itemInfos[3].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        //selectedItem.itemType
    }
}
