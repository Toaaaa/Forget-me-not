using System.Collections;
using System.Collections.Generic;
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
    override public void Skill3(Transform trans) //땅울리기. >>모든 몬스터에게 공격력만큼의 데미지를 주고 방어력을 5씩 감소시킴.
    { //>> 높은 가치의 스킬이기에 코스트 높게 설정할것.
        Debug.Log("땅울리기");
        for (int i=0; i<CombatManager.Instance.monsterAliveList.Count; i++)
        {
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<TankSkill3>().player = this;
            obj.GetComponent<TankSkill3>().targetMob = CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>();
            obj.GetComponent<TankSkill3>().targetLocked();

            CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>().Def -= 5;
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
    override public void SkillDmgCalc2()
    {

    }
    override public void SkillDmgCalc3()
    {
               
    }
    override public void SkillDmgCalc4()
    {

    }

    override public void MultiDmg3(PlayableC player, TestMob mob)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);

        mob.Hp -= CheckCrit(atk, this.crit);
        CombatManager.Instance.damagePrintManager.PrintDamage(mob.thisSlot.gameObject.transform.position, critatk, isCrit, false);
    }
}

