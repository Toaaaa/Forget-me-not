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
    public int priority;

    public Item equipedWeapon;
    public Item equipedAcc;

    //�ϴ��� ���������

    // Update is called once per frame
    void Update()
    {
        
    }
}

