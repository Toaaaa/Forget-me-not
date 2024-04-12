using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "PlayableCharacter/Tank")]
public class Tank : PlayableC
{
    public bool isDefPlused;//방어력 증가 스킬 사용시.
    public bool isAggroOn;//탱커의 어그로스킬 적용중.

    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk;
        Debug.Log("탱커의 기본 공격");
    }
    override public void Skill1()
    {
        this.def = def * 2;
        Debug.Log("방어력 증가");
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

