using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "PlayableCharacter/Tank")]
public class Tank : PlayableC
{
    public bool isDefPlused;//���� ���� ��ų ����.
    public bool isAggroOn;//��Ŀ�� ��׷ν�ų ������.

    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk;
        Debug.Log("��Ŀ�� �⺻ ����");
    }
    override public void Skill1()
    {
        this.def = def * 2;
        Debug.Log("���� ����");
    }
    override public void Skill2()
    {
        Debug.Log("��׷�");
    }
    override public void Skill3()
    {

    }
    override public void Skill4()
    {

    }
}

