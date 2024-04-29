using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{


    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk, this.crit) - CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Def;
        Debug.Log("������ �⺻ ����");
    }
    override public void Skill1()//���� ȸ��
    {
        Debug.Log("���� ȸ��");
        CombatManager.Instance.selectedPlayer.hp += this.atk *1.5f;
        if(CombatManager.Instance.selectedPlayer.hp > CombatManager.Instance.selectedPlayer.maxHp)
        {
            CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp;
        }
    }
    override public void Skill2()//���� ȸ�� //Earth Blessing
    {
        Debug.Log("���� ȸ��");
        for(int i =0; i<CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].hp += this.atk;
            if (CombatManager.Instance.playerList[i].hp > CombatManager.Instance.playerList[i].maxHp)
            {
                CombatManager.Instance.playerList[i].hp = CombatManager.Instance.playerList[i].maxHp;
            }
        }
    }
    override public void Skill3() //ť��.
    {
        CombatManager.Instance.selectedPlayer.isPoisoned = false;
    }
    override public void Skill4() //��������.
    {
        CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp*0.5f;
        CombatManager.Instance.selectedPlayer.isDead = false;
        CombatManager.Instance.selectedPlayer.isStunned = false;
        CombatManager.Instance.selectedPlayer.isPoisoned = false;
    }



}

