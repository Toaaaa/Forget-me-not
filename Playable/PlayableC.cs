using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SkillType
{
    Fire,
    Water,
    Wood,
    none,//공격이 아닌 버프,디버프형 스킬의 경우
}

public class PlayableC : ScriptableObject
{

    public GameObject InGamePrefab;//전투 씬에서 존재할 프리팹.
    public GameObject Char_Prefab;
    public Sprite CharIcon;//전투 디스플레이에서의 아이콘 ui
    public Sprite characterImage;
    public Sprite charaterRewardImage;
    public bool isPerson;
    public Sprite RewardImageForCat;
    public int level;
    public float hp; //힐러는 공격력으로 힐을 해서 힐러의 공격력 수치에 주의.
    public float maxHp;
    public float mp;
    public float maxMp;
    public float atk;
    public float def;
    public int spd;
    public int crit; //요 크리티컬 데미지 반영 하려면. 따로 singleton으로 데미지 매니저 만들어서 거기의 함수와 int 파라미터를 받아서 데미지 계산하는 방식으로 하는것도 좋을듯.
    public float exp;
    public float maxExp;
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
    //스킬 설명
    public string skill1Desc;
    public string skill2Desc;
    public string skill3Desc;
    public string skill4Desc;
    //스킬의 기능 설명 (@@@한 능력을 가짐의 실질적인 기능 표시) SkillUseAlarm을 통해 표시.
    public string skill1Func;
    public string skill2Func;
    public string skill3Func;
    public string skill4Func;
    //스킬 코스트
    public int skill1Cost;
    public int skill2Cost;
    public int skill3Cost;
    public int skill4Cost;

    //사용할 스킬의 타입(아이템 등으로 변경될 수도 있는 부분)
    public SkillType normalAttackType;
    public SkillType skill1Type;
    public SkillType skill2Type;
    public SkillType skill3Type;
    public SkillType skill4Type;

    //원래 스킬 타입
    public SkillType Ori_normalAttackType;
    public SkillType Ori_skill1Type;
    public SkillType Ori_skill2Type;
    public SkillType Ori_skill3Type;
    public SkillType Ori_skill4Type;



    public GameObject singleTarget;
    public List<GameObject> multiTarget;

    public GameObject normalAttack;
    public GameObject skillEffect1;//각각의 투사체들 추후 1~4까지 각각 instantiate도 변경필요.
    public GameObject skillEffect2;
    public GameObject skillEffect3;
    public GameObject skillEffect4;

    public void ResetStat()//나중에 맵에 따라서 리셋이 안되는 스탯도 만들어 두기 (설원맵에서는 기본 이속이 2/3로 됨)
    {
        maxHp = originalMaxHp;
        maxMp = originalMaxMp;
        atk = originalAtk;
        def = originalDef;
        spd = originalSpd;
        crit = originalCrit;
        fatigue = 0;
    }
    public void SetElement(SkillType st)//스킬의 속성을 변경하는 함수(하나의 속성으로 세팅하기).
    {
        normalAttackType = st;
        skill1Type = st;
        skill2Type = st;
        skill3Type = st;
        skill4Type = st;
    }
    public void ResetElement()//스킬의 속성을 리셋하는 함수.
    {
        normalAttackType = Ori_normalAttackType;
        skill1Type = Ori_skill1Type;
        skill2Type = Ori_skill2Type;
        skill3Type = Ori_skill3Type;
        skill4Type = Ori_skill4Type;
    }

    
    virtual public void Attack(Transform trans)
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

    virtual public void AttackDmgCalc(GameObject g)
    {

    }
    virtual public void SkillDmgCalc1(GameObject g)
    {

    }
    virtual public void SkillDmgCalc2(GameObject g)
    {

    }
    virtual public void SkillDmgCalc3(GameObject g)
    {

    }
    virtual public void SkillDmgCalc4(GameObject g)
    {

    }
    virtual public void MultiDmg1(PlayableC player,TestMob target,GameObject g)
    {

    }
    virtual public void MultiDmg2(PlayableC player, TestMob target, GameObject g)
    {

    }
    virtual public void MultiDmg3(PlayableC player, TestMob target, GameObject g)
    {

    }
    virtual public void MultiDmg4(PlayableC player, TestMob target, GameObject g)
    {

    }
    virtual public void HolyRayDmgCalc(GameObject g)
    {

    }
    virtual public void LastHolyRayDmgCalc(GameObject g)//홀리레이의 마지막 타격(5번째)의 속성 스택부여
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
    virtual public void LevelUpStat()
    {
        switch(level)
        {
            case 2:
                
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            case 5:
                
                break;
            case 6:
                
                break;
            case 7:
                
                break;
            case 8:
                
                break;
            case 9:
                
                break;
            case 10:
                
                break;
            case 11:
                
                break;
            case 12:
                
                break;
            case 13:
                
                break;
            case 14:
                
                break;
            case 15:
                
                break;
        }

    }
    virtual public int LevelUpEffectInfo()
    {
        return 0;
    }
    public void ElementStack(SkillType thisSkillType, TestMob monster)//공격시의 속성 스택
    {
        if (thisSkillType != SkillType.none)//무속성이 아닐경우 스택 쌓기, 무속성의 경우 그냥 스킵.
        {
            monster.stackedElement = thisSkillType;//몬스터에 쌓이는 속성 스택을 플레이어의 스킬 기반으로 세팅.
            return;
        }
    }
    public float ElementDamage(SkillType thisSkillType, TestMob monster, float critatk)//속성 스택 해당시 추가 데미지
    {
        if (thisSkillType != SkillType.none)
        {
            switch (thisSkillType)
            {
                case SkillType.Fire:
                    if (monster.stackedElement == SkillType.Water)//물 속성 스택의 경우 반감
                    {
                        return critatk * 0.8f;
                    }
                    else if (monster.stackedElement == SkillType.Wood)//나무 속성 스택의 경우 추가데미지.
                    {
                        return critatk * 1.2f;
                    }
                    else//화염속성의 경우
                    {
                        return critatk;
                    }
                case SkillType.Water:
                    if (monster.stackedElement == SkillType.Wood)//나무 속성 스택의 경우 반감
                    {
                        return critatk * 0.8f;
                    }
                    else if (monster.stackedElement == SkillType.Fire)//화염 속성 스택의 경우 추가데미지.
                    {
                        return critatk * 1.2f;
                    }
                    else//물속성의 경우
                    {
                        return critatk;
                    }
                case SkillType.Wood:
                    if (monster.stackedElement == SkillType.Fire)//화염 속성 스택의 경우 반감
                    {
                        return critatk * 0.8f;
                    }
                    else if (monster.stackedElement == SkillType.Water)//물 속성 스택의 경우 추가데미지.
                    {
                        return critatk * 1.2f;
                    }
                    else//나무속성의 경우
                    {
                        return critatk;
                    }
                default:
                    return critatk;
            }
        }
        else//무속성 공격의 경우
        {
            Debug.Log("무속성 데미지");
            return critatk;
        }
    }//elementdamage 를 호출하고 elementstack을 호출해야함.
}


