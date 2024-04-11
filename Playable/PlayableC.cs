using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableC : ScriptableObject
{
    public int level;
    public int hp;
    public int maxHp;
    public int mp;
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

    //debuff
    public bool isPoisoned;
    public bool isSkillSealed;//��ų ����

    public int jobNum; // 0:���� 1:������ 2:��Ŀ 3:����
    public Item equipedWeapon;
    public Item equipedAcc;



    //�ϴ��� ���������

    // Update is called once per frame
    void Update()
    {
        
    }
}

