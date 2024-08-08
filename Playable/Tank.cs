using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "PlayableCharacter/Tank")]
public class Tank : PlayableC
{
    public Animator TankSkillAnim;
    public bool isDefPlused;//���� ���� ��ų ����.
    public bool isAggroOn;//��Ŀ�� ��׷ν�ų ������.


    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
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
    override public void Skill3(Transform trans) //���︮��. >>��� ���Ϳ��� ���ݷ¸�ŭ�� �������� �ְ� ������ 4�� ���ҽ�Ŵ.
    { //>> ���� ��ġ�� ��ų�̱⿡ �ڽ�Ʈ ���� �����Ұ�.
        Debug.Log("���︮��");
        for (int i=0; i<CombatManager.Instance.monsterAliveList.Count; i++)
        {
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<TankSkill3>().player = this;
            obj.GetComponent<TankSkill3>().targetMob = CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>();
            obj.GetComponent<TankSkill3>().targetLocked();

            CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>().Def -= 4;
            if (CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>().Def < 0)
            {
                CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>().Def = 0;
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

    public override void AttackDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(normalAttackType, monster, critatk);//�Ӽ� ������ ���.
        ElementStack(normalAttackType, monster);//�Ӽ� ���� �ױ�.
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1, isCrit, false);
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk - monster.Def, isCrit, false);
        }
        Destroy(g);
    }
    override public void SkillDmgCalc1(GameObject g)
    {

    }
    override public void SkillDmgCalc2(GameObject g)
    {

    }
    override public void SkillDmgCalc3(GameObject g)
    {
               
    }
    override public void SkillDmgCalc4(GameObject g)
    {

    }

    override public void MultiDmg3(PlayableC player, TestMob mob)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);

        critatk = ElementDamage(skill3Type, mob, critatk);//�Ӽ� ������ ���.
        ElementStack(skill3Type, mob);//�Ӽ� ���� �ױ�.

        mob.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(mob.thisSlot.gameObject.transform.position, critatk, isCrit, false);
    }

    override public void LevelUpStat()//��Ŀ�� ��� ��׷� ��ų�� �ֱ⿡ ���� ü�¼�ġ ������ ����.
    {
        switch (level)//2�������� 15���������� �������� ���� ������
        {
            case 2://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 3://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 4://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 5://bc
                originalDef += 1;
                originalMaxHp += 2;
                originalSpd += 1;
                break;
            case 6://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 7://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 8://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 9://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 10: //10������ ������ 2�� ����//abc
                originalAtk += 2;
                spd += 2;
                originalDef += 2;
                originalMaxHp += 2;
                break;
            case 11://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 12://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 13://b
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 14://ab
                originalAtk += 1;
                originalDef += 1;
                originalMaxHp += 2;
                break;
            case 15://bc
                originalDef += 1;
                originalMaxHp += 2;
                spd += 1;
                break;
            default:
                break;

        }
        //1���� ���� ����
        //atk: 5, def: 15, hp: 22, spd:3, crit: 0
    }
    public override int LevelUpEffectInfo()//a:1,b:2,ab:3,ac:4,bc:5,abc:6
    {
        switch (level)
        {
            case 2:
                return 3;
            case 3:
                return 2;
            case 4:
                return 3;
            case 5:
                return 5;
            case 6:
                return 3;
            case 7:
                return 2;
            case 8:
                return 3;
            case 9:
                return 2;
            case 10:
                return 6;
            case 11:
                return 2;
            case 12:
                return 3;
            case 13:
                return 2;
            case 14:
                return 3;
            case 15:
                return 5;
             default:
                return 0;

        }
    }
    //LevelUpEffectInfo()�� �������� atk�� �ö��� ��� 1. def,hp�� �ö��� ��� 2. atk,def,hp �ö��� ��� 3.atk,spd�� �ö��� ��� 4. def,hp,spd�� �ö��� ��� 5. atk,def,hp,spd�� �ö��� ��� 6. �̷������� ���ϰ��� �־ �������� � ������ �ö����� �˼� �ְ� ����.
}

