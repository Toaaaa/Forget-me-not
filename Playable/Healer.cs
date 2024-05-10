using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{


    override public void Attack(Transform trans)
    {
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity, CombatManager.Instance.mobplace.transform);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans)//���� ȸ��
    {
        var obj = Instantiate(skillEffect1, trans.transform.position, Quaternion.identity, CombatManager.Instance.combatDisplay.transform);
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
            var obj = Instantiate(skillEffect2, trans.transform.position, Quaternion.identity, CombatManager.Instance.combatDisplay.transform);
            obj.GetComponent<HealSkill2>().player = this;
            obj.GetComponent<HealSkill2>().targetPlayer = CombatManager.Instance.playerList[i];
            obj.GetComponent<HealSkill2>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[i];
            obj.GetComponent<HealSkill2>().targetLocked();
        }
    }
    override public void Skill3(Transform trans) //ť��.
    {
        var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity, CombatManager.Instance.combatDisplay.transform);
        obj.GetComponent<HealSkill3>().player = this;
        obj.GetComponent<HealSkill3>().targetPlayer = CombatManager.Instance.selectedPlayer;
        obj.GetComponent<HealSkill3>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[CombatManager.Instance.combatDisplay.selectedSlotIndex];
        obj.GetComponent<HealSkill3>().targetLocked();
    }
    override public void Skill4(Transform trans) //��������.
    {//�� ������ �ڽ�Ʈ�� ��� ���� �Ҹ�. (�⺻������ 1��ų = 1����)
        var obj = Instantiate(skillEffect4, trans.transform.position, Quaternion.identity, CombatManager.Instance.combatDisplay.transform);
        obj.GetComponent<HealSkill4>().player = this;
        obj.GetComponent<HealSkill4>().targetPlayer = CombatManager.Instance.selectedPlayer;
        obj.GetComponent<HealSkill4>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[CombatManager.Instance.combatDisplay.selectedSlotIndex];
        obj.GetComponent<HealSkill4>().targetLocked();

    }

    public override void AttackDmgCalc()
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, 1, isCrit,false);
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, critatk - monster.Def, isCrit, false);
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

    private float WhenMaxHpPrint(PlayableC player) //������ �ִ� ü���� �Ѿ��, �󸶳� ȸ���Ǵ��� ���.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}

