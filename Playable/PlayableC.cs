using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayableC : ScriptableObject
{
    public Sprite characterImage;
    public int level;
    public float hp; //������ ���ݷ����� ���� �ؼ� ������ ���ݷ� ��ġ�� ����.
    public float maxHp;
    public float mp;
    public float maxMp;
    public float atk;
    public float def;
    public int spd;
    public int crit; //�� ũ��Ƽ�� ������ �ݿ� �Ϸ���. ���� singleton���� ������ �Ŵ��� ���� �ű��� �Լ��� int �Ķ���͸� �޾Ƽ� ������ ����ϴ� ������� �ϴ°͵� ������.
    public int exp;
    public int maxExp;
    public int priority;
    public float fatigue;
    public float maxFatigue;

    public bool isDead;

    //�������� ���� //������ ������ �ش� �������� �������ֱ�.
    public float originalMaxHp;
    public float originalMaxMp;
    public float originalAtk;
    public float originalDef;
    public int originalSpd;
    public int originalCrit;

    //buff
    public bool isBuffed;
    ///////////////////�Ʒ��� 3 ������ ��ø �Ұ���. (�Ҹ� ���������� ����ϴ� ����)
    public bool attackBuff;
    public bool defenseBuff;
    public bool speedBuff;
    ///////////////////
    //ġ��Ÿ�� ��� ��ų�θ� ���� �ϱ⿡ �ٸ� ������ ��ø ����. ���� ȿ���� ���� ����.
    public bool critBuff;

    //debuff
    public bool isStunned;
    public bool isPoisoned;
    public bool isSkillSealed;
    public bool isTired; //fatique�� �ִ�ġ �϶�. //���ݷ��� maxAtk�� 1/2�� ����.


    public int jobNum; // 0:���� 1:������ 2:��Ŀ 3:����
    public Item equipedWeapon;
    public Item equipedAcc;

    //��ų �̸�
    public string skill1Name;
    public string skill2Name;
    public string skill3Name;
    public string skill4Name;
    //��ų �ڽ�Ʈ
    public int skill1Cost;
    public int skill2Cost;
    public int skill3Cost;
    public int skill4Cost;

    public GameObject singleTarget;
    public List<GameObject> multiTarget;
    public GameObject damagePrint; //������ ��¿� ������Ʈ

    public PlayerSkill normalAttack;
    public GameObject skillEffect1;//�ӽ÷� 1�� ���� �� ������, ���� 1~4���� ���� instantiate�� �����ʿ�.

    public void resetStat()//���߿� �ʿ� ���� ������ �ȵǴ� ���ȵ� ����� �α� (�����ʿ����� �⺻ �̼��� 2/3�� ��)
    {
        maxHp = originalMaxHp;
        maxMp = originalMaxMp;
        atk = originalAtk;
        def = originalDef;
        spd = originalSpd;
        crit = originalCrit;
        fatigue = 0;
    }
    
    virtual public void Attack()
    {
        //���� ���⼭ normalAttack�� �����ϰԲ� ����.
        Debug.Log("����");
    }
    virtual public void Skill1(Transform trans)
    {
        Debug.Log("��ų");
    }
    virtual public void Skill2(Transform trans)
    {
        Debug.Log("��ų");
    }
    virtual public void Skill3(Transform trans)
    {
        Debug.Log("��ų");
    }
    virtual public void Skill4(Transform trans)
    {
        Debug.Log("��ų");
    }

    virtual public void SkillDmgCalc1()
    {

    }
    virtual public void SkillDmgCalc2()
    {

    }
    virtual public void SkillDmgCalc3()
    {

    }
    virtual public void SkillDmgCalc4()
    {

    }



    virtual public float CheckCrit(float atkDMG,int critPercent)
    {
        if (Random.Range(0, 100) < critPercent)
        {
            Debug.Log("ũ��Ƽ�� ������!");
            return atkDMG * 1.5f;
        }
        else
            Debug.Log("�Ϲ� ������");
            return atkDMG;
    }
    virtual public bool IsCritical(float atkDMG, float DMG)
    {
        if (atkDMG * 1.5f == DMG)
            return true;
        else
            return false;
    }
    virtual public void ResetBUff()
    {
        attackBuff = false;
        defenseBuff = false;
        speedBuff = false;
        critBuff = false;
        isSkillSealed = false;
        isStunned = false;

    }
}

