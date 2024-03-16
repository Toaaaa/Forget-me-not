using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New DBManager", menuName = "DBManager/DBManager")]
public class DBManager : ScriptableObject, ISerializationCallbackReceiver
{ 
    public string[] var_name;
    public float[] var; //float형 변수들 정보저장

    public string[] switch_name;
    public bool[] switches; //bool형 변수들 정보저장

    public Item[] items;
    public Dictionary<Item, int> GetId = new Dictionary<Item, int>(); //아이템 아이디 정보 저장
    public Dictionary<int, Item> GetItem = new Dictionary<int, Item>(); //딕셔너리의 복제본
    //아이템 등의 대량의 정보 관리의 경우 List보다 Dictionary가 효율적이다.

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<Item, int>();
        GetItem = new Dictionary<int, Item>();
        for (int i = 0; i < items.Length; i++)
        {
            GetId.Add(items[i], i);
            GetItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
