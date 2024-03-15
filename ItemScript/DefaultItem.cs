using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory/Items/Default")]
public class DefaultItem : Item
{
    

    private void Awake()
    {
        itemType = ItemType.Default;
    }
}
