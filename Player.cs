using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MovingObject //추후 다른거 상속받게 바꾸자 movingobject는 필요없네.
{
    [SerializeField]
    float Speed = 5f;
    RandomEncounter randomEncounter;

    float h;
    float v;
    bool isHorizonMove;
    public bool isMoving;
    Vector2 dirVec;//direction of where player is looking at

    Rigidbody2D rigid;
    GameObject scanedObject;

    protected override void Start()
    {
        GameManager.Instance.Player = this;
        base.Start();
    }

    private void Awake()
    {
       rigid = GetComponent<Rigidbody2D>();

       DontDestroyOnLoad(this.gameObject); //이거 문제 없겟지..? 추후 플레이어가 중복 생성 되지 않는지 확인@@@@@
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
            Debug.Log("스캔한 오브젝트: " + scanedObject.name);
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
        if (rayHit.collider != null)
        {
            scanedObject = rayHit.collider.gameObject;
        }
        else
            scanedObject = null;

    }
}
