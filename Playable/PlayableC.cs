using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableC : ScriptableObject
{
    public int level;
    public int hp;
    public int mp;
    public int atk;
    public int def;
    public int spd;
    public int exp;
    public int maxExp;
    public bool isDead;

    public bool isJoin; //현재 파티에 참가중인지 확인.

    public Item equipedWeapon;
    public Item equipedAcc;

    //일단은 여기까지만

    // Update is called once per frame
    void Update()
    {
        
    }
}

