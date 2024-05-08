using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity, CombatManager.Instance.mobplace.transform);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans) //블레이즈
    {
        Debug.Log("블레이즈");
        /*
        multiTarget = CombatManager.Instance.monsterAliveList;
        for(int i=0; i<multiTarget.Count; i++)
        {
            var obj =Instantiate(skillEffect1, trans.transform.position, Quaternion.identity,CombatManager.Instance.mobplace.transform);
            obj.GetComponent<TestProjectile>().targetMob = multiTarget[i].GetComponent<TestMob>();
            obj.GetComponent<TestProjectile>().targetLocked();
        }*/ //다중 타겟 공격시 projectile의 타겟 설정에 사용하는 코드.
        for(int i =0; i<CombatManager.Instance.monsterList.Count; i++) //모든 몬스터에게 1.5배의 공격력으로 공격
        {
            float temp = CheckCrit(atk * 1.5f, this.crit);
            Debug.Log(temp);
            Debug.Log("공격 데미지는 :" + atk * 1.5f + "입니다.");
            CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Hp -= temp;
        }
    }
    override public void Skill2(Transform trans) //모든 플레이어들 치명타 확률 증가 //Sharpening accuracy
    {
        if (CombatManager.Instance.playerList[0].critBuff)
        {
            Debug.Log("치명타 버프가 이미 적용중 입니다.");
            return;
        }
        for(int i = 0; i < CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].crit += 15;
            CombatManager.Instance.playerList[i].critBuff = true;
        }
    }
    override public void Skill3(Transform trans) //속도 감소 //시간 비동기화
    {//코스트 상(적의 속도를 감소키기기에 밸류가 높음)
        multiTarget = CombatManager.Instance.monsterAliveList;
        for (int i = 0; i < CombatManager.Instance.monsterObject.Count; i++)
        {
            if (CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isslowed == false)
            {
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Speed -= 3;
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isslowed = true;
            }
            else
            {
                Debug.Log("이미 속도가 감소되어 있습니다.");
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Speed -= 1;
            }
        }
    }
    override public void Skill4(Transform trans) //피어싱 라이트닝. 3연속 관통 공격. (단일기) (빛의 봉인검 비주얼)
    { //>>높은 데미지 높은 코스트
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk * 3.5f,this.crit);
    }


    public override void AttackDmgCalc()
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        monster.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, critatk, isCrit,false);
    }
    override public void SkillDmgCalc1()
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
}

