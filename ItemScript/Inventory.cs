using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath; ///Inventory.Save 가 savePath 
    private DBManager database;
    public List<InvenSlot> Container = new List<InvenSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (DBManager)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObject/DB_manager.asset", typeof(DBManager));
        //#else 추후 빌드시에는 resource.load가 아니라 addressable asset system을 사용하자. <<
#endif
    }
    
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