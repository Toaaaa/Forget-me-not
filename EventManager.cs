using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public int encounterRate = 10; //���Ŀ� �� �ʸ��� �ٸ��� ���� �Ҽ� �ֵ��� �̰����� ��ī���� Ȯ���� �޾Ƽ� �����ϰ� �ϱ�.

    public void encounterEvent()
    {
        Debug.Log("���� ��ī����!");
    }
}
