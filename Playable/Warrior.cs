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
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk;
        Debug.Log("전사의 기본 공격");
    }
    override public void Skill1()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk * 2;
        Debug.Log("강하게 내려치기");
    }
    override public void Skill2()
    {
        
    }
    override public void Skill3()
    {
        
    }
    override public void Skill4()
    {
        
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


