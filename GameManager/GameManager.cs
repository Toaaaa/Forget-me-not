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

    public int questNum; //���� ����Ʈ �� Ư�� �����Ȳ�� ������ Ȱ���ϴ� ����.

    public bool cantAction; //�÷��̾ npc�� ��ȣ �ۿ� ���� �����̸� �ȵǴ� ������ �������϶� true.
    public bool onSceneChange; //���� �ٲ�� ���϶� true.
    public bool isTalk; //��ȭ���϶� true.
    public bool isOtherUI; //�ٸ� UI�� Ȱ��ȭ �Ǿ������� true.


    public MapData mapData;
    public TextManager textManager;//�ڵ� ��� ��ȭ�� ���. istalk���ְ�, istalk�϶��� �Լ�(storyscriptplay) ��� �ȵǰ� ����.
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public EventManager eventManager;
    public GameObject MenuUI;
    public GameObject shopUI;
    public Camera Camera;
    public DBManager database;
    public Inventory inventory; //inventory.save // inventory.load �޼��� ��밡��.
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
        DontDestroyOnLoad(Player.gameObject); //�÷��̾� ������Ʈ�� ���� �ٲ� �ı����� �ʰ� ��.
                                              //�÷��̾ ���� �����Ѷ� �ϳ��� �����

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !shopUI.activeSelf && !combatDisplay.gameObject.activeSelf && !isTalk && !isOtherUI)
            MenuUI.SetActive(true); //ui�� ���� ��� �޴� Ȱ��ȭ.

        /*
        if (Input.GetKeyDown(KeyCode.S)) //������ �߰� �׽�Ʈ��.
        {
            inventory.AddItem(database.GetItem[1], 1, 0);
        }*/

        cantAction = MenuUI.activeSelf || shopUI.activeSelf || combatDisplay.gameObject.activeSelf || onSceneChange || isTalk || isOtherUI ? true : false; //�޴��� Ȱ��ȭ �Ǿ������� cantAction�� true.
    }
    private void OnApplicationQuit()
    {
        //inventory.Container.Clear(); //����Ƽ �÷��� �����// ��������� �κ��丮 Ŭ����
    }

    public void ChangeCameraView()
    {
        
    }
    void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        switch (arg0.name)
        {
            case "perspective���� ��"://perspective�� �����ؾ��ϴ� ��
                ChangeCameraViewPerspective();
                break;
            default://���� �������� ���� ��� orthographic���� ����
                ChangeCameraViewOrtho();
                break;
        }
    }
    public void ChangeCameraViewOrtho()//�Ϻ� �������� orthographic���� �����
    {
        if(Camera != null)
        { 
            Camera.orthographic = true; //orthographic���� ����
            Camera.orthographicSize = 5;
        }
    }
    public void ChangeCameraViewPerspective()//�Ϻ� �������� perspective�� �����
    {
        if(Camera != null)
        {
            Camera.orthographic = false; //perspective�� ����
            Camera.fieldOfView = 36.8f;
        }
    }
}
