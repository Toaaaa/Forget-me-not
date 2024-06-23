using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Warrior", menuName = "PlayableCharacter/Warrior")]
public class Warrior : PlayableC
{

    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans)//���⼭�� ����ü�� ����.
    {
        var obj=Instantiate(skillEffect1, trans.transform.position, Quaternion.identity);
        obj.GetComponent<WarriorSkill1>().player = this;
        obj.GetComponent<WarriorSkill1>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<WarriorSkill1>().targetLocked();
    }
    override public void Skill2(Transform trans)
    {
        Debug.Log("������ ��ų2");
    }
    override public void Skill3(Transform trans)
    {
        Debug.Log("������ ��ų3");
    }
    override public void Skill4(Transform trans)
    {
        Debug.Log("������ ��ų4");
    }

    public override void AttackDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1,isCrit, false);
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk - monster.Def,isCrit, false);
        }
        Destroy(g);
    }
    override public void SkillDmgCalc1(GameObject g)//���⼭ ����ü�� �ǰݽ� ������ �����.
    {
        float critatk = CheckCrit(atk, this.crit); //������ ��꿡 ġ��Ÿ ����.
        bool isCrit = IsCritical(critatk, atk); // �ش� �������� ġ��Ÿ���� Ȯ��.
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1,isCrit, false);
        }
        else
        {
            monster.Hp -= critatk * 2f - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk * 2f - monster.Def, isCrit, false);
        }
        Destroy(g);
    }
    override public void SkillDmgCalc2()
    {
        Debug.Log("������ ��ų2 ������ ���");
    }
    override public void SkillDmgCalc3()
    {
        Debug.Log("������ ��ų3 ������ ���");
    }
    override public void SkillDmgCalc4()
    {
        Debug.Log("������ ��ų4 ������ ���");
    }

    override public void LevelUpStat()//������ ��� ���ݷ� ���� ������ ����.
    {
        switch (level)//2�������� 15���������� �������� ���� ������
        {
            case 2:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 3:
                atk += 2;
                break;
            case 4:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 5:
                atk += 2;
                spd += 2;
                break;
            case 6:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 7:
                atk += 2;
                break;
            case 8:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 9:
                atk += 2;
                break;
            case 10: //10������ def,hp�� 1�� �ƴ� 2 ����.
                atk += 2;
                spd += 2;
                def += 2;
                hp += 2;
                break;
            case 11:
                atk += 2;
                break;
            case 12:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 13:
                atk += 2;
                break;
            case 14:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 15:
                atk += 2;
                spd += 2;
                break;
            default:
                break;

        }
    }

    public override int LevelUpEffectInfo()//a:1,b:2,ab:3,ac:4,bc:5,abc:6
    {
        switch (level)
        {
            case 2:
                return 3;
            case 3:
                return 1;
            case 4:
                return 3;
            case 5:
                return 4;
            case 6:
                return 3;
            case 7:
                return 1;
            case 8:
                return 3;
            case 9:
                return 1;
            case 10:
                return 6;
            case 11:
                return 1;
            case 12:
                return 3;
            case 13:
                return 1;
            case 14:
                return 3;
            case 15:
                return 4;
            default:
                return 0;
        }
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


