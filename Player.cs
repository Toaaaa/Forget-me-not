using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Player :Singleton<Player> //추후 다른거 상속받게 바꾸자 movingobject는 필요없네.
{
    [SerializeField]
    float Speed = 5f;

    public float h;
    public float v;
    bool isHorizonMove;
    bool isAutoMove;// 잘못된 경로로 가고 있어서 자동으로 이동 시켜줘야 할 경우 쓰는 변수.
    public bool isMoving;
    public string currentMapName;//이동전 맵이름을 받아주기
    public bool isDown;//이동전 맵 세부 데이터를 받아줌.
    public int buildingNum;//이동전 맵의 빌딩 번호를 받아줌.
    private float hori_time;//h인풋 지속시간
    private float verti_time;//v인풋 지속시간
    private float hori_delta;
    private float verti_delta;
    public Vector2 dirVec;//direction of where player is looking at
    public Vector3 combatPosition;
    public bool wasInCombat;//전투에 참여했었는지 확인하는 변수.//해당 변수의 경우 게임을 새로 킬때는 저장되면 안됨
    public Vector3 placeBeforeEnteringCombat;//전투입장전 위치 저장용 변수.//해당 변수의 경우 게임을 새로 킬때는 저장되면 안됨
    Rigidbody2D rigid;
    GameObject scanedObject;
    public GameObject CAT;//고양이 오브젝트.
    public GameManager gameManager;
    public Animator animator;


    ///대화 시스템
    public VirtualCamera virtualCamera; //메인 카메라. (이것을 온오프 하면서 스토리 출력시의 카메라 이동 조절)
    public TextManager textManager;
    public GameObject textPanel;//대화창 전체.
    public GameObject nameBox;
    public GameObject nameText;
    public GameObject imageBox; //초상화가 담긴 박스.
    public Image portrait;
    public TypeEffect talk;
    public int talkIndex;
    public bool talking;
    public bool alarmOn;//알람용 텍스트 메시지 변수.
    public bool isStory;//스토리 진행용 텍스트 메시지 변수.

    public bool storyTalking;

    public PlayableC magician;//마법사 캐릭터 스크립트.

     void Start()
    {
        GameManager.Instance.Player = this;
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
       rigid = GetComponent<Rigidbody2D>();
        if (gameManager == null)
            gameManager = GameManager.Instance;
    }

    void Update()
    {
        PlayerZSorting();//플레이어의 z값을 y값에 따라 조정하여 오브젝트와의 정렬순서를 조정하는 함수.
        isMoving = h != 0 || v != 0; //if h or v is not 0, isMoving is true.
        if (!isAutoMove)
        {
            if(hori_time != 0 && verti_time != 0)//만약 두개가 동시에 눌러지고 있을 경우 마지막에 입력된 값을 출력하기
            {
                CheckTheLastDir();
            }
            else
            {
                h = gameManager.cantAction ? 0 : Input.GetAxisRaw("Horizontal"); //if cantAction is true, h is 0.
                v = gameManager.cantAction ? 0 : Input.GetAxisRaw("Vertical"); //if cantAction is true, v is 0.
            }
        }

        bool hDown = gameManager.cantAction ? false : Input.GetButton("Horizontal");
        bool vDown = gameManager.cantAction ? false : Input.GetButton("Vertical");
        bool hUp = gameManager.cantAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.cantAction ? false : Input.GetButtonUp("Vertical");
        //////시간 세팅 + 리셋
        ///
        hori_delta += Time.deltaTime;
        verti_delta += Time.deltaTime;

        if (hDown)
            hori_time = hori_delta;
        if (vDown)
            verti_time = verti_delta;
        if (hUp)
        {
            hori_time = 0;
            hori_delta = 0;
        }
        if (vUp)
        {
            verti_time = 0;
            verti_delta = 0;
        }
        if (h == 0 & v == 0)
        {
            verti_delta = 0;
            hori_delta = 0;
        }
        /////
        if(hDown)
            isHorizonMove = true;
        else
            isHorizonMove = false;
        if(hUp || vUp)
            isHorizonMove = h != 0; //if h is not 0, isHorizonMove is true. (if h is 0, isHorizonMove is false.
        if(hDown&&vDown)
            isHorizonMove = hori_time > verti_time;//두개가 동시에 눌렸을때 마지막에 눌린것을 우선으로 출력하기 위한 변수 설정.

        //direction
        if(vDown && v == 1)
            dirVec = Vector3.up;
        else if(vDown && v == -1)
            dirVec = Vector3.down;
        else if(hDown && h == 1)
            dirVec = Vector3.right;
        else if(hDown && h == -1)
            dirVec = Vector3.left;

        //움직임 애니메이터
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
        animator.SetBool("isMoving", isMoving);

        //scan object
        if (Input.GetButtonUp("Jump") && scanedObject != null)
        {
            switch(scanedObject.tag)
            {
                case "NPC":
                    Debug.Log("NPC");
                    TalkAction(scanedObject);
                    //talk
                    break;
                case "ExtraNPC":
                    Debug.Log("ExtraNPC");
                    TalkAction(scanedObject);
                    //talk
                    break;
                case "Object":// 짧은 설명이 나오는 오브젝트. (낡은 책이다. 등)
                    Debug.Log("Object");
                    TalkAction(scanedObject);
                    break;
                case "Story":
                    int i=scanedObject.GetComponent<ObjectId>().ID;
                    StoryAction(i);
                    //스토리 진행 오브젝트로. 해당 오브젝트가 들고있는 변수 참고 or 함수 실행을 통해
                    //자동 재생 스토리 스크립트 재생.
                    break;
                case "Shop":
                    gameManager.shopUI.SetActive(true);
                    gameManager.shopUI.GetComponent<ShopUI>().shopName = scanedObject.name;
                    //여기를 통해서 추가로 shopUI에 대한 정보에 접근 가능.
                    break;
                case "Portal":
                    scanedObject.GetComponent<Portal>().portalUseUI.SetActive(true);
                    //추후 portalOn에서 ui를 띠워서 이동할지 물어보는 기능 추가.
                    break;
                case "Item":
                    //획득 가능한 아이템의 경우 상호작용.>>일반적으로는 Box를 이용해서 하나
                    //혹시 사용할 일이 있으면 이용할 것.
                    break;
                case "Box":
                    //상자를 열어서 아이템 획득 + 상자의 상태 변경.
                    ItemBox box = scanedObject.GetComponent<ItemBox>();
                    if (!box.isOpened)
                    {
                        box.OpenBox();
                        alarmOn = true;
                        ShowExtraAlarm(box.itemInBox);
                    }
                    break;
                case "Interactable":
                    //상호작용 가능한 오브젝트(부수는 문, 벽, 등..)
                    GetComponent<CheckSSForStatus>().Interaction();
                    break;
                    default:
                        break;
            }
        }//각요소별 interaction

        //플레이어의 스프라이트 on/off 관련 스크립트.
        if (CombatManager.Instance.isCombatStart)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }//전투가 시작되면 카메라 위치만 잡아주고 플레이어 안보이게 하기.
        else if(SceneChangeManager.Instance.keepPlayerNoSprite)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void FixedUpdate()
    {
        //move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;
        //ray
        //Debug.DrawRay(this.transform.position, dirVec*0.7f, new Color(0, 1, 0)); <<레이를 실제로 보여줌
        RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, dirVec, 0.7f, LayerMask.GetMask("Interactable"));
        //chekcing if the ray hit the object that is in the layermask "Interactable" <<interactable 레이어는 상호 작용이 가능한 모든 오브젝트
        if (rayHit.collider != null) // 캐릭터앞에 있는 오브젝트를 스캔하여 저장.
        {
            scanedObject = rayHit.collider.gameObject;
        }
        else
            scanedObject = null;

    }

    public void ShowAlarm(int storyNum,int vec)// 알람 일때는 talking 과 storytalking이 true가 된다 << npc나 오브젝트의 대화일 경우 talking 만 true가 된다.
    {
        storyTalking = true;
        if (!textPanel.activeSelf)
            gameManager.isTalk = true;
        if (alarmOn)
        {
            textPanel.SetActive(true);
            alarmOn = false;
        }
        MovePlayer(vec);
        Alarm(storyNum);
    }
    public void ShowExtraAlarm(Item item)// 알람 일때는 talking 과 storytalking이 true가 된다 << npc나 오브젝트의 대화일 경우 talking 만 true가 된다.
    {
        storyTalking = true;
        if (!textPanel.activeSelf)
            gameManager.isTalk = true;
        if (alarmOn)
        {
            textPanel.SetActive(true);
            alarmOn = false;
        }
        AlarmForItem(item);
    }


    void Alarm(int storyNum)
    {
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            talkData = textManager.GetStoryTalk(storyNum, talkIndex);
        }

        if (talkData == null)
        {
            alarmOn = false;
            talking = false;
            storyTalking = false;
            gameManager.isTalk = false;
            talkIndex = 0;
            return;
        }
        //
        talk.SetMsg(talkData);
        imageBox.SetActive(false);
        nameBox.SetActive(false);

        talking = true;
    }
    public void AlarmForItem(Item item)
    {

        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            talkData = textManager.GetStoryTalk(100, talkIndex); // 아이템 획득시 대사 번호는 storyTalkData 100에 있음.
        }

        if (talkData == null)
        {
            alarmOn = false;
            talking = false;
            storyTalking = false;
            gameManager.isTalk = false;
            talkIndex = 0;
            return;
        }
        //
        talk.SetExtraMsg(talkData, item);
        imageBox.SetActive(false);
        nameBox.SetActive(false);

        talking = true;
    }
    public void AlarmOff()
    {
        alarmOn = false;
        talking = false;
        storyTalking = false;
        gameManager.isTalk = false;
        talkIndex = 0;
        textPanel.SetActive(talking);

    }
    public void TalkAction(GameObject talkObject)
    {
        scanedObject = talkObject;
        ObjectId objectId = scanedObject.GetComponent<ObjectId>();
        Talk(objectId.ID, objectId.isNPC);
        if(!textPanel.activeSelf)
            gameManager.isTalk = true;
        textPanel.SetActive(talking);

    }
    /*public void Talk_Tile(int storynum)//인터렉트를 통해 talkaction을 실행하는것이 아니라 타일을 밟아서 실행되는 경우 사용//테스트 해본적 없어서 추후 확인 필요.
    {
        Talk(storynum, false);//false == npc가 아님으로 text메시지만 출력.
        if (!textPanel.activeSelf)
            gameManager.isTalk = true;
        textPanel.SetActive(talking);
    }*///다른방법 사용하기로 함.
    private void Talk(int ID, bool isNPC)
    {
        string talkData = "";

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            talkData = textManager.GetTalk(ID, talkIndex);
        }

        if(talkData == null)
        {
            talking = false;
            Debug.Log("End of talk");
            gameManager.isTalk = false;
            talkIndex = 0;
            return;
        }

        if(isNPC) //대화 상대가 npc일 경우.
        {
            talk.SetMsg(talkData.Split(':')[0]);
            imageBox.SetActive(true);
            nameBox.SetActive(true);
            //nameText.GetComponent<TextMeshProUGUI>().text = scanedObject.name;
            portrait.sprite = textManager.GetPortrait(ID, int.Parse(talkData.Split(':')[1]));
            nameText.GetComponent<TextMeshProUGUI>().text = talkData.Split(':')[2];
            portrait.color = new Color32(255, 255, 255, 255);
        }   
        else
        {
            talk.SetMsg(talkData);
            imageBox.SetActive(false);
            nameBox.SetActive(false);
            portrait.color = new Color32(255, 255, 255, 0);
        }

        talking = true;
        talkIndex++;
    }

    public void StoryAction(int storyNum)//이곳에서 스토리 진행
    {
        if (!CheckTheProgress(storyNum))
            return;
        storyTalking = true;
        Story(storyNum);
        if(!textPanel.activeSelf)
            gameManager.isTalk = true;
        textPanel.SetActive(talking);
    }

    private void Story(int storyNum)//변수 수정해야 함.
    {
        string talkData = "";
        isStory = true;
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            
            talkData = textManager.GetStoryTalk(storyNum, talkIndex);
        }

        if (talkData == null)
        {
            isStory = false;
            alarmOn = false;
            talking = false;
            storyTalking = false;
            Debug.Log(talkData == null);
            SetTheProgress(storyNum);
            gameManager.isTalk = false;
            talkIndex = 0;
            return;
        }
        //
            talk.SetMsg(talkData.Split(':')[0]);
        if (storyNum == 23000)//일부 스토리의 경우 imageBox와 nameBox를 사용하지 않음.
        {
            imageBox.SetActive(false);
            nameBox.SetActive(false);
        }
        else
        {
            imageBox.SetActive(true);
            nameBox.SetActive(true);
        }
            portrait.sprite = textManager.GetStoryPortrait(storyNum, int.Parse(talkData.Split(':')[1]));
            nameText.GetComponent<TextMeshProUGUI>().text = talkData.Split(':')[2];

        switch (talkData.Split(':')[3])
        {
            case "1":
                SceneChangeManager.Instance.BlackOut();
                break;
            case "2"://고양이 추가 (inparty에)
                //고양이 추가.+고양이 오브젝트 fadein (고양이 오브젝트 >>CAT)
                break;
            case "3":
                //고양이오브젝트 fadeout
                break;
            case "4":
                TextBoxShake();
                break;
            case "5"://카메라 이동
                TurnOffMainCamera();
                break;
            case "6"://카메라 이동 + 채팅 박스 진동 (포커싱할 인물에게 카메라를 이동시켜주는 함수)
                TurnOffMainCamera();//추후 내용물 수정
                TextBoxShake();
                break;
            case "7"://카메라 이동 원상 복구
                TurnOnMainCamera();
                break;
            case "8"://스테이지 1 종료 시점에서 고양이 >> 마법사로 이미지 변경.
                //이펙트와 함께 스프라이트 변경.
                break;
            case "9"://mapdata 에서 데이터 가져와서 전투 시작 (스테이지 1에서의 보스전투)
                Player.Instance.placeBeforeEnteringCombat = Player.Instance.transform.position;
                gameManager.combatManager.OnCombatStart();
                break;
            case "10":
                //리시아가 페이드 인으로 등장.
                FindObjectOfType<Licia>().TurnOnAlpha();
                //리시아가 파티에 추가됨
                if (!gameManager.playableManager.inParty.inPartySlots[3].isJoin)//힐러가 아직 없을때
                {
                    gameManager.playableManager.inParty.inPartySlots[3].isJoin = true;//힐러 넣어주기
                }
                break;
            case "11"://드래곤의 봉인석 파괴
                FindObjectOfType<RuneStone>().DistoryRune();
                break;
            case "12"://견습기사 파티 추가
                if (!gameManager.playableManager.inParty.inPartySlots[2].isJoin)//전사가 아직 없을때
                {
                    gameManager.playableManager.inParty.inPartySlots[2].isJoin = true;//전사 넣어주기
                }
                break;
            case "13"://견습기사 파티 이탈
                if (gameManager.playableManager.inParty.inPartySlots[2].isJoin)
                {
                    gameManager.playableManager.inParty.inPartySlots[2].isJoin = false;//전사 빼주기
                }
                break;
            case "14"://스테이지 1에서 동상을 칼로 부술때
                //추후 입력
                break;
            case "101"://스테이지 1 완료
                gameManager.storyScriptable.isStage1Completed = true;
                magician.isPerson = true;
                break;
            default:
                break;
        }
        portrait.color = new Color32(255, 255, 255, 255);
        //

        talking = true;
        talkIndex++;
    }
    bool CheckTheProgress(int storyNum)//진행상황을 확인하는 함수. >>storyscriptable의 변수 확인, 만약 이미 진행된 대사일 경우 스킵.
    {
        switch(storyNum)
        {
            case 1000:
                if (!gameManager.storyScriptable.firstTime)
                    return true;//진행되지 않은 대사일 경우
                else
                    return false;//진행된 대사일 경우 
            case 2000:
                if(!gameManager.storyScriptable.secondTime)
                    return true;
                else
                    return false;
            case 3000:
                if(gameManager.storyScriptable.second_map1&&gameManager.storyScriptable.second_map2&&!gameManager.storyScriptable.isTutorial)
                    return true;
                else
                    return false;
            case 4000:
                if(gameManager.storyScriptable.isTutorial&&!gameManager.storyScriptable.isTutorialCompleted)
                    return true;
                else
                    return false;
            case 6000:
                if(!gameManager.storyScriptable.Stage1Started&&gameManager.storyScriptable.isOnStage1)
                    return true;
                else
                    return false;
            case 7000:
                if(!gameManager.storyScriptable.Stage1beforEncounter&&gameManager.storyScriptable.Stage1Started)
                    return true;
                else
                    return false;
            case 8000:
                if(!gameManager.storyScriptable.Stage1BossCompleted && gameManager.storyScriptable.Stage1Encountered)
                    return true;
                else
                    return false;
            case 9000:
                if(!gameManager.storyScriptable.isStage1Completed&&gameManager.storyScriptable.Stage1BossCompleted)
                    return true;
                else
                    return false;
            case 10000:
                if(!gameManager.storyScriptable.isOnStage2)
                    return true;
                else
                    return false;
            case 11000:
                if(!gameManager.storyScriptable.Stage2Check1 && gameManager.storyScriptable.isOnStage2)
                    return true;
                else
                    return false;
            case 12000:
                if(!gameManager.storyScriptable.Stage2Check2 && gameManager.storyScriptable.Stage2Check1)
                    return true;
                else
                    return false;
            case 13000:
                if(!gameManager.storyScriptable.Stage2Check3 && gameManager.storyScriptable.Stage2Check2)
                    return true;
                else
                    return false;
            case 14000:
                if(!gameManager.storyScriptable.Stage2Check4 && gameManager.storyScriptable.Stage2Check3)
                    return true;
                else
                    return false;
            case 15000:
                if(!gameManager.storyScriptable.Stage2Check5 && gameManager.storyScriptable.Stage2Check4)
                    return true;
                else
                    return false;
            case 16000:
                if(!gameManager.storyScriptable.Stage2Check6 && gameManager.storyScriptable.Stage2Check5)
                    return true;
                else
                    return false;
            case 17000://드래곤 처치후 대사
                if(!gameManager.storyScriptable.Stage2Check7 && gameManager.storyScriptable.Stage2Check7Dragon)
                    return true;
                else
                    return false;
            case 18000:
                if(!gameManager.storyScriptable.Stage2Check8 && gameManager.storyScriptable.Stage2Check7)
                    return true;
                else
                    return false;
            case 19000:
                if(!gameManager.storyScriptable.isStage2Completed && gameManager.storyScriptable.Stage2Check8)
                    return true;
                else
                    return false;
            case 20000:
                if(!gameManager.storyScriptable.Stage2Extra0&& !gameManager.storyScriptable.isStage2Completed&& gameManager.storyScriptable.Stage2Check4)
                    return true;
                else
                    return false;
            case 21000:
                if(!gameManager.storyScriptable.Stage2Extra1&& !gameManager.storyScriptable.isStage2Completed&& gameManager.storyScriptable.Stage2Extra0)
                    return true;
                else
                    return false;
            case 22000:
                if(!gameManager.storyScriptable.Stage2Extra2&& !gameManager.storyScriptable.isStage2Completed&& gameManager.storyScriptable.Stage2Extra1)
                    return true;
                else
                    return false;
            //case 23000의 경우 tilemap을 밟으면 자동 재생 이며 tilemanager에서 진행도 체킹 + 진행도 저장을 진행한다

            case 24000:
                if(!gameManager.storyScriptable.Stage2Extra4&& !gameManager.storyScriptable.isStage2Completed&& gameManager.storyScriptable.Stage2Extra3)
                    return true;
                else
                    return false;
            case 25000:
                if(!gameManager.storyScriptable.Stage2Check2&&gameManager.storyScriptable.Stage2Check1)
                    return true;
                else
                    return false;
            case 26000:
                if(!gameManager.storyScriptable.Stage2Check4)
                    return true;
                else
                    return false;
            case 27000:
                if(gameManager.storyScriptable.Stage2Extra4)
                    return true;
                else
                    return false;
            case 28000://도서관 사서 반복대화//기본조건
                if(gameManager.storyScriptable.isOnStage2)
                    return true;
                else
                    return false;
            case 29000://도서관의 일반인 반복대화//기본조건
                if(gameManager.storyScriptable.isOnStage2)
                    return true;
                else
                    return false;
            case 30000://pub의 일반인 반복대화//기본조건
                if(gameManager.storyScriptable.isOnStage2)
                    return true;
                else
                    return false;
            case 31000://메인스토리가 전부 끝난뒤 장로와의 반복 대화
                if(gameManager.storyScriptable.Stage2Check8)
                    return true;
                else
                    return false;
            default:
                Debug.Log("Wrong StoryNum");
                return false;
        }
    }
    void SetTheProgress(int storyNum)//대사가 끝난뒤 진행상황을 저장하는 함수. >>storyscriptable의 변수 수정.
    {
        switch(storyNum)
        {
            case 1000:
                gameManager.storyScriptable.firstTime = true;
                break;
            case 2000:
                gameManager.storyScriptable.secondTime = true;
                break;
            case 3000:
                gameManager.storyScriptable.isTutorial = true;
                break;
            case 4000:
                gameManager.storyScriptable.isTutorialCompleted = true;
                break;
            case 5000:
                gameManager.storyScriptable.isOnStage1 = true;
                break;
            case 6000:
                gameManager.storyScriptable.Stage1Started = true;
                break;
            case 7000:
                gameManager.storyScriptable.Stage1beforEncounter = true;
                break;
            case 8000:
                gameManager.storyScriptable.Stage1BossCompleted = true;
                break;
            case 9000:
                gameManager.storyScriptable.isStage1Completed = true;
                break;
            case 10000:
                gameManager.storyScriptable.isOnStage2 = true;
                break;
            case 11000:
                gameManager.storyScriptable.Stage2Check1 = true;
                break;
            case 12000:
                gameManager.storyScriptable.Stage2Check2 = true;
                break;
            case 13000:
                gameManager.storyScriptable.Stage2Check3 = true;
                break;
            case 14000:
                gameManager.storyScriptable.Stage2Check4 = true;
                break;
            case 15000:
                gameManager.storyScriptable.Stage2Check5 = true;
                break;
            case 16000:
                gameManager.storyScriptable.Stage2Check6 = true;
                break;
            case 17000:
                gameManager.storyScriptable.Stage2Check7 = true;
                break;
            case 18000:
                gameManager.storyScriptable.Stage2Check8 = true;
                break;
            case 19000:
                gameManager.storyScriptable.isStage2Completed = true;
                break;
            case 20000:
                gameManager.storyScriptable.Stage2Extra0 = true;
                break;
            case 21000:
                gameManager.storyScriptable.Stage2Extra1 = true;
                break;
            case 22000:
                gameManager.storyScriptable.Stage2Extra2 = true;
                break;
            case 24000:
                gameManager.storyScriptable.Stage2Extra4 = true;
                break;
            case 25000:
                //반복 대사. 추가 조건 X
                break;
            case 26000:
                //반복 대사. 추가 조건 X
                break;
            case 27000:
                //반복 대사. 추가 조건 X
                break;
            case 28000:
                //반복 대사. 추가 조건 X
                break;
            case 29000:
                //반복 대사. 추가 조건 X
                break;
            case 30000:
                //반복 대사. 추가 조건 X
                break;
            case 31000:
                //반복 대사. 추가 조건 X
                break;
            default:
                break;
        }
    }

    //텍스트 박스
    public void TextBoxShake()
    {
        textPanel.gameObject.transform.DOShakePosition(0.5f, 20, 50, 90, false, true);        
    }
    public void TurnOffMainCamera()
    {
        virtualCamera.gameObject.SetActive(false);
    }
    public void TurnOnMainCamera()
    {
        virtualCamera.gameObject.SetActive(true);
    }

    public void CombatPositioning()
    {
        transform.position = combatPosition;
    }

    private void CheckTheLastDir()//방향 인풋 h,v를 당담하는 입력중 동시에 입력중일떄 마지막으로 입력된 인풋을 우선으로 제공하는 함수.
    {
        if(hori_time > verti_time)
        {
            h = gameManager.cantAction ? 0 : Input.GetAxisRaw("Horizontal");
            v = 0;
        }
        else
        {
            h = 0;
            v = gameManager.cantAction ? 0 : Input.GetAxisRaw("Vertical");
        }
    }
    private void PlayerZSorting()
    {
        var position = this.gameObject.transform.position;
        position.z = 3 + position.y * 0.01f; //(최소값 3)
        this.gameObject.transform.position = position;
    }//플레이어의 z값을 y값에 따라 조정하여 오브젝트와의 정렬순서를 조정하는 함수.

    public void MovePlayer(int i) //i == 0 : up, i == 1 : down, i == 2 : left, i == 3 : right
    {
        isAutoMove = true;
        switch(i)
        {
            case 0:
                v = 1;
                h = 0;
                break;
            case 1:
                v = -1;
                h = 0;
                break;
            case 2:
                v = 0;
                h = -1;
                break;
            case 3:
                v = 0;
                h = 1;
                break;
            default:
                break;
        }
        StartCoroutine(MoveCoroutine());
    }
    
    IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        isAutoMove = false;
        v = 0;
        h = 0;
    } //가면 안될곳을 가고있을 경우 강제로 이동시키는 함수.
}
