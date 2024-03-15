using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Inventory/Items/Equipment")]
public class EquipItem : Item
{
    enum EquipType
    {
        Weapon,
        Accessory
    }

    public float atk;
    public float def;
    public float hp;
    public float mp;
    public float spd;

    private void Awake()
    {
        itemType = ItemType.Equipment;
    }
}
