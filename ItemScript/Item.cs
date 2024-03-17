using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using Unity.VisualScripting;

public enum ItemType
{
    Consumable, //�Һ�    
    Equipment, //���
    Default //��Ÿ
}

[System.Serializable]   
public abstract class Item : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;

    public int itemID;
    [TextArea(5, 15)]
    public string itemDescription;
    public ItemType itemType;




}

[System.Serializable]
public class ItemInfo
{
    public string Name;
    public int Id;
    public ItemType Itemtype;

    public ItemInfo(Item item)
    {
        Name = item.name;
        Id = item.itemID;
        Itemtype = item.itemType;
        Debug.Log("ItemInfo ������ ȣ��");
    }
    
}
