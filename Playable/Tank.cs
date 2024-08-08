using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "PlayableCharacter/Tank")]
public class Tank : PlayableC
{
    public Animator TankSkillAnim;
    public bool isDefPlused;//방어력 증가 스킬 사용시.
    public bool isAggroOn;//탱커의 어그로스킬 적용중.


    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans)
    {
        if(isDefPlused == false)
        {
            /*for (int i = 0; i < CombatManager.Instance.playerList.Count; i++)
            {
                CombatManager.Instance.playerList[i].def += 5;
            }*/
            this.def += 5;
            Debug.Log("방어력 증가");
            isDefPlused = true;
            //30초 지나면 버프가 해제되는 코루틴 실행하기.(def = originalDef) //코루틴 이 아니라 combatmanager에서 변수로 시간을 체크하는게 나을수도..?
        }
        else
        {
            //30초 타이머 다시 시작.
        }
    }
    override public void Skill2(Transform trans)
    {
        Debug.Log("어그로");
        isAggroOn = true;
        CombatManager.Instance.isAggroOn = true;
    }
    override public void Skill3(Transform trans) //땅울리기. >>모든 몬스터에게 공격력만큼의 데미지를 주고 방어력을 4씩 감소시킴.
    { //>> 높은 가치의 스킬이기에 코스트 높게 설정할것.
        Debug.Log("땅울리기");
        for (int i=0; i<CombatManager.Instance.monsterAliveList.Count; i++)
        {
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<TankSkill3>().player = this;
            obj.GetComponent<TankSkill3>().targetMob = CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>();
            obj.GetComponent<TankSkill3>().targetLocked();

            CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>().Def -= 4;
            if (CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>().Def < 0)
            {
                CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>().Def = 0;
            }
        }
    }
    override public void Skill4(Transform trans)
    {
        //견습기사이기 때문에 스킬이 3개밖에 없음.
        //10레벨 달성시 스킬 해금 스크립트에서, "견습기사는 스킬이 3개밖에 없습니다." 라는 메세지를 띄워주면 될듯.
    }

    override public void ResetBUff()
    {
        isDefPlused = false;
        isAggroOn = false;
    }

    public override void AttackDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(normalAttackType, monster, critatk);//속성 데미지 계산.
        ElementStack(normalAttackType, monster);//속성 스택 쌓기.
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1, isCrit, false);
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk - monster.Def, isCrit, false);
        }
        Destroy(g);
    }
    override public void SkillDmgCalc1(GameObject g)
    {

    }
    override public void SkillDmgCalc2(GameObject g)
    {

    }
    override public void SkillDmgCalc3(GameObject g)
    {
               
    }
    override public void SkillDmgCalc4(GameObject g)
    {

    }

    override public void MultiDmg3(PlayableC player, TestMob mob)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);

        critatk = ElementDamage(skill3Type, mob, critatk);//속성 데미지 계산.
        ElementStack(skill3Type, mob);//속성 스택 쌓기.

        mob.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(mob.thisSlot.gameObject.transform.position, critatk, isCrit, false);
    }

    override public void LevelUpStat()//탱커의 경우 어그로 스킬이 있기에 방어력 체력수치 조정에 유의.
    {
        switch (level)//2레벨부터 15레벨까지의 레벨업시 스텟 증가량
        {
            case 2://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 3://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 4://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 5://bc
                originalDef += 1;
                originalMaxHp += 2;
                originalSpd += 1;
                break;
            case 6://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 7://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 8://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 9://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 10: //10레벨때 전스텟 2씩 증가//abc
                originalAtk += 2;
                spd += 2;
                originalDef += 2;
                originalMaxHp += 2;
                break;
            case 11://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 12://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 13://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 14://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 15://bc
                originalDef += 1;
                originalMaxHp += 2;
                spd += 1;
                break;
            default:
                break;

        }
        //1레벨 시작 스텟
        //atk: 5, def: 15, hp: 22, spd:3, crit: 0
    }
    public override int LevelUpEffectInfo()//a:1,b:2,ab:3,ac:4,bc:5,abc:6
    {
        switch (level)
        {
            case 2:
                return 3;
            case 3:
                return 2;
            case 4:
                return 3;
            case 5:
                return 5;
            case 6:
                return 3;
            case 7:
                return 2;
            case 8:
                return 3;
            case 9:
                return 2;
            case 10:
                return 6;
            case 11:
                return 2;
            case 12:
                return 3;
            case 13:
                return 2;
            case 14:
                return 3;
            case 15:
                return 5;
             default:
                return 0;

        }
    }
    //LevelUpEffectInfo()는 레벨업시 atk만 올랏을 경우 1. def,hp만 올랏을 경우 2. atk,def,hp 올랏을 경우 3.atk,spd가 올랐을 경우 4. def,hp,spd가 올랏을 경우 5. atk,def,hp,spd가 올랏을 경우 6. 이런식으로 리턴값을 주어서 레벨업시 어떤 스탯이 올랏는지 알수 있게 해줌.
}

