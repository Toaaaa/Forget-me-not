using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

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
        Debug.Log("���Ͱ� ������ �Ͽ����ϴ�" + "��ų ��ȣ : "+skillNum);
        Animator anim = mob.GetComponent<Animator>();
        switch (skillNum)
        {
            case 0://0����ų
                   //(0�� ��ų�� ���) Ư�� ���� ��ų.
                break;
            case 1://1����ų(�Ϲ����� ����1)
                anim.SetInteger("skillNum", 1);
                mob.GetComponent<MonsterAnimatorController>().Attack("ATK1").Forget();//�� �ȿ� attacking�� true�ϴ� �ڵ� ����.
                break;
            case 2://2����ų(�Ϲ����� ����2)
                anim.SetInteger("skillNum", 2);
                mob.GetComponent<MonsterAnimatorController>().Attack("ATK2").Forget();
                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

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

    public void NormalAttack(TestMob mob) //�Ϲ� ����
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

    public void strongAttack(TestMob mob) //���ݷ� ��ȭ
    {
        mob.Atk += 10;
        Debug.Log(skillName);
    }
    public void hpRegen(TestMob mob) //ü�� ȸ��
    {
        mob.Hp += 10;
        Debug.Log(skillName);
    }
    public void allAttackDebuff() //��� �÷��̾� ���ݷ� ����
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
    public void WideAttack(TestMob mob) //��� �÷��̾�� ���ݷ��� 1�踸ŭ �������� ��.
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
    public void DoubleAttack(TestMob mob) //Ÿ�� �÷��̾�� 2����ŭ �������� ��.
    {
            if (mob.target.def >= mob.Atk)
            {
                mob.target.hp -= 1;
            }
            else
            {
                mob.target.hp -= mob.Atk * 1f - mob.target.def;
                mob.target.hp -= mob.Atk * 1f - mob.target.def;
            }
    }

    public void PoisonAttack(TestMob mob) //��� �÷��̾��  �ߵ� ���� �ο�.
    {
        mob.target.isPoisoned = true;
        mob.target.hp -= 10;
    }
    public void skillSeal(TestMob mob) //��� �÷��̾��� ��ų ����.
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
        CombatManager.Instance.monsterAttackManager.isAttacking = false;
    }

}
