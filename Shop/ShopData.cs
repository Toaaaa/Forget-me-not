using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using static EquipItem;


[CreateAssetMenu(fileName = "New Shoptlist", menuName = "Inventory/Shoptlist")]
public class ShopData : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath; ///Inventory.Save �� savePath 
    private DBManager database;
    public List<shopSlot> StartStage = new List<shopSlot>();
    public List<shopSlot> fisrtArea = new List<shopSlot>();
    public List<shopSlot> secondArea = new List<shopSlot>();
    public List<shopSlot> thirdArea = new List<shopSlot>();
    public List<shopSlot> lastArea = new List<shopSlot>();
    public List<shopSlot> specialShop1 = new List<shopSlot>();
    public List<shopSlot> specialShop2 = new List<shopSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (DBManager)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObject/DB_manager.asset", typeof(DBManager));
        AfterEnableAndDesrialize();//OnAfterDeserialize >> OnEnable �����̱⿡ �ش� �۾��� OnEnable���� ����.
        //#else ���� ����ÿ��� resource.load�� �ƴ϶� addressable asset system�� �������. <<
#endif
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
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close(); //to prevent memory leak
        }
    }
    private void AfterEnableAndDesrialize()
    {
        for (int i = 0; i < StartStage.Count; i++)
        {
            StartStage[i].item = database.GetItem[StartStage[i].ID];
        }
        for (int i = 0; i < fisrtArea.Count; i++)
        {
            fisrtArea[i].item = database.GetItem[fisrtArea[i].ID];
        }
        for (int i = 0; i < secondArea.Count; i++)
        {
            secondArea[i].item = database.GetItem[secondArea[i].ID];
        }
        for (int i = 0; i < thirdArea.Count; i++)
        {
            thirdArea[i].item = database.GetItem[thirdArea[i].ID];
        }
        for (int i = 0; i < lastArea.Count; i++)
        {
            lastArea[i].item = database.GetItem[lastArea[i].ID];
        }
        for (int i = 0; i < specialShop1.Count; i++)
        {
            specialShop1[i].item = database.GetItem[specialShop1[i].ID];
        }
        for (int i = 0; i < specialShop2.Count; i++)
        {
            specialShop2[i].item = database.GetItem[specialShop2[i].ID];
        }
    }

    public void OnAfterDeserialize()
    {       
    }
    public void OnBeforeSerialize()
    {
    }


}

[System.Serializable]
public class shopSlot //�����̳ʿ� ����� ����
{
    public int ID; //��� ������ ���̽��� id���� ����
    public Item item;
    public int amount;
    public int _itemType; // 0: weapon+acc  1:consumable 2:other
    public bool isAcc;
    public int price;

    public shopSlot(int _id, Item _item, int _amount, int itemtype)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        _itemType = itemtype;
        if (_itemType == 0) //�κ��� ���� itemtype�� equip �϶� �ش� �������� �Ǽ����� ������ Ȯ��, isAcc���� ���� ���.
        {
            EquipType equipType = (EquipType)item.itemType;
            if (equipType == EquipType.Accessory)
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
