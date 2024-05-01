using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New monsterskill", menuName = "skill/monster")]
public class MonsterSkill : ScriptableObject
{
    public List<skills> skillList; //�̰� �ʿ���� �ѵ�... ������ ���� �Ǵ� ��ų�� testmob�� ����ִ� monsterSkill�̴�.
}

[System.Serializable]
public class skills
{
    public int skillNum;
    public int skillValue; //��ų�� ����Ҷ� �Ҹ�Ǵ� �ð���.
    public string skillName;
    public string skillDesc;

   
    

    public void UseSkill(TestMob mob)
    {
        switch (skillNum)
        {
            case 0:
                //�Ϲ� ����
                NormalAttack(mob);
                break;
            case 1:
                //���ݷ� ��ȭ
                strongAttack(mob);
                break;
            case 2:
                //ü�� ȸ��
                hpRegen(mob);
                break;
            case 3:
                allAttackDebuff();
                break;
            case 4:
                WideAttack(mob);
                break;
            case 5:
                Debug.Log("��ų5 ���");
                break;
            case 6:
                Debug.Log("��ų6 ���");
                break;
            case 7:
                Debug.Log("��ų7 ���");
                break;
            case 8:
                Debug.Log("��ų8 ���");
                break;
            case 9:
                Debug.Log("��ų9 ���");
                break;
            case 10:
                Debug.Log("��ų10 ���");
                break;
            default:
                Debug.Log("��ų�� ����� �� �����ϴ�.");
                break;
        }
    }

    private void NormalAttack(TestMob mob) //�Ϲ� ����
    {
        if(mob.target.def >= mob.Atk)
        {
            mob.target.hp -= 1;
        }
        else
        {
            mob.target.hp -= mob.Atk - mob.target.def;
        }
        mob.target.hp -= mob.Atk;
        Debug.Log(skillName);
    }

    private void strongAttack(TestMob mob) //���ݷ� ��ȭ
    {
        mob.Atk += 10;
        Debug.Log(skillName);
    }
    private void hpRegen(TestMob mob) //ü�� ȸ��
    {
        mob.Hp += 10;
        Debug.Log(skillName);
    }
    private void allAttackDebuff() //��� �÷��̾� ���ݷ� ����
    {
        if (CombatManager.Instance.isAtkDebuff)//�̹� �ش� ��ų�� ���� ���� ���.
            //�ٸ� ���ݽ�ų�� ��� ����ϰ� �ϱ�.
            return;
        for(int i=0; i<CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].atk -= 10;
            CombatManager.Instance.isAtkDebuff = true;
        }
    }
    private void WideAttack(TestMob mob) //��� �÷��̾�� ���ݷ��� 1�踸ŭ �������� ��.
    {
        for(int i=0; i<CombatManager.Instance.playerList.Count; i++)
        {
            mob.target = CombatManager.Instance.playerList[i];
            if (mob.target.def >= mob.Atk*1f)
            {
                mob.target.hp -= 1;
            }
            else
            {
                mob.target.hp -= mob.Atk*1f - mob.target.def;
            }
        }
    }
    private void PoisonAttack(TestMob mob) //��� �÷��̾��  �ߵ� ���� �ο�.
    {
        mob.target.isPoisoned = true;
        mob.target.hp -= 10;
    }
    private void skillSeal(TestMob mob) //��� �÷��̾��� ��ų ����.
    {
        mob.target.isSkillSealed = true;
    }


    ///////Ư�� ���� ��ų

    public void BattleCry(TestMob mob) //������ �Լ� (���ݷ� 1.5��, ü�� 30%ȸ��)
    {
        float atk;
        for(int i=0; i<CombatManager.Instance.monsterObject.Count; i++)
        {
            atk = CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Atk;
            CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Atk = Mathf.Round(atk*1.5f);
            if (!CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isDead)
            {
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Hp += CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().MaxHp*0.3f;
            }
        }
        
    }



}
