using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]   
public class Item : MonoBehaviour
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {
        Consumable, //�Һ�    
        Equipment, //���
        Quest, //����Ʈ
        Acc,       //�Ǽ�
        Default //��Ÿ
    }

    public Item(int _itemID, string _itemName, string _itemDescription, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDescription;
        itemCount = _itemCount;
        itemType = _itemType;
        itemIcon = Resources.Load<Sprite>("ItemIcons/" + _itemName);
    }


}
