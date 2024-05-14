using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player :Singleton<Player> //���� �ٸ��� ��ӹް� �ٲ��� movingobject�� �ʿ����.
{
    [SerializeField]
    float Speed = 5f;

    float h;
    float v;
    bool isHorizonMove;
    public bool isMoving;
    public string currentMapName;//�̵��� ���̸��� �޾��ֱ�
    public Vector2 dirVec;//direction of where player is looking at
    public Vector3 combatPosition;
    Rigidbody2D rigid;
    GameObject scanedObject;
    ///��ȭ �ý���
    public TextManager textManager;
    public GameObject textPanel;//��ȭâ ��ü.
    public GameObject nameBox;
    public GameObject nameText;
    public GameObject imageBox; //�ʻ�ȭ�� ��� �ڽ�.
    public Image portrait;
    public TypeEffect talk;
    public int talkIndex;
    public bool talking;



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
    }

    void Update()
    {
        isMoving = h != 0 || v != 0; //if h or v is not 0, isMoving is true.
        
        h = GameManager.Instance.cantAction ? 0 : Input.GetAxisRaw("Horizontal"); //if cantAction is true, h is 0.
        v = GameManager.Instance.cantAction ? 0 : Input.GetAxisRaw("Vertical"); //if cantAction is true, v is 0.

        bool hDown = GameManager.Instance.cantAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = GameManager.Instance.cantAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = GameManager.Instance.cantAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = GameManager.Instance.cantAction ? false : Input.GetButtonUp("Vertical");

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
                    //���丮 ���� ������Ʈ��. �ش� ������Ʈ�� ����ִ� ���� ���� or �Լ� ������ ����
                    //�ڵ� ��� ���丮 ��ũ��Ʈ ���.
                    break;
                case "Shop":
                    GameManager.Instance.shopUI.SetActive(true);
                    GameManager.Instance.shopUI.GetComponent<ShopUI>().shopName = scanedObject.name;
                    //���⸦ ���ؼ� �߰��� shopUI�� ���� ������ ���� ����.
                    break;
                case "Portal":
                    scanedObject.GetComponent<Portal>().portalOn();
                    //���� portalOn���� ui�� ����� �̵����� ����� ��� �߰�.
                    break;

                    default:
                        break;
            }
        }

        if (CombatManager.Instance.isCombatStart)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }//������ ���۵Ǹ� ī�޶� ��ġ�� ����ְ� �Ⱥ��̰� �ϱ�.
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
            GameManager.Instance.isTalk = true;
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
            GameManager.Instance.isTalk = false;
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

    }

    private void Story(int ID, bool isNPC)//���� �����ؾ� ��.
    {

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
    public void CombatPositioning()
    {
        transform.position = combatPosition;
    }
}
