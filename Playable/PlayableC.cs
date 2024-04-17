using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableC : ScriptableObject
{
    public Sprite characterImage;
    public int level;
    public float hp;
    public int maxHp;
    public float mp;
    public int maxMp;
    public int atk;
    public int def;
    public int spd;
    public int crit;
    public int exp;
    public int maxExp;
    public int priority;
    public int fatigue;
    public int maxFatigue;

    //오리지널 스텟 //전투가 끝나면 해당 스텟으로 리셋해주기.
    public int originalMaxHp;
    public int originalMaxMp;
    public int originalAtk;
    public int originalDef;
    public int originalSpd;
    public int originalCrit;

    //buff
    public bool isBuffed;
    ///////////////////
    public bool attackBuff;
    public bool defenseBuff;
    public bool speedBuff;
    public bool critBuff;

    //debuff
    public bool isStunned;
    public bool isPoisoned;
    public bool isSkillSealed;


    public int jobNum; // 0:전사 1:마법사 2:탱커 3:힐러
    public Item equipedWeapon;
    public Item equipedAcc;



    
    virtual public void Attack()
    {
        Debug.Log("공격");
    }
    virtual public void Skill1()
    {
        Debug.Log("스킬");
    }
    virtual public void Skill2()
    {
        Debug.Log("스킬");
    }
    virtual public void Skill3()
    {
        Debug.Log("스킬");
    }
    virtual public void Skill4()
    {
        Debug.Log("스킬");
    }
}

