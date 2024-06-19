using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SkillType
{
    Fire,
    Water,
    Wood,
    none,//������ �ƴ� ����,������� ��ų�� ���
}

public class PlayableC : ScriptableObject
{
    

    public GameObject Char_Prefab;
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
    //��ų ����
    public string skill1Desc;
    public string skill2Desc;
    public string skill3Desc;
    public string skill4Desc;
    //��ų �ڽ�Ʈ
    public int skill1Cost;
    public int skill2Cost;
    public int skill3Cost;
    public int skill4Cost;

    //����� ��ų�� Ÿ��(������ ������ ����� ���� �ִ� �κ�)
    public SkillType normalAttackType;
    public SkillType skill1Type;
    public SkillType skill2Type;
    public SkillType skill3Type;
    public SkillType skill4Type;

    //���� ��ų Ÿ��
    public SkillType Ori_normalAttackType;
    public SkillType Ori_skill1Type;
    public SkillType Ori_skill2Type;
    public SkillType Ori_skill3Type;
    public SkillType Ori_skill4Type;



    public GameObject singleTarget;
    public List<GameObject> multiTarget;

    public GameObject normalAttack;
    public GameObject skillEffect1;//������ ����ü�� ���� 1~4���� ���� instantiate�� �����ʿ�.
    public GameObject skillEffect2;
    public GameObject skillEffect3;
    public GameObject skillEffect4;

    public void ResetStat()//���߿� �ʿ� ���� ������ �ȵǴ� ���ȵ� ����� �α� (�����ʿ����� �⺻ �̼��� 2/3�� ��)
    {
        maxHp = originalMaxHp;
        maxMp = originalMaxMp;
        atk = originalAtk;
        def = originalDef;
        spd = originalSpd;
        crit = originalCrit;
        fatigue = 0;
    }
    public void SetElement(SkillType st)//��ų�� �Ӽ��� �����ϴ� �Լ�(�ϳ��� �Ӽ����� �����ϱ�).
    {
        normalAttackType = st;
        skill1Type = st;
        skill2Type = st;
        skill3Type = st;
        skill4Type = st;
    }
    public void ResetElement()//��ų�� �Ӽ��� �����ϴ� �Լ�.
    {
        normalAttackType = Ori_normalAttackType;
        skill1Type = Ori_skill1Type;
        skill2Type = Ori_skill2Type;
        skill3Type = Ori_skill3Type;
        skill4Type = Ori_skill4Type;
    }

    
    virtual public void Attack(Transform trans)
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

    virtual public void AttackDmgCalc(GameObject g)
    {

    }
    virtual public void SkillDmgCalc1(GameObject g)
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
    virtual public void MultiDmg1(PlayableC player,TestMob target)
    {

    }
    virtual public void MultiDmg2(PlayableC player, TestMob target)
    {

    }
    virtual public void MultiDmg3(PlayableC player, TestMob target)
    {

    }
    virtual public void MultiDmg4(PlayableC player, TestMob target)
    {

    }
    virtual public void HolyRayDmgCalc(GameObject g)
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


