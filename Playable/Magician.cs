using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
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
        for(int i =0; i<CombatManager.Instance.monsterObject.Count; i++) //��� ���Ϳ��� 1.5���� ���ݷ����� ����
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
        for (int i = 0; i < CombatManager.Instance.monsterAliveList.Count; i++)
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


    public override void AttackDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        monster.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk, isCrit,false);
        Destroy(g);
    }
    override public void SkillDmgCalc1(GameObject g)
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

    override public void LevelUpStat()//�������� ��� ���ݷ� ���� ������ ����.
    {
        switch (level)//2�������� 15���������� �������� ���� ������
        {
            case 2:
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 1;
                break;
            case 3:
                originalAtk += 1;
                break;
            case 4:
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 1;
                break;
            case 5:
                originalAtk += 1;
                originalSpd += 2;
                break;
            case 6:
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 1;
                break;
            case 7:
                originalAtk += 1;
                break;
            case 8:
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 1;
                break;
            case 9:
                originalAtk += 1;
                break;
            case 10: //10������ ������2
                originalAtk += 2;
                originalSpd += 2;
                originalDef += 2;
                originalMaxHp += 2;
                break;
            case 11:
                originalAtk += 1;
                break;
            case 12:
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 1;
                break;
            case 13:
                originalAtk += 1;
                break;
            case 14:
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 1;
                break;
            case 15:
                originalAtk += 1;
                originalSpd += 2;
                break;
            default:
                break;

        }
        //1���� ���� ����
        //atk: 6, def: 6, hp: 15, spd: 4, crit: 5
    }
    public override int LevelUpEffectInfo()//atk�� a, def�� hp�� b, spd�� c�� �ش���
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

