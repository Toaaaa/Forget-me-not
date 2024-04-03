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
                        TempText += equipItem.options[i].ToString() + " "; //���� ���������� �����Ұ�.
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
            //option�� itemtype�� ���� �ٸ��� ǥ���Ұ�. ��� �������� ���. �����ϴ� �ֿ� ������, �Һ�������� ��� hp,mp,�������� ����������. ��Ÿ�������� ��� ����x.
            //itemInfos[2].GetComponentInChildren<TextMeshProUGUI>().text = optionText;
            itemInfos[2].GetComponentInChildren<TextMeshProUGUI>().text = "Price:   "+itemPrice.ToString()+" Gold";
            itemInfos[3].GetComponentInChildren<TextMeshProUGUI>().text = selectedItem.itemDescription;

        }
        else //�ش� �κ��� �������� ������.
        {
            itemInfos[0].GetComponent<Image>().sprite = null; /////////////���� null�� �ƴ� �������� ���°��� �����ִ� �̹����� �����Ұ�.
            itemInfos[1].GetComponentInChildren<TextMeshProUGUI>().text = "";
            itemInfos[2].GetComponentInChildren<TextMeshProUGUI>().text = "";
            itemInfos[3].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        //selectedItem.itemType
    }
}
