using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    public string[] var_name;
    public float[] var; //float�� ������ ��������

    public string[] switch_name;
    public bool[] switches; //bool�� ������ ��������

    public List<Item> itemList = new List<Item>(); //������ ���� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
