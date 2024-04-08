using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> //일단은 사용보류.
{
   public MapData mapData;

    public void encounterEvent()
    {
        if(mapData.monsters.Count == 0 ) //몬스터가 없음
        {
            return;
        }
        else
        {

        }
    }
}
