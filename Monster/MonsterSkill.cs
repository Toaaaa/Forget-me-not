using System.Collections;
using System.Collections.Generic;
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
            case 1:
                //���ݷ� ��ȭ
                strongAttack(mob);
                break;
            case 2:
                //ü�� ȸ��
                hpRegen(mob);
                break;
            case 3:
                Debug.Log("��ų3 ���");
                break;
            case 4:
                Debug.Log("��ų4 ���");
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
}
