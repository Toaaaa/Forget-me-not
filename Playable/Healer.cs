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
        Debug.Log("���� ȸ��");
        CombatManager.Instance.selectedPlayer.hp += this.atk *3f;
        if(CombatManager.Instance.selectedPlayer.hp > CombatManager.Instance.selectedPlayer.maxHp)
        {
            CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp;
        }
    }
    override public void Skill2(Transform trans)//���� ȸ�� //Earth Blessing
    {
        Debug.Log("���� ȸ��");
        for(int i =0; i<CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].hp += this.atk*2;
            if (CombatManager.Instance.playerList[i].hp > CombatManager.Instance.playerList[i].maxHp)
            {
                CombatManager.Instance.playerList[i].hp = CombatManager.Instance.playerList[i].maxHp;
            }
        }
    }
    override public void Skill3(Transform trans) //ť��.
    {
        CombatManager.Instance.selectedPlayer.isPoisoned = false;
    }
    override public void Skill4(Transform trans) //��������.
    {//�� ������ �ڽ�Ʈ�� ��� ���� �Ҹ�. (�⺻������ 1��ų = 1����)
        if(CombatManager.Instance.selectedPlayer.isDead)
        {
            CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp*0.5f;
            CombatManager.Instance.selectedPlayer.isDead = false;
            CombatManager.Instance.selectedPlayer.isStunned = false;
            CombatManager.Instance.selectedPlayer.isPoisoned = false;
        }
        else
        {
            CombatManager.Instance.selectedPlayer.hp += CombatManager.Instance.selectedPlayer.maxHp * 0.5f;
            if (CombatManager.Instance.selectedPlayer.hp > CombatManager.Instance.selectedPlayer.maxHp)
            {
                CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp;
            }
            Debug.Log("�̹� ����ִ� ���, ������ ü�� ȸ��.");
        }
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

