using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player Player;
    public bool cantAction; //�÷��̾ npc�� ��ȣ �ۿ� ���� �����̸� �ȵǴ� ������ �������϶� true.
    public EventManager eventManager;
    
    
    void Update()
    {
        
    }
}
