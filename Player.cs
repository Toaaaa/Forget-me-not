using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MovingObject
{
    float h;
    float v;

    Rigidbody2D rigid;

    protected override void Start()
    {
        GameManager.Instance.Player = this;
        base.Start();
    }

    private void Awake()
    {
       rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v);
    }
}
