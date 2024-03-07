using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public int encounterRate = 10; //추후에 각 맵마다 다르게 설정 할수 있도록 이곳에서 인카운터 확률을 받아서 반응하게 하기.

    public void encounterEvent()
    {
        Debug.Log("몬스터 인카운터!");
    }
}
