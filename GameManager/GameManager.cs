using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player Player;

    public int questNum; //���� ����Ʈ �� Ư�� �����Ȳ�� ������ Ȱ���ϴ� ����.

    public bool cantAction; //�÷��̾ npc�� ��ȣ �ۿ� ���� �����̸� �ȵǴ� ������ �������϶� true.
    public bool onSceneChange; //���� �ٲ�� ���϶� true.
    public bool isTalk; //��ȭ���϶� true.


    public MapData mapData;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public EventManager eventManager;
    public GameObject MenuUI;
    public GameObject shopUI;
    public CameraManager Camera;
    public DBManager database;
    public Inventory inventory; //inventory.save // inventory.load �޼��� ��밡��.
    public PlayableManager playableManager;


    private void Awake()
    {
        Camera = GetComponent<CameraManager>();
        eventManager = GetComponent<EventManager>();
        DontDestroyOnLoad(Player.gameObject); //�÷��̾� ������Ʈ�� ���� �ٲ� �ı����� �ʰ� ��.
        //�÷��̾ ���� �����Ѷ� �ϳ��� �����
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)&&!shopUI.activeSelf&&!combatDisplay.gameObject.activeSelf&&!isTalk) 
            MenuUI.SetActive(true); //ui�� ���� ��� �޴� Ȱ��ȭ.
        
        /*
        if (Input.GetKeyDown(KeyCode.S)) //������ �߰� �׽�Ʈ��.
        {
            inventory.AddItem(database.GetItem[1], 1, 0);
        }*/

        cantAction = MenuUI.activeSelf || shopUI.activeSelf || combatDisplay.gameObject.activeSelf||onSceneChange||isTalk ? true : false; //�޴��� Ȱ��ȭ �Ǿ������� cantAction�� true.
    }
    private void OnApplicationQuit()
    {
        //inventory.Container.Clear(); //����Ƽ �÷��� �����// ��������� �κ��丮 Ŭ����
    }
}
