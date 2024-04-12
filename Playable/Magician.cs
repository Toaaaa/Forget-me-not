using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk;
        Debug.Log("마법사의 기본 공격");
    }
    override public void Skill1()
    {
        for(int i =0; i<CombatManager.Instance.monsterList.Count; i++) //모든 몬스터에게 1.5배의 공격력으로 공격
        {
            CombatManager.Instance.monsterList[i].Hp -= (int)(atk * 1.5f);
        }
    }
    override public void Skill2()
    {
        Debug.Log("어그로");
    }
    override public void Skill3()
    {

    }
    override public void Skill4()
    {

    }
}

