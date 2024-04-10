using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        switch (skillNum)
        {
            case 1:
                //공격력 강화
                strongAttack(mob);
                break;
            case 2:
                //체력 회복
                hpRegen(mob);
                break;
            case 3:
                allAttackDebuff();
                break;
            case 4:
                WideAttack(mob);
                break;
            case 5:
                Debug.Log("스킬5 사용");
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



    private void strongAttack(TestMob mob) //공격력 강화
    {
        mob.Atk += 10;
        Debug.Log(skillName);
    }
    private void hpRegen(TestMob mob) //체력 회복
    {
        mob.Hp += 10;
        Debug.Log(skillName);
    }
    private void allAttackDebuff() //모든 플레이어 공격력 감소
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
    private void WideAttack(TestMob mob) //모든 플레이어에게 공격력의 2배만큼 데미지를 줌.
    {
        for(int i=0; i<CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].hp -= 2*mob.Atk;
        }
    }
    private void PoisonAttack(TestMob mob) //모든 플레이어에게  중독 상태 부여.
    {
        mob.target.isPoisoned = true;
        mob.target.hp -= 10;
    }
}
