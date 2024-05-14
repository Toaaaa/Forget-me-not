using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player :Singleton<Player> //추후 다른거 상속받게 바꾸자 movingobject는 필요없네.
{
    [SerializeField]
    float Speed = 5f;

    float h;
    float v;
    bool isHorizonMove;
    public bool isMoving;
    public string currentMapName;//이동전 맵이름을 받아주기
    public Vector2 dirVec;//direction of where player is looking at
    public Vector3 combatPosition;
    Rigidbody2D rigid;
    GameObject scanedObject;
    public GameManager gameManager;
    ///대화 시스템
    public TextManager textManager;
    public GameObject textPanel;//대화창 전체.
    public GameObject nameBox;
    public GameObject nameText;
    public GameObject imageBox; //초상화가 담긴 박스.
    public Image portrait;
    public TypeEffect talk;
    public int talkIndex;
    public bool talking;

    public bool storyTalking;



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
        isMoving = h != 0 || v != 0; //if h or v is not 0, isMoving is true.
        
        h = gameManager.cantAction ? 0 : Input.GetAxisRaw("Horizontal"); //if cantAction is true, h is 0.
        v = gameManager.cantAction ? 0 : Input.GetAxisRaw("Vertical"); //if cantAction is true, v is 0.

        bool hDown = gameManager.cantAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.cantAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.cantAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.cantAction ? false : Input.GetButtonUp("Vertical");

        if(hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if(hUp || vUp)
            isHorizonMove = h != 0; //if h is not 0, isHorizonMove is true. (if h is 0, isHorizonMove is false.

        //direction
        if(vDown && v == 1)
            dirVec = Vector3.up;
        else if(vDown && v == -1)
            dirVec = Vector3.down;
        else if(hDown && h == 1)
            dirVec = Vector3.right;
        else if(hDown && h == -1)
            dirVec = Vector3.left;


        //scan object
        if(Input.GetButtonDown("Jump") && scanedObject != null)
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
                case "Object":
                    Debug.Log("Object");
                    TalkAction(scanedObject);
                    break;
                case "Story":
                    //스토리 진행 오브젝트로. 해당 오브젝트가 들고있는 변수 참고 or 함수 실행을 통해
                    //자동 재생 스토리 스크립트 재생.
                    break;
                case "Shop":
                    gameManager.shopUI.SetActive(true);
                    gameManager.shopUI.GetComponent<ShopUI>().shopName = scanedObject.name;
                    //여기를 통해서 추가로 shopUI에 대한 정보에 접근 가능.
                    break;
                case "Portal":
                    scanedObject.GetComponent<Portal>().portalOn();
                    //추후 portalOn에서 ui를 띠워서 이동할지 물어보는 기능 추가.
                    break;

                    default:
                        break;
            }
        }

        if (CombatManager.Instance.isCombatStart)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }//전투가 시작되면 카메라 위치만 잡아주고 안보이게 하기.
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
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
            nameText.GetComponent<TextMeshProUGUI>().text = scanedObject.name;
            portrait.sprite = textManager.GetPortrait(ID, int.Parse(talkData.Split(':')[1]));
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

    public void StoryAction(int storyNum)
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
            talking = false;
            storyTalking = false;
            SetTheProgress(storyNum);
            gameManager.isTalk = false;
            talkIndex = 0;
            return;
        }
        //
            talk.SetMsg(talkData.Split(':')[0]);
            imageBox.SetActive(true);
            nameBox.SetActive(true);
            portrait.sprite = textManager.GetStoryPortrait(storyNum, int.Parse(talkData.Split(':')[1]));
            nameText.GetComponent<TextMeshProUGUI>().text = talkData.Split(':')[2];
        /*if (talkData.Split(':')[3] != null) //3이 1일 경우 암전 한번.
        {
            암전 기능을 넣고싶은데.. 일단 보류.
        }*/
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
            default:
                break;
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
    public void CombatPositioning()
    {
        transform.position = combatPosition;
    }
}
