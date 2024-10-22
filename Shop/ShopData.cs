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
    public string savePath; ///Inventory.Save 가 savePath 
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
        AfterEnableAndDesrialize();//OnAfterDeserialize >> OnEnable 순서이기에 해당 작업을 OnEnable에서 수행.
        //#else 추후 빌드시에는 resource.load가 아니라 addressable asset system을 사용하자. <<
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
public class shopSlot //컨테이너에 저장될 정보
{
    public int ID; //얘는 데이터 베이스의 id값과 같음
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
        if (_itemType == 0) //인벤에 들어갈때 itemtype이 equip 일때 해당 아이템의 악세서리 유무를 확인, isAcc값에 변수 등록.
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
