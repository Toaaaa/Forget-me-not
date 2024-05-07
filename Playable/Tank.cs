using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "PlayableCharacter/Tank")]
public class Tank : PlayableC
{
    public bool isDefPlused;//���� ���� ��ų ����.
    public bool isAggroOn;//��Ŀ�� ��׷ν�ų ������.


    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity, CombatManager.Instance.mobplace.transform);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans)
    {
        if(isDefPlused == false)
        {
            /*for (int i = 0; i < CombatManager.Instance.playerList.Count; i++)
            {
                CombatManager.Instance.playerList[i].def += 5;
            }*/
            this.def += 5;
            Debug.Log("���� ����");
            isDefPlused = true;
            //30�� ������ ������ �����Ǵ� �ڷ�ƾ �����ϱ�.(def = originalDef) //�ڷ�ƾ �� �ƴ϶� combatmanager���� ������ �ð��� üũ�ϴ°� ��������..?
        }
        else
        {
            //30�� Ÿ�̸� �ٽ� ����.
        }
    }
    override public void Skill2(Transform trans)
    {
        Debug.Log("��׷�");
        isAggroOn = true;
        CombatManager.Instance.isAggroOn = true;
    }
    override public void Skill3(Transform trans) //���︮��. >>��� ���Ϳ��� ���ݷ¸�ŭ�� �������� �ְ� ������ 5�� ���ҽ�Ŵ.
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
    override public void Skill4(Transform trans)
    {
        //�߽�����̱� ������ ��ų�� 3���ۿ� ����.
        //10���� �޼��� ��ų �ر� ��ũ��Ʈ����, "�߽����� ��ų�� 3���ۿ� �����ϴ�." ��� �޼����� ����ָ� �ɵ�.
    }

    override public void ResetBUff()
    {
        isDefPlused = false;
        isAggroOn = false;
    }

    public override void AttackDmgCalc()
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, 1, isCrit);
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, critatk - monster.Def, isCrit);
        }
    }
    override public void SkillDmgCalc1()
    {

    }
    override public void SkillDmgCalc2()
    {

    }
    override public void SkillDmgCalc3()
    {

    }
    override public void SkillDmgCalc4()
    {

    }
}

