using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player Player;
    public bool cantAction; //�÷��̾ npc�� ��ȣ �ۿ� ���� �����̸� �ȵǴ� ������ �������϶� true.

    public EventManager eventManager;
    public GameObject MenuUI;
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            MenuUI.SetActive(!MenuUI.activeSelf); //Ȱ��ȭ �Ǿ������� ��Ȱ��ȭ, ��Ȱ��ȭ �Ǿ������� Ȱ��ȭ.

        cantAction = MenuUI.activeSelf ? true : false; //�޴��� Ȱ��ȭ �Ǿ������� cantAction�� true.
    }
}
