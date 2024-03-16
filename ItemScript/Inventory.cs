using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public DBManager database;
    public List<InvenSlot> Container = new List<InvenSlot>();
    public void AddItem(Item _item, int _amount)
    {
        for(int i=0; i<Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InvenSlot(database.GetId[_item],_item, _amount));

    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < Container.Count; i++)
            Container[i].item = database.GetItem[Container[i].ID];
    }

    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class InvenSlot
{
    public int ID;
    public Item item;
    public int amount;
    public InvenSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    
    public void AddAmount(int value)
    {
        amount += value;
    }
}