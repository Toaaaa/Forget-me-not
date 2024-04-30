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
        Debug.Log("������ �⺻ ����");
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
        Debug.Log("������ ��ų2");
    }
    override public void Skill3()
    {
        Debug.Log("������ ��ų3");
    }
    override public void Skill4()
    {
        Debug.Log("������ ��ų4");
    }
}



/*[System.Serializable]
public class skillWarrior //�̰��� ����� �ְ�. ���� override public void Skill ���� ȣ�� �ϸ� �ɵ�.
{
    public void skill1()
    {
        Debug.Log("������ ��ų1");
    }
    public void skill2()
    {
        Debug.Log("������ ��ų2");
    }
    public void skill3()
    {
        Debug.Log("������ ��ų3");
    }
    public void skill4()
    {
        Debug.Log("������ ��ų4");
    }
}*/


