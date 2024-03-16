using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player Player;
    public bool cantAction; //�÷��̾ npc�� ��ȣ �ۿ� ���� �����̸� �ȵǴ� ������ �������϶� true.

    public EventManager eventManager;
    public GameObject MenuUI;
    public CameraManager Camera;
    public Inventory inventory;


    private void Awake()
    {
        Camera = GetComponent<CameraManager>();
        eventManager = GetComponent<EventManager>();
        DontDestroyOnLoad(Player.gameObject); //�÷��̾� ������Ʈ�� ���� �ٲ� �ı����� �ʰ� ��.
        //�÷��̾ ���� �����Ѷ� �ϳ��� �����
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            MenuUI.SetActive(!MenuUI.activeSelf); //Ȱ��ȭ �Ǿ������� ��Ȱ��ȭ, ��Ȱ��ȭ �Ǿ������� Ȱ��ȭ.

        cantAction = MenuUI.activeSelf ? true : false; //�޴��� Ȱ��ȭ �Ǿ������� cantAction�� true.
    }
    private void OnApplicationQuit()
    {
        //inventory.Container.Clear(); //����Ƽ �÷��� �����// ��������� �κ��丮 Ŭ����
    }
}
