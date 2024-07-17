using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public Player Player;

    public int questNum; //현재 퀘스트 등 특정 진행상황이 있을때 활용하는 변수.

    public bool cantAction; //플레이어가 npc와 상호 작용 등의 움직이면 안되는 동작을 진행중일때 true.
    public bool onSceneChange; //씬이 바뀌는 중일때 true.
    public bool isTalk; //대화중일때 true.
    public bool isOtherUI; //다른 UI가 활성화 되어있을때 true.


    public MapData mapData;
    public TextManager textManager;//자동 재생 대화일 경우. istalk켜주고, istalk일때는 함수(storyscriptplay) 재생 안되게 주의.
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public EventManager eventManager;
    public GameObject MenuUI;
    public GameObject shopUI;
    public Camera Camera;
    public DBManager database;
    public Inventory inventory; //inventory.save // inventory.load 메서드 사용가능.
    public StoryScriptable storyScriptable;
    public PlayableManager playableManager;
    public RewardPageManager rewardPageManager;
    public UIPanelEffect portalUI;
    public CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        if (Camera == null)
            Camera = FindObjectOfType<Camera>();

        eventManager = GetComponent<EventManager>();
        DontDestroyOnLoad(Player.gameObject); //플레이어 오브젝트는 씬이 바뀌어도 파괴되지 않게 함.
                                              //플레이어가 복수 존재한때 하나만 남기기

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !shopUI.activeSelf && !combatDisplay.gameObject.activeSelf && !isTalk && !isOtherUI)
            MenuUI.SetActive(true); //ui가 전부 담긴 메뉴 활성화.

        /*
        if (Input.GetKeyDown(KeyCode.S)) //아이템 추가 테스트용.
        {
            inventory.AddItem(database.GetItem[1], 1, 0);
        }*/

        cantAction = MenuUI.activeSelf || shopUI.activeSelf || combatDisplay.gameObject.activeSelf || onSceneChange || isTalk || isOtherUI ? true : false; //메뉴가 활성화 되어있으면 cantAction은 true.
    }
    private void OnApplicationQuit()
    {
        //inventory.Container.Clear(); //유니티 플레이 종료시// 게임종료시 인벤토리 클리어
    }

    public void ChangeCameraView()
    {
        
    }
    void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        switch (arg0.name)
        {
            case "perspective쓰는 씬"://perspective로 변경해야하는 씬
                ChangeCameraViewPerspective();
                break;
            default://따로 정해주지 않은 경우 orthographic으로 변경
                ChangeCameraViewOrtho();
                break;
        }
    }
    public void ChangeCameraViewOrtho()//일부 씬에서는 orthographic으로 변경시
    {
        if(Camera != null)
        { 
            Camera.orthographic = true; //orthographic으로 변경
            Camera.orthographicSize = 5;
        }
    }
    public void ChangeCameraViewPerspective()//일부 씬에서는 perspective로 변경시
    {
        if(Camera != null)
        {
            Camera.orthographic = false; //perspective로 변경
            Camera.fieldOfView = 36.8f;
        }
    }
}
