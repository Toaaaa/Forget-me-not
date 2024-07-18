using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player :Singleton<Player> //���� �ٸ��� ��ӹް� �ٲ��� movingobject�� �ʿ����.
{
    [SerializeField]
    float Speed = 5f;

    public float h;
    public float v;
    bool isHorizonMove;
    bool isAutoMove;// �߸��� ��η� ���� �־ �ڵ����� �̵� ������� �� ��� ���� ����.
    public bool isMoving;
    public string currentMapName;//�̵��� ���̸��� �޾��ֱ�
    public bool isDown;//�̵��� �� ���� �����͸� �޾���.
    public int buildingNum;//�̵��� ���� ���� ��ȣ�� �޾���.
    private float hori_time;//h��ǲ ���ӽð�
    private float verti_time;//v��ǲ ���ӽð�
    private float hori_delta;
    private float verti_delta;
    public Vector2 dirVec;//direction of where player is looking at
    public Vector3 combatPosition;
    Rigidbody2D rigid;
    GameObject scanedObject;
    public GameObject CAT;//����� ������Ʈ.
    public GameManager gameManager;
    public Animator animator;


    ///��ȭ �ý���
    public VirtualCamera virtualCamera; //���� ī�޶�. (�̰��� �¿��� �ϸ鼭 ���丮 ��½��� ī�޶� �̵� ����)
    public TextManager textManager;
    public GameObject textPanel;//��ȭâ ��ü.
    public GameObject nameBox;
    public GameObject nameText;
    public GameObject imageBox; //�ʻ�ȭ�� ��� �ڽ�.
    public Image portrait;
    public TypeEffect talk;
    public int talkIndex;
    public bool talking;
    public bool alarmOn;//�˶��� �ؽ�Ʈ �޽��� ����.
    public bool isStory;//���丮 ����� �ؽ�Ʈ �޽��� ����.

    public bool storyTalking;

    public PlayableC magician;//������ ĳ���� ��ũ��Ʈ.

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
        if (!isAutoMove)
        {
            if(hori_time != 0 && verti_time != 0)//���� �ΰ��� ���ÿ� �������� ���� ��� �������� �Էµ� ���� ����ϱ�
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
        //////�ð� ���� + ����
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
            isHorizonMove = hori_time > verti_time;//�ΰ��� ���ÿ� �������� �������� �������� �켱���� ����ϱ� ���� ���� ����.

        //direction
        if(vDown && v == 1)
            dirVec = Vector3.up;
        else if(vDown && v == -1)
            dirVec = Vector3.down;
        else if(hDown && h == 1)
            dirVec = Vector3.right;
        else if(hDown && h == -1)
            dirVec = Vector3.left;

        //������ �ִϸ�����
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
                case "Object":// ª�� ������ ������ ������Ʈ. (���� å�̴�. ��)
                    Debug.Log("Object");
                    TalkAction(scanedObject);
                    break;
                case "Story":
                    //���丮 ���� ������Ʈ��. �ش� ������Ʈ�� ����ִ� ���� ���� or �Լ� ������ ����
                    //�ڵ� ��� ���丮 ��ũ��Ʈ ���.
                    break;
                case "Shop":
                    gameManager.shopUI.SetActive(true);
                    gameManager.shopUI.GetComponent<ShopUI>().shopName = scanedObject.name;
                    //���⸦ ���ؼ� �߰��� shopUI�� ���� ������ ���� ����.
                    break;
                case "Portal":
                    scanedObject.GetComponent<Portal>().portalUseUI.SetActive(true);
                    //���� portalOn���� ui�� ����� �̵����� ����� ��� �߰�.
                    break;
                case "Item":
                    //ȹ�� ������ �������� ��� ��ȣ�ۿ�.>>�Ϲ������δ� Box�� �̿��ؼ� �ϳ�
                    //Ȥ�� ����� ���� ������ �̿��� ��.
                    break;
                case "Box":
                    //���ڸ� ��� ������ ȹ�� + ������ ���� ����.
                    ItemBox box = scanedObject.GetComponent<ItemBox>();
                    if (!box.isOpened)
                    {
                        box.OpenBox();
                        alarmOn = true;
                        ShowExtraAlarm(box.itemInBox);
                    }
                    break;
                case "Interactable":
                    //��ȣ�ۿ� ������ ������Ʈ(�μ��� ��, ��, ��..)
                    GetComponent<CheckSSForStatus>().Interaction();
                    break;
                    default:
                        break;
            }
        }//����Һ� interaction

        //�÷��̾��� ��������Ʈ on/off ���� ��ũ��Ʈ.
        if (CombatManager.Instance.isCombatStart)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }//������ ���۵Ǹ� ī�޶� ��ġ�� ����ְ� �÷��̾� �Ⱥ��̰� �ϱ�.
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
        //Debug.DrawRay(this.transform.position, dirVec*0.7f, new Color(0, 1, 0)); <<���̸� ������ ������
        RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, dirVec, 0.7f, LayerMask.GetMask("Interactable"));
        //chekcing if the ray hit the object that is in the layermask "Interactable" <<interactable ���̾�� ��ȣ �ۿ��� ������ ��� ������Ʈ
        if (rayHit.collider != null) // ĳ���;տ� �ִ� ������Ʈ�� ��ĵ�Ͽ� ����.
        {
            scanedObject = rayHit.collider.gameObject;
        }
        else
            scanedObject = null;

    }

    public void ShowAlarm(int storyNum,int vec)// �˶� �϶��� talking �� storytalking�� true�� �ȴ� << npc�� ������Ʈ�� ��ȭ�� ��� talking �� true�� �ȴ�.
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
    public void ShowExtraAlarm(Item item)// �˶� �϶��� talking �� storytalking�� true�� �ȴ� << npc�� ������Ʈ�� ��ȭ�� ��� talking �� true�� �ȴ�.
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
            talkData = textManager.GetStoryTalk(100, talkIndex); // ������ ȹ��� ��� ��ȣ�� storyTalkData 100�� ����.
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

        if(isNPC) //��ȭ ��밡 npc�� ���.
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

    private void Story(int storyNum)//���� �����ؾ� ��.
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

        switch (talkData.Split(':')[3])
        {
            case "1":
                SceneChangeManager.Instance.BlackOut();
                break;
            case "2"://����� �߰� (inparty��)
                //����� �߰�.+����� ������Ʈ fadein (����� ������Ʈ >>CAT)
                break;
            case "3":
                //����̿�����Ʈ fadeout
                break;
            case "4":
                TextBoxShake();
                break;
            case "5"://ī�޶� �̵�
                TurnOffMainCamera();
                break;
            case "6"://ī�޶� �̵� + ä�� �ڽ� ���� (��Ŀ���� �ι����� ī�޶� �̵������ִ� �Լ�)
                TurnOffMainCamera();//���� ���빰 ����
                TextBoxShake();
                break;
            case "7"://ī�޶� �̵� ���� ����
                TurnOnMainCamera();
                break;
            case "8"://�������� 1 ���� �������� ����� >> ������� �̹��� ����.
                //����Ʈ�� �Բ� ��������Ʈ ����.
                break;
            case "9"://mapdata ���� ������ �����ͼ� ���� ���� (�������� 1������ ��������)
                gameManager.combatManager.OnCombatStart();
                break;
            case "101"://�������� 1 �Ϸ�
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
    bool CheckTheProgress(int storyNum)//�����Ȳ�� Ȯ���ϴ� �Լ�. >>storyscriptable�� ���� Ȯ��, ���� �̹� ����� ����� ��� ��ŵ.
    {
        switch(storyNum)
        {
            case 1000:
                if (!gameManager.storyScriptable.firstTime)
                    return true;//������� ���� ����� ���
                else
                    return false;//����� ����� ��� 
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
                if(gameManager.storyScriptable.Stage1Started)
                    return true;
                else
                    return false;
            case 7000:
                if(gameManager.storyScriptable.Stage1beforEncounter)
                    return true;
                else
                    return false;
            default:
                Debug.Log("Wrong StoryNum");
                return false;
        }
    }
    void SetTheProgress(int storyNum)//��簡 ������ �����Ȳ�� �����ϴ� �Լ�. >>storyscriptable�� ���� ����.
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
            case 6000:
                gameManager.storyScriptable.Stage1Started = true;
                break;
            case 7000:
                gameManager.storyScriptable.Stage1beforEncounter = true;
                break;
            default:
                break;
        }
    }

    //�ؽ�Ʈ �ڽ�
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

    private void CheckTheLastDir()//���� ��ǲ h,v�� ����ϴ� �Է��� ���ÿ� �Է����ϋ� ���������� �Էµ� ��ǲ�� �켱���� �����ϴ� �Լ�.
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
    } //���� �ȵɰ��� �������� ��� ������ �̵���Ű�� �Լ�.
}
