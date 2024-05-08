using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity, CombatManager.Instance.mobplace.transform);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans) //������
    {
        Debug.Log("������");
        /*
        multiTarget = CombatManager.Instance.monsterAliveList;
        for(int i=0; i<multiTarget.Count; i++)
        {
            var obj =Instantiate(skillEffect1, trans.transform.position, Quaternion.identity,CombatManager.Instance.mobplace.transform);
            obj.GetComponent<TestProjectile>().targetMob = multiTarget[i].GetComponent<TestMob>();
            obj.GetComponent<TestProjectile>().targetLocked();
        }*/ //���� Ÿ�� ���ݽ� projectile�� Ÿ�� ������ ����ϴ� �ڵ�.
        for(int i =0; i<CombatManager.Instance.monsterList.Count; i++) //��� ���Ϳ��� 1.5���� ���ݷ����� ����
        {
            float temp = CheckCrit(atk * 1.5f, this.crit);
            Debug.Log(temp);
            Debug.Log("���� �������� :" + atk * 1.5f + "�Դϴ�.");
            CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Hp -= temp;
        }
    }
    override public void Skill2(Transform trans) //��� �÷��̾�� ġ��Ÿ Ȯ�� ���� //Sharpening accuracy
    {
        if (CombatManager.Instance.playerList[0].critBuff)
        {
            Debug.Log("ġ��Ÿ ������ �̹� ������ �Դϴ�.");
            return;
        }
        for(int i = 0; i < CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].crit += 15;
            CombatManager.Instance.playerList[i].critBuff = true;
        }
    }
    override public void Skill3(Transform trans) //�ӵ� ���� //�ð� �񵿱�ȭ
    {//�ڽ�Ʈ ��(���� �ӵ��� ����Ű��⿡ ����� ����)
        multiTarget = CombatManager.Instance.monsterAliveList;
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
    override public void Skill4(Transform trans) //�Ǿ�� ����Ʈ��. 3���� ���� ����. (���ϱ�) (���� ���ΰ� ���־�)
    { //>>���� ������ ���� �ڽ�Ʈ
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk * 3.5f,this.crit);
    }


    public override void AttackDmgCalc()
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        monster.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, critatk, isCrit,false);
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

