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

    //�������� ���� //������ ������ �ش� �������� �������ֱ�.
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


    public int jobNum; // 0:���� 1:������ 2:��Ŀ 3:����
    public Item equipedWeapon;
    public Item equipedAcc;



    
    virtual public void Attack()
    {
        Debug.Log("����");
    }
    virtual public void Skill1()
    {
        Debug.Log("��ų");
    }
    virtual public void Skill2()
    {
        Debug.Log("��ų");
    }
    virtual public void Skill3()
    {
        Debug.Log("��ų");
    }
    virtual public void Skill4()
    {
        Debug.Log("��ų");
    }
}

