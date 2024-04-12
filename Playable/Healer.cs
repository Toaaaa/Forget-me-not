using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{

    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk;
        Debug.Log("힐러의 기본 공격");
    }
    override public void Skill1()
    {
        CombatManager.Instance.combatDisplay.selectedSlot.player.hp += 10;
        if(CombatManager.Instance.combatDisplay.selectedSlot.player.hp > CombatManager.Instance.combatDisplay.selectedSlot.player.maxHp)
        {
            CombatManager.Instance.combatDisplay.selectedSlot.player.hp = CombatManager.Instance.combatDisplay.selectedSlot.player.maxHp;
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

