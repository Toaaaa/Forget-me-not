using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectId : MonoBehaviour
{
    public int ID;
    public int tempID; //�������� ID ������, questNum ���� ������ �߰��Ǿ� ID�� ������ �ټ��� �ִ� ����,
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
