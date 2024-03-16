using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New DBManager", menuName = "DBManager/DBManager")]
public class DBManager : ScriptableObject, ISerializationCallbackReceiver
{ 
    public string[] var_name;
    public float[] var; //float�� ������ ��������

    public string[] switch_name;
    public bool[] switches; //bool�� ������ ��������

    public Item[] items;
    public Dictionary<Item, int> GetId = new Dictionary<Item, int>(); //������ ���̵� ���� ����
    public Dictionary<int, Item> GetItem = new Dictionary<int, Item>(); //��ųʸ��� ������
    //������ ���� �뷮�� ���� ������ ��� List���� Dictionary�� ȿ�����̴�.

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
