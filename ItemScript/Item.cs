using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable, //소비    
    Equipment, //장비
    Quest, //퀘스트
    Default //기타
}

[System.Serializable]   
public abstract class Item : ScriptableObject
{
    public GameObject prefab;

    public int itemID;
    public string itemName;
    [TextArea(5, 15)]
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;

    

    /*public Item(int _itemID, string _itemName, string _itemDescription, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDescription;
        itemCount = _itemCount;
        itemType = _itemType;
        itemIcon = Resources.Load<Sprite>("ItemIcons/" + _itemName);
    }*/


}
