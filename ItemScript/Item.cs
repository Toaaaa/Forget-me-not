using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using Unity.VisualScripting;

public enum ItemType
{
    Equipment, //���
    Consumable, //�Һ�    
    Default //��Ÿ
}
public enum EquipType
{
    Weapon,
    Accessory
}

[System.Serializable]   
public abstract class Item : ScriptableObject
{
    public GameObject prefab;
    public GameObject combatPrefab; //������ ����� ������ �ڽ�.
    public Sprite sprite;

    public int itemID;
    [TextArea(5, 15)]
    public string itemDescription;
    public ItemType itemType;
    public EquipType equipType;
    //public EquipItem.EquipType equipType;
    //public EquipItem.OptionType optionType;
    //public ConsumeItem.ConsumeType consumeType;




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
