using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player Player;
    public bool cantAction; //플레이어가 npc와 상호 작용 등의 움직이면 안되는 동작을 진행중일때 true.

    public EventManager eventManager;
    public GameObject MenuUI;
    public CameraManager Camera;
    public Inventory inventory;


    private void Awake()
    {
        Camera = GetComponent<CameraManager>();
        eventManager = GetComponent<EventManager>();
        DontDestroyOnLoad(Player.gameObject); //플레이어 오브젝트는 씬이 바뀌어도 파괴되지 않게 함.
        //플레이어가 복수 존재한때 하나만 남기기
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            MenuUI.SetActive(!MenuUI.activeSelf); //활성화 되어있으면 비활성화, 비활성화 되어있으면 활성화.

        cantAction = MenuUI.activeSelf ? true : false; //메뉴가 활성화 되어있으면 cantAction은 true.
    }
    private void OnApplicationQuit()
    {
        //inventory.Container.Clear(); //유니티 플레이 종료시// 게임종료시 인벤토리 클리어
    }
}
