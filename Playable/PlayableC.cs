using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayableC : ScriptableObject
{
    public Sprite characterImage;
    public int level;
    public float hp; //힐러는 공격력으로 힐을 해서 힐러의 공격력 수치에 주의.
    public float maxHp;
    public float mp;
    public float maxMp;
    public float atk;
    public float def;
    public int spd;
    public int crit; //요 크리티컬 데미지 반영 하려면. 따로 singleton으로 데미지 매니저 만들어서 거기의 함수와 int 파라미터를 받아서 데미지 계산하는 방식으로 하는것도 좋을듯.
    public int exp;
    public int maxExp;
    public int priority;
    public float fatigue;
    public float maxFatigue;

    public bool isDead;

    //오리지널 스텟 //전투가 끝나면 해당 스텟으로 리셋해주기.
    public float originalMaxHp;
    public float originalMaxMp;
    public float originalAtk;
    public float originalDef;
    public int originalSpd;
    public int originalCrit;

    //buff
    public bool isBuffed;
    ///////////////////아래의 3 버프는 중첩 불가능. (소모 아이템으로 사용하는 버프)
    public bool attackBuff;
    public bool defenseBuff;
    public bool speedBuff;
    ///////////////////
    //치명타의 경우 스킬로만 증가 하기에 다른 버프와 중첩 가능. 따라서 효과도 따로 적용.
    public bool critBuff;

    //debuff
    public bool isStunned;
    public bool isPoisoned;
    public bool isSkillSealed;
    public bool isTired; //fatique가 최대치 일때. //공격력이 maxAtk의 1/2로 감소.


    public int jobNum; // 0:전사 1:마법사 2:탱커 3:힐러
    public Item equipedWeapon;
    public Item equipedAcc;

    //스킬 이름
    public string skill1Name;
    public string skill2Name;
    public string skill3Name;
    public string skill4Name;
    //스킬 코스트
    public int skill1Cost;
    public int skill2Cost;
    public int skill3Cost;
    public int skill4Cost;

    public GameObject singleTarget;
    public List<GameObject> multiTarget;
    public GameObject damagePrint; //데미지 출력용 오브젝트

    public PlayerSkill normalAttack;
    public GameObject skillEffect1;//임시로 1만 만들어서 다 쓰는중, 추후 1~4까지 각각 instantiate도 변경필요.

    public void resetStat()//나중에 맵에 따라서 리셋이 안되는 스탯도 만들어 두기 (설원맵에서는 기본 이속이 2/3로 됨)
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
        //추후 여기서 normalAttack을 실행하게끔 수정.
        Debug.Log("공격");
    }
    virtual public void Skill1(Transform trans)
    {
        Debug.Log("스킬");
    }
    virtual public void Skill2(Transform trans)
    {
        Debug.Log("스킬");
    }
    virtual public void Skill3(Transform trans)
    {
        Debug.Log("스킬");
    }
    virtual public void Skill4(Transform trans)
    {
        Debug.Log("스킬");
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
            Debug.Log("크리티컬 데미지!");
            return atkDMG * 1.5f;
        }
        else
            Debug.Log("일반 데미지");
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

