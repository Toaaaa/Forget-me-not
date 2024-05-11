using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectId : MonoBehaviour
{
    public int ID;
    public int tempID; //오리지널 ID 이지만, questNum 같은 변수가 추가되어 ID를 세팅해 줄수도 있는 변수,
    public bool isNPC;

    private void Awake()
    {
        if (isNPC)
        {
            ID = tempID + GameManager.Instance.questNum;
        }
        else
        {
            ID = tempID;
        }
    }
}
