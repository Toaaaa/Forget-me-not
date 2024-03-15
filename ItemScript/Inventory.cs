using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InvenSlot> Container = new List<InvenSlot>();
    public void AddItem(Item _item, int _amount)
    {
        bool hasItem = false;
        for(int i=0; i<Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InvenSlot(_item, _amount));
        }

    }
}

[System.Serializable]
public class InvenSlot
{
    public Item item;
    public int amount;
    public InvenSlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    
    public void AddAmount(int value)
    {
        amount += value;
    }
}