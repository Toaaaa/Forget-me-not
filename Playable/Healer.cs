using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{


    override public void Attack()
    {
        float critatk = CheckCrit(atk, this.crit);
        TestMob monster = CombatManager.Instance.monsterSelected.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
        }
        Debug.Log("힐러의 기본 공격");
    }
    override public void Skill1()//단일 회복
    {
        Debug.Log("단일 회복");
        CombatManager.Instance.selectedPlayer.hp += this.atk *3f;
        if(CombatManager.Instance.selectedPlayer.hp > CombatManager.Instance.selectedPlayer.maxHp)
        {
            CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp;
        }
    }
    override public void Skill2()//광역 회복 //Earth Blessing
    {
        Debug.Log("광역 회복");
        for(int i =0; i<CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].hp += this.atk*2;
            if (CombatManager.Instance.playerList[i].hp > CombatManager.Instance.playerList[i].maxHp)
            {
                CombatManager.Instance.playerList[i].hp = CombatManager.Instance.playerList[i].maxHp;
            }
        }
    }
    override public void Skill3() //큐어.
    {
        CombatManager.Instance.selectedPlayer.isPoisoned = false;
    }
    override public void Skill4() //레저렉션.
    {//중 정도의 코스트에 모든 마나 소모. (기본적으로 1스킬 = 1마나)
        if(CombatManager.Instance.selectedPlayer.isDead)
        {
            CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp*0.5f;
            CombatManager.Instance.selectedPlayer.isDead = false;
            CombatManager.Instance.selectedPlayer.isStunned = false;
            CombatManager.Instance.selectedPlayer.isPoisoned = false;
        }
        else
        {
            CombatManager.Instance.selectedPlayer.hp += CombatManager.Instance.selectedPlayer.maxHp * 0.5f;
            if (CombatManager.Instance.selectedPlayer.hp > CombatManager.Instance.selectedPlayer.maxHp)
            {
                CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp;
            }
            Debug.Log("이미 살아있는 대상, 절반의 체력 회복.");
        }
    }



}

