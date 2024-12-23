using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(fileName = "New monsterskill", menuName = "skill/monster")]
public class MonsterSkill : ScriptableObject
{
    public List<skills> skillList; //이거 필요없긴 한데... 실제로 적용 되는 스킬은 testmob이 들고있는 monsterSkill이다.
}

[System.Serializable]
public class skills
{
    public int skillNum;
    public int skillValue; //스킬을 사용할때 소모되는 시간값.
    public string skillName;
    public string skillDesc;

   
    

    public void UseSkill(TestMob mob)
    {
        Animator anim = mob.GetComponent<Animator>();
        switch (skillNum)
        {
            case 0://0번스킬
                   //(0번 스킬의 경우) 특수 패턴 스킬.
                break;
            case 1://1번스킬(일반적인 공격1)
                anim.SetInteger("skillNum", 1);
                mob.GetComponent<MonsterAnimatorController>().Attack("ATK1").Forget();//이 안에 attacking을 true하는 코드 있음.
                break;
            case 2://2번스킬(일반적인 공격2)
                anim.SetInteger("skillNum", 2);
                mob.GetComponent<MonsterAnimatorController>().Attack("ATK2").Forget();
                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:
                Debug.Log("스킬6 사용");
                break;
            case 7:
                Debug.Log("스킬7 사용");
                break;
            case 8:
                Debug.Log("스킬8 사용");
                break;
            case 9:
                Debug.Log("스킬9 사용");
                break;
            case 10:
                Debug.Log("스킬10 사용");
                break;
            default:
                Debug.Log("스킬을 사용할 수 없습니다.");
                break;
        }
    }

    public void NormalAttack(TestMob mob) //일반 공격
    {
        if(mob.target.def >= mob.Atk)
        {
            mob.target.hp -= 1;
        }
        else
        {
            mob.target.hp -= mob.Atk - mob.target.def;
        }
        mob.target.hp -= mob.Atk;
    }

    public void strongAttack(TestMob mob) //공격력 강화
    {
        mob.Atk += 10;
        Debug.Log(skillName);
    }
    public void hpRegen(TestMob mob) //체력 회복
    {
        mob.Hp += 10;
    }
    public void allAttackDebuff() //모든 플레이어 공격력 감소
    {
        if (CombatManager.Instance.isAtkDebuff)//이미 해당 스킬이 적용 중인 경우.
            //다른 공격스킬을 대신 사용하게 하기.
            return;
        for(int i=0; i<CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].atk -= 10;
            CombatManager.Instance.isAtkDebuff = true;
        }
    }
    public void WideAttack(TestMob mob) //모든 플레이어에게 공격력의 1배만큼 데미지를 줌.
    {
        for(int i=0; i<CombatManager.Instance.playerList.Count; i++)
        {
            mob.target = CombatManager.Instance.playerList[i];
            if (mob.target.def >= mob.Atk*1f)
            {
                mob.target.hp -= 1;
            }
            else
            {
                mob.target.hp -= mob.Atk*1f - mob.target.def;
            }
        }
    }
    public void DoubleAttack(TestMob mob) //타겟 플레이어에게 2번만큼 데미지를 줌.
    {
            if (mob.target.def >= mob.Atk)
            {
                mob.target.hp -= 1;
            }
            else
            {
                mob.target.hp -= mob.Atk * 1f - mob.target.def;
                mob.target.hp -= mob.Atk * 1f - mob.target.def;
            }
    }

    public void PoisonAttack(TestMob mob) //모든 플레이어에게  중독 상태 부여.
    {
        mob.target.isPoisoned = true;
        mob.target.hp -= 10;
    }
    public void skillSeal(TestMob mob) //모든 플레이어의 스킬 봉인.
    {
        mob.target.isSkillSealed = true;
    }


    ///////특수 패턴 스킬

    public void BattleCry(TestMob mob) //전투의 함성 (공격력 1.5배, 체력 30%회복)
    {
        float atk;
        for(int i=0; i<CombatManager.Instance.monsterObject.Count; i++)
        {
            atk = CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Atk;
            CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Atk = Mathf.Round(atk*1.5f);
            if (!CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isDead)
            {
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Hp += CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().MaxHp*0.3f;
            }
        }
        CombatManager.Instance.monsterAttackManager.isAttacking = false;
    }

}
