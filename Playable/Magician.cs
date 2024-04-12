using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= atk;
        Debug.Log("�������� �⺻ ����");
    }
    override public void Skill1()
    {
        for(int i =0; i<CombatManager.Instance.monsterList.Count; i++) //��� ���Ϳ��� 1.5���� ���ݷ����� ����
        {
            CombatManager.Instance.monsterList[i].Hp -= (int)(atk * 1.5f);
        }
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

