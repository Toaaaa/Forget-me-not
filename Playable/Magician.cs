using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk , this.crit);
        Debug.Log("�������� �⺻ ����");
    }
    override public void Skill1() //������
    {
        for(int i =0; i<CombatManager.Instance.monsterList.Count; i++) //��� ���Ϳ��� 1.5���� ���ݷ����� ����
        {
            CombatManager.Instance.monsterList[i].Hp -= CheckCrit(atk * 1.5f, this.crit);
        }
    }
    override public void Skill2() //��� �÷��̾�� ġ��Ÿ Ȯ�� ����
    {
        for(int i = 0; i < CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].crit += 15;
            CombatManager.Instance.playerList[i].critBuff = true;
        }
    }
    override public void Skill3() //�ӵ� ����
    {//�ڽ�Ʈ �� (���� �ӵ��� ����Ű��⿡ ����� ����)
        for (int i = 0; i < CombatManager.Instance.monsterObject.Count; i++)
        {
            if (CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isslowed == false)
            {
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Speed -= 3;
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isslowed = true;
            }
            else
            {
                Debug.Log("�̹� �ӵ��� ���ҵǾ� �ֽ��ϴ�.");
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Speed -= 1;
            }
        }
    }
    override public void Skill4() //�Ǿ�� ����Ʈ��. 3���� ���� ����. (���ϱ�) (���� ���ΰ� ���־�)
    { //>>���� ������ ���� �ڽ�Ʈ
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk * 3.5f,this.crit);
    }
    
}

