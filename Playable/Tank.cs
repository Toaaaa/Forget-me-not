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
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk, this.crit) - CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Def;
        Debug.Log("��Ŀ�� �⺻ ����");
    }
    override public void Skill1()
    {
        if(isDefPlused == false)
        {
            for (int i = 0; i < CombatManager.Instance.playerList.Count; i++)
            {
                CombatManager.Instance.playerList[i].def = CombatManager.Instance.playerList[i].def * 1.1f;
            }
            Debug.Log("���� ����");
            isDefPlused = true;
            //30�� ������ ������ �����Ǵ� �ڷ�ƾ �����ϱ�.(def = originalDef) //�ڷ�ƾ �� �ƴ϶� combatmanager���� ������ �ð��� üũ�ϴ°� ��������..?
        }
        else
        {
            //30�� Ÿ�̸� �ٽ� ����.
        }
    }
    override public void Skill2()
    {
        Debug.Log("��׷�");
        isAggroOn = true;
    }
    override public void Skill3() //���︮��. >>��� ���Ϳ��� ���ݷ¸�ŭ�� �������� �ְ� ������ 5�� ���ҽ�Ŵ.
    { //>> ���� ��ġ�� ��ų�̱⿡ �ڽ�Ʈ ���� �����Ұ�.
        Debug.Log("���︮��");
        for (int i=0; i<CombatManager.Instance.monsterObject.Count; i++)
        {
            CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Hp -= CheckCrit(atk, this.crit) - CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Def;
            CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Def -= 5;
            if (CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Def < 0)
            {
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Def = 0;
            }
        }
    }
    override public void Skill4()
    {
        //�߽�����̱� ������ ��ų�� 3���ۿ� ����.
        //10���� �޼��� ��ų �ر� ��ũ��Ʈ����, "�߽����� ��ų�� 3���ۿ� �����ϴ�." ��� �޼����� ����ָ� �ɵ�.
    }
}

