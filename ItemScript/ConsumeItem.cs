using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consume Item", menuName = "Inventory/Items/Consume")]
public class ConsumeItem : Item
{
    public int restoreAmount;
    enum ConsumeType
    {
        Health,
        Mana,
        Stamina
    }

    private void Awake()
    {
        itemType = ItemType.Consumable;
    }
}
