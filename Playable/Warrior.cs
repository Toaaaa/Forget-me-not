using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Warrior", menuName = "PlayableCharacter/Warrior")]
public class Warrior : PlayableC
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
        Debug.Log("전사의 기본 공격");
    }
    override public void Skill1()
    {
        float critatk = CheckCrit(atk, this.crit);
        TestMob monster = CombatManager.Instance.monsterSelected.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
        }
        else
        {
            monster.Hp -= critatk*2f - monster.Def;
        }
    }
    override public void Skill2()
    {
        Debug.Log("전사의 스킬2");
    }
    override public void Skill3()
    {
        Debug.Log("전사의 스킬3");
    }
    override public void Skill4()
    {
        Debug.Log("전사의 스킬4");
    }
}



/*[System.Serializable]
public class skillWarrior //이곳에 기능을 넣고. 위의 override public void Skill 에서 호출 하면 될듯.
{
    public void skill1()
    {
        Debug.Log("전사의 스킬1");
    }
    public void skill2()
    {
        Debug.Log("전사의 스킬2");
    }
    public void skill3()
    {
        Debug.Log("전사의 스킬3");
    }
    public void skill4()
    {
        Debug.Log("전사의 스킬4");
    }
}*/


