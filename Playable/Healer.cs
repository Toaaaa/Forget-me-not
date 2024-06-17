using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{


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
        Debug.Log("Ȧ�� ���� �ǰ�");
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
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
    public override void HolyRayDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk*0.5f, this.crit); //������ ��꿡 ġ��Ÿ ����.
        bool isCrit = IsCritical(critatk, atk); // �ش� �������� ġ��Ÿ���� Ȯ��.
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
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
        Debug.Log("Ȧ�� ���� �������� :" + critatk + "�Դϴ�.");
        Destroy(g);
    }
    override public void SkillDmgCalc4()
    {
        
    }


    public async void StartSpawning(Transform trans)
    {
        await SpawnSkillEffectRepeatedly(trans, 5, 200); //0.2�� �������� 5�� �ݶ��̴��� �߻�
    }

    private async UniTask SpawnSkillEffectRepeatedly(Transform trans, int repetitions, int delayMilliseconds)//0.2�� �������� 5�� �ݶ��̴��� �߻��� �Լ�
    {
        for (int i = 0; i < repetitions; i++)
        {
            Debug.Log("Ȧ�� ���� �߻�");
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<HealSkill3>().player = this;
            obj.GetComponent<HealSkill3>().targetMob = this.singleTarget.GetComponent<TestMob>();
            obj.GetComponent<HealSkill3>().targetLocked();
            await UniTask.Delay(delayMilliseconds);
        }
    }

    private float WhenMaxHpPrint(PlayableC player) //������ �ִ� ü���� �Ѿ��, �󸶳� ȸ���Ǵ��� ���.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}

