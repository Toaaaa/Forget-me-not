using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{


    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk, this.crit) - CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Def;
        Debug.Log("힐러의 기본 공격");
    }
    override public void Skill1()//단일 회복
    {
        Debug.Log("단일 회복");
        CombatManager.Instance.selectedPlayer.hp += this.atk *1.5f;
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
            CombatManager.Instance.playerList[i].hp += this.atk;
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
    {
        CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp*0.5f;
        CombatManager.Instance.selectedPlayer.isDead = false;
        CombatManager.Instance.selectedPlayer.isStunned = false;
        CombatManager.Instance.selectedPlayer.isPoisoned = false;
    }



}

