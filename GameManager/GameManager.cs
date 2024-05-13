using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player Player;

    public int questNum; //현재 퀘스트 등 특정 진행상황이 있을때 활용하는 변수.

    public bool cantAction; //플레이어가 npc와 상호 작용 등의 움직이면 안되는 동작을 진행중일때 true.
    public bool onSceneChange; //씬이 바뀌는 중일때 true.
    public bool isTalk; //대화중일때 true.


    public MapData mapData;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public EventManager eventManager;
    public GameObject MenuUI;
    public GameObject shopUI;
    public CameraManager Camera;
    public DBManager database;
    public Inventory inventory; //inventory.save // inventory.load 메서드 사용가능.
    public PlayableManager playableManager;


    private void Awake()
    {
        Camera = GetComponent<CameraManager>();
        eventManager = GetComponent<EventManager>();
        DontDestroyOnLoad(Player.gameObject); //플레이어 오브젝트는 씬이 바뀌어도 파괴되지 않게 함.
        //플레이어가 복수 존재한때 하나만 남기기
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)&&!shopUI.activeSelf&&!combatDisplay.gameObject.activeSelf&&!isTalk) 
            MenuUI.SetActive(true); //ui가 전부 담긴 메뉴 활성화.
        
        /*
        if (Input.GetKeyDown(KeyCode.S)) //아이템 추가 테스트용.
        {
            inventory.AddItem(database.GetItem[1], 1, 0);
        }*/

        cantAction = MenuUI.activeSelf || shopUI.activeSelf || combatDisplay.gameObject.activeSelf||onSceneChange||isTalk ? true : false; //메뉴가 활성화 되어있으면 cantAction은 true.
    }
    private void OnApplicationQuit()
    {
        //inventory.Container.Clear(); //유니티 플레이 종료시// 게임종료시 인벤토리 클리어
    }
}
