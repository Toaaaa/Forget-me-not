using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> //�ϴ��� ��뺸��.
{
   public MapData mapData;

    public void encounterEvent()
    {
        if(mapData.monsters.Count == 0 ) //���Ͱ� ����
        {
            return;
        }
        else
        {

        }
    }
}
