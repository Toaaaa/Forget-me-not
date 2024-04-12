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
        Debug.Log("������ �⺻ ����");
    }
    override public void Skill1()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk * 2;
        Debug.Log("���ϰ� ����ġ��");
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


