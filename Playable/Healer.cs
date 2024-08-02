using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{
    public GameObject skillEffect3Last;

    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans)//���� ȸ��
    {
        var obj = Instantiate(skillEffect1, trans.transform.position, Quaternion.identity);
        obj.GetComponent<HealSkill1>().player = this;
        obj.GetComponent<HealSkill1>().targetPlayer = CombatManager.Instance.selectedPlayer;
        obj.GetComponent<HealSkill1>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[CombatManager.Instance.combatDisplay.selectedSlotIndex];
        obj.GetComponent<HealSkill1>().targetLocked();

    }
    override public void Skill2(Transform trans)//���� ȸ�� //Earth Blessing
    {
        Debug.Log("���� ȸ��");
        for(int i =0; i<CombatManager.Instance.playerList.Count; i++)
        {
            var obj = Instantiate(skillEffect2, trans.transform.position, Quaternion.identity);
            obj.GetComponent<HealSkill2>().player = this;
            obj.GetComponent<HealSkill2>().targetPlayer = CombatManager.Instance.playerList[i];
            obj.GetComponent<HealSkill2>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[i];
            obj.GetComponent<HealSkill2>().targetLocked();
        }
    }
    override public void Skill3(Transform trans) //Ȧ�� ���� (5�� ���� ����,0.1�ʿ� �ѹ���)
    {
        StartSpawning(trans);
    }
    override public void Skill4(Transform trans) //��������.
    {//�� ������ �ڽ�Ʈ�� ��� ���� �Ҹ�. (�⺻������ 1��ų = 1����)
        var obj = Instantiate(skillEffect4, trans.transform.position, Quaternion.identity);
        obj.GetComponent<HealSkill4>().player = this;
        obj.GetComponent<HealSkill4>().targetPlayer = CombatManager.Instance.selectedPlayer;
        obj.GetComponent<HealSkill4>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[CombatManager.Instance.combatDisplay.selectedSlotIndex];
        obj.GetComponent<HealSkill4>().targetLocked();

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
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1, isCrit,false);
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
    override public void SkillDmgCalc2()
    {
        
    }
    public override void HolyRayDmgCalc(GameObject g)//Ȧ�������� 1~4��° Ÿ���� ������ ���
    {
        float critatk = CheckCrit(atk*0.5f, this.crit); //������ ��꿡 ġ��Ÿ ����.
        bool isCrit = IsCritical(critatk, atk); // �ش� �������� ġ��Ÿ���� Ȯ��.
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(skill3Type, monster, critatk);//�Ӽ� ������ ���.
        //1~4��° Ÿ���� �Ӽ� ���� �ױ� ����.
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1, isCrit, false);
        }
        else
        {
            monster.Hp -= critatk * 2f - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk * 2f - monster.Def, isCrit, false);
        }
        Destroy(g);
    }
    public override void LastHolyRayDmgCalc(GameObject g)//Ȧ�������� ������ Ÿ��(5��°)�� �Ӽ� ���úο�
    {
        float critatk = CheckCrit(atk * 0.5f, this.crit); //������ ��꿡 ġ��Ÿ ����.
        bool isCrit = IsCritical(critatk, atk); // �ش� �������� ġ��Ÿ���� Ȯ��.
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(skill3Type, monster, critatk);//�Ӽ� ������ ���.
        ElementStack(skill3Type, monster);//�Ӽ� ���� �ױ�.
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1, isCrit, false);
        }
        else
        {
            monster.Hp -= critatk * 2f - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk * 2f - monster.Def, isCrit, false);
        }
        Destroy(g);
    }
    override public void SkillDmgCalc4()
    {
        
    }


    public async void StartSpawning(Transform trans)
    {
        await SpawnSkillEffectRepeatedly(trans, 4, 200); //0.2�� �������� 4��+������1�� �ݶ��̴��� �߻�
    }

    private async UniTask SpawnSkillEffectRepeatedly(Transform trans, int repetitions, int delayMilliseconds)//0.2�� �������� 5�� �ݶ��̴��� �߻��� �Լ�
    {
        for (int i = 0; i < repetitions; i++)
        {
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<HealSkill3>().player = this;
            obj.GetComponent<HealSkill3>().targetMob = this.singleTarget.GetComponent<TestMob>();
            obj.GetComponent<HealSkill3>().targetLocked();
            await UniTask.Delay(delayMilliseconds);
        }
        //������ Ÿ��
        var objL = Instantiate(skillEffect3Last, trans.transform.position, Quaternion.identity);
        objL.GetComponent<HealSkill3Last>().player = this;
        objL.GetComponent<HealSkill3Last>().targetMob = this.singleTarget.GetComponent<TestMob>();
        objL.GetComponent<HealSkill3Last>().targetLocked();
    }
    private float WhenMaxHpPrint(PlayableC player) //������ �ִ� ü���� �Ѿ��, �󸶳� ȸ���Ǵ��� ���.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }

    override public void LevelUpStat()//������ ��� ������ ���ݷ� ����̱⿡ ���ݷ� ��ġ ����.
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
        //atk: 3, def: 8, hp: 15, spd: 3, crit: 5
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

