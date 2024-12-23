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
    public string savePath; ///Inventory.Save 가 savePath 
    private DBManager database;
    public int goldHave;
    public List<InvenSlot> Container = new List<InvenSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (DBManager)AssetDatabase.LoadAssetAtPath("Assets/ScriptableObject/DB_manager.asset", typeof(DBManager));
        AfterEnableAndDesrialize(); //OnAfterDeserialize >> OnEnable 순서이기에 해당 작업을 OnEnable에서 수행.
        //#else 추후 빌드시에는 resource.load가 아니라 addressable asset system을 사용하자. <<
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

    public void OnAfterDeserialize()//에디터 에서 인벤토리에 바로 아이템을 추가할때 동기화시키는 함수
    {      
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item.itemType == ItemType.Equipment)
            {
                if (Container[i].item.equipType == EquipType.Accessory)
                {
                    Container[i].isAcc = true;
                }
                else
                {
                    Container[i].isAcc = false;
                }
            }
            else
            {
                Container[i].isAcc = false;
            }
        }//인벤에 들어갈때 itemtype이 equip 일때 해당 아이템의 악세서리 유무를 확인, isAcc값에 변수 등록.
    }
    private void AfterEnableAndDesrialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            try { Container[i].item = database.GetItem[Container[i].ID]; }
            catch (System.Exception e) { Debug.Log("Exception : " + e); }
        }
        for (int i = 0; i < Container.Count; i++)
        {
            try
            {
                if (Container[i].item == null || Container[i].item != database.GetItem[Container[i].ID])
                {
                    Container[i].item = database.GetItem[Container[i].ID];
                }
            }
            catch (KeyNotFoundException e) { Debug.Log("ItemID가 데이터 베이스와 다름. : " + e); }
        }
    }


    public void OnBeforeSerialize()
    {
    }

    /*
     * if (Input.GetKeyDown(??)) 이와 같은 방식으로 아이템 추가 가능.
        {
            inventory.AddItem(database.GetItem[0], 1, 1);
        }
    */

   
}

[System.Serializable]
public class InvenSlot //컨테이너에 저장될 정보
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
        if(_itemType == 0) //인벤에 들어갈때 itemtype이 equip 일때 해당 아이템의 악세서리 유무를 확인, isAcc값에 변수 등록.
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