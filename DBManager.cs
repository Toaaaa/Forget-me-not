using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    public string[] var_name;
    public float[] var; //float형 변수들 정보저장

    public string[] switch_name;
    public bool[] switches; //bool형 변수들 정보저장

    public List<Item> itemList = new List<Item>(); //아이템 정보 저장

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
