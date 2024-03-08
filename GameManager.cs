using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player Player;
    public bool cantAction; //플레이어가 npc와 상호 작용 등의 움직이면 안되는 동작을 진행중일때 true.
    public EventManager eventManager;
    
    
    void Update()
    {
        
    }
}
