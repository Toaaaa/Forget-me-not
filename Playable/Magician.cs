using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack(Transform trans)
    {
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK0",0).Forget();//���� �ִϸ��̼� ���
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx0();//�⺻ ���� sfx ���
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans) //������//��� ���Ϳ��� 1.5���� ���ݷ����� ����
    {
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK1",1).Forget();//��ų1 �ִϸ��̼� ���
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx1();//��ų1 sfx ���
        for (int i = 0; i < CombatManager.Instance.monsterAliveList.Count; i++)
        {
            var obj = Instantiate(skillEffect1, trans.transform.position, Quaternion.identity);
            obj.GetComponent<MagiSkill1>().player = this;
            obj.GetComponent<MagiSkill1>().targetMob = CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>();
            obj.GetComponent<MagiSkill1>().targetLocked();
        }
    }
    override public void Skill2(Transform trans) //���� ���� + ������ �ð�����(�� �ӵ� ���� ���� ���� ���� ����)�� ����Ͽ� ������ + ���� ����
    { 
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK2",2).Forget();//��ų2 �ִϸ��̼� ���
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx2();//��ų2 sfx ���
        var obj = Instantiate(skillEffect2, trans.transform.position, Quaternion.identity);
        obj.GetComponent<MagiSkill2>().player = this;
        obj.GetComponent<MagiSkill2>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<MagiSkill2>().targetLocked();
    }
    override public void Skill3(Transform trans) //�ӵ� ���� //�ð� �񵿱�ȭ
    {//�ڽ�Ʈ ��(���� �ӵ��� ����Ű��⿡ ����� ����)
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK3",3).Forget();//��ų3 �ִϸ��̼� ���
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx3();//��ų3 sfx ���
        for (int i = 0; i < CombatManager.Instance.monsterAliveList.Count; i++)
        {
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<MagiSkill3>().player = this;
            obj.GetComponent<MagiSkill3>().targetMob = CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>();
            obj.GetComponent<MagiSkill3>().targetLocked();
        }
    }
    override public void Skill4(Transform trans) //�Ǿ�� ����Ʈ��. 3���� ���� ����. (���ϱ�) ���� ��������� �ٴ���Ʈ 3������ ������ �����Ҽ���(���־����� ������)(Ȧ������ ����)
    { //>>���� ������ ���� �ڽ�Ʈ
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK4",4).Forget();//��ų3 �ִϸ��̼� ���
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4();//��ų4 sfx ���
        var obj = Instantiate(skillEffect4, trans.transform.position, Quaternion.identity);
        obj.GetComponent<MagiSkill4>().player = this;
        obj.GetComponent<MagiSkill4>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<MagiSkill4>().targetLocked();
        TripleLighting(obj);//����ü �̵��ð� 1.6���� ,  0.4�� 0.8�� 1.2�� ������ �̵���λ� ���� õ�� sfx ��� �ϴ� �Լ�,
    }


    //DmgCalc ���� ��ų�� �������� ����Ǵ� ��� ����.
    public override void AttackDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(normalAttackType, monster, critatk);//�Ӽ� ������ ���.
        ElementStack(normalAttackType, monster);//�Ӽ� ���� �ױ�.

        monster.TakeDamage();//�ǰݽ� ��¦�̴� ȿ��
        monster.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject, critatk, isCrit,false);
        Destroy(g);
    }
    override public void SkillDmgCalc1(GameObject g)
    {

    }
    override public void SkillDmgCalc2(GameObject g)//3�� ��ų�� ���� �ӵ� ������ �߰� �������� ����.
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(skill2Type, monster, critatk);//�Ӽ� ������ ���.
        ElementStack(skill2Type, monster);//�Ӽ� ���� �ױ�.

        float stack = TimeStack(monster);//������ �ð� ���� ��� ������;
        monster.slowStack = 0;//���� �ʱ�ȭ

        monster.TakeDamage();//�ǰݽ� ��¦�̴� ȿ��
        monster.Hp -= critatk *(1.5f+ stack);//�⺻ ������ 2�� + ����(�ִ�4ȸ)��ŭ �߰� ������
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject, critatk *stack, isCrit, false);
        Destroy(g);
    }
    override public void SkillDmgCalc3(GameObject g)
    {

    }
    override public void SkillDmgCalc4(GameObject g)//�Ǿ�� ����Ʈ�� ������ ���.
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(skill4Type, monster, critatk);//�Ӽ� ������ ���.
        ElementStack(skill4Type, monster);//�Ӽ� ���� �ױ�.

        monster.TakeDamage();//�ǰݽ� ��¦�̴� ȿ��
        monster.Hp -= critatk * 3.5f;
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject, critatk * 3.5f, isCrit, false);
        Destroy(g);
    }
    override public void MultiDmg1(PlayableC player, TestMob mob)//�������� ��� ���� ���� ���� Ʈ�絥����.
    {
        float critatk = CheckCrit(atk, this.crit);//������ ġ��Ÿ ����
        bool isCrit = IsCritical(critatk, atk);

        critatk = ElementDamage(skill1Type, mob, critatk);//���������� �Ӽ� ������ ���.
        ElementStack(skill1Type, mob);//�Ӽ� ���� �ױ�.

        mob.TakeDamage();//�ǰݽ� ��¦�̴� ȿ��
        mob.Hp -= critatk * 1.5f;
        CombatManager.Instance.damagePrintManager.PrintDamage(mob.thisSlot.gameObject, critatk * 1.5f, isCrit, false);
    }
    override public void MultiDmg3(PlayableC player, TestMob mob)//�ð� ����� �̱⶧���� �Ӽ� ������ ���� �Ҽ� �ִ� �ּ����� ������ 1 �� ����.
    {
        float critatk = CheckCrit(atk, this.crit);//������ ġ��Ÿ ����
        bool isCrit = IsCritical(critatk, atk);
        isCrit =false;//�ð� ������� �ּ� �������� �����ϱ⿡ ġ��Ÿ ���� only �Ӽ� �߰� ��������..
        critatk = ElementDamage(skill3Type, mob, 1);//�⺻������ 1�� ������� �Ӽ� ������ ���.
        ElementStack(skill3Type, mob);//�Ӽ� ���� �ױ�.

        TimeAsynchronization(mob);//������ speed ���� ȿ��.
        mob.TakeDamage();//�ǰݽ� ��¦�̴� ȿ��
        mob.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(mob.thisSlot.gameObject, critatk, isCrit, false);//�ּ� ������ 1
    }
    public void TimeAsynchronization(TestMob mob)//��ų 3�� �ӵ� ����� ȿ��//slowstack�� �ִ� 4������
    {
         if (mob.isslowed == false)
         {
            mob.Speed -= 3;
            mob.slowStack++;
         }
         else
         {
            Debug.Log("�̹� �ӵ��� ���ҵǾ� �ֽ��ϴ�.");
            mob.Speed -= 1;
            mob.slowStack++;
         }
         if (mob.slowStack >= 4)
         {
             mob.slowStack = 4;
         }//�ִ� 4 ���� ������ ����
    }
    public float TimeStack(TestMob monster)
    {
        int speedStack = monster.slowStack;
        return (float)speedStack;
    }

    private async void TripleLighting(GameObject obj)//�Ǿ�� ����Ʈ���� ���� Ÿ���� ��ο� 3��(0.4,0.8,1.2�ʸ���) ���� ������ ��ȯ�ϴ� ȿ��
    {
        /*
        for(int i=0; i<3; i++)
        {
            await UniTask.Delay(400);
            InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(i, obj.transform);//����ü �̵��߿� ���� ȿ��
        }*/
        await UniTask.Delay(800);
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(0, obj.transform);//����ü �̵��߿� ���� ȿ��
        await UniTask.Delay(200);
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(1, obj.transform);//����ü �̵��߿� ���� ȿ��
        await UniTask.Delay(200);
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(2, obj.transform);//����ü �̵��߿� ���� ȿ��

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

