using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using UnityEditor;
using Unity.VisualScripting;
using static EquipItem;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath; ///Inventory.Save �� savePath 
    private DBManager database;
    public int goldHave;
    public List<InvenSlot> Container = new List<InvenSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (DBManager)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObject/DB_manager.asset", typeof(DBManager));
        //#else ���� ����ÿ��� resource.load�� �ƴ϶� addressable asset system�� �������. <<
#endif
    }
    
    public void AddItem(Item _item, int _amount, int itemtype) 
    {
        for(int i=0; i<Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InvenSlot(database.GetId[_item],_item, _amount, itemtype));
        
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close(); //to prevent memory leak
        }
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID];
            //if (Container[i].item == null)
            //{
            //    Container[i].item = database.GetItem[Container[i].ID]; //�̰ɷ� �ϸ� ������ �Ȼ������ �κ��� �������� �Ｎ���� �߰��Ҷ� ������ ������.. �ϴ��� �ӽ÷� ����
            //}
        }
    }

    public void OnBeforeSerialize()
    {
    }

    /*
     * if (Input.GetKeyDown(??)) �̿� ���� ������� ������ �߰� ����.
        {
            inventory.AddItem(database.GetItem[0], 1, 1);
        }
    */

   
}

[System.Serializable]
public class InvenSlot //�����̳ʿ� ����� ����
{
    public int ID;
    public Item item;
    public int amount;
    public int _itemType; // 0: weapon+acc  1:consumable 2:other
    public bool isAcc;

    public InvenSlot(int _id, Item _item, int _amount, int itemtype)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        _itemType = itemtype;
        if(_itemType == 0) //�κ��� ���� itemtype�� equip �϶� �ش� �������� �Ǽ����� ������ Ȯ��, isAcc���� ���� ���.
        {
            EquipType equipType = (EquipType)item.itemType;
            if(equipType == EquipType.Accessory)
            {
                isAcc = true;
            }
            else
            {
                isAcc = false;
            }
        }
    }
    
    public void AddAmount(int value)
    {
        amount += value;
    }
}