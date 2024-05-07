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
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity, CombatManager.Instance.mobplace.transform);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans)//���⼭�� ����ü�� ����.
    {
        var obj=Instantiate(skillEffect1, trans.transform.position, Quaternion.identity,CombatManager.Instance.mobplace.transform);
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

    public override void AttackDmgCalc()
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, 1,isCrit);
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, critatk - monster.Def,isCrit);
        }
    }
    override public void SkillDmgCalc1()//���⼭ ����ü�� �ǰݽ� ������ �����.
    {
        float critatk = CheckCrit(atk, this.crit); //������ ��꿡 ġ��Ÿ ����.
        bool isCrit = IsCritical(critatk, atk); // �ش� �������� ġ��Ÿ���� Ȯ��.
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, 1,isCrit);
        }
        else
        {
            monster.Hp -= critatk * 2f - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, critatk * 2f - monster.Def, isCrit);
        }
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


