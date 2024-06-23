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
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans)//여기서는 투사체의 구현.
    {
        var obj=Instantiate(skillEffect1, trans.transform.position, Quaternion.identity);
        obj.GetComponent<WarriorSkill1>().player = this;
        obj.GetComponent<WarriorSkill1>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<WarriorSkill1>().targetLocked();
    }
    override public void Skill2(Transform trans)
    {
        Debug.Log("전사의 스킬2");
    }
    override public void Skill3(Transform trans)
    {
        Debug.Log("전사의 스킬3");
    }
    override public void Skill4(Transform trans)
    {
        Debug.Log("전사의 스킬4");
    }

    public override void AttackDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1,isCrit, false);
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk - monster.Def,isCrit, false);
        }
        Destroy(g);
    }
    override public void SkillDmgCalc1(GameObject g)//여기서 투사체의 피격시 데미지 계산방법.
    {
        float critatk = CheckCrit(atk, this.crit); //데미지 계산에 치명타 연산.
        bool isCrit = IsCritical(critatk, atk); // 해당 데미지가 치명타인지 확인.
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, 1,isCrit, false);
        }
        else
        {
            monster.Hp -= critatk * 2f - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject.transform.position, critatk * 2f - monster.Def, isCrit, false);
        }
        Destroy(g);
    }
    override public void SkillDmgCalc2()
    {
        Debug.Log("전사의 스킬2 데미지 계산");
    }
    override public void SkillDmgCalc3()
    {
        Debug.Log("전사의 스킬3 데미지 계산");
    }
    override public void SkillDmgCalc4()
    {
        Debug.Log("전사의 스킬4 데미지 계산");
    }

    override public void LevelUpStat()//전사의 경우 공격력 증가 위주의 스탯.
    {
        switch (level)//2레벨부터 15레벨까지의 레벨업시 스텟 증가량
        {
            case 2:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 3:
                atk += 2;
                break;
            case 4:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 5:
                atk += 2;
                spd += 2;
                break;
            case 6:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 7:
                atk += 2;
                break;
            case 8:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 9:
                atk += 2;
                break;
            case 10: //10레벨때 def,hp를 1이 아닌 2 증가.
                atk += 2;
                spd += 2;
                def += 2;
                hp += 2;
                break;
            case 11:
                atk += 2;
                break;
            case 12:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 13:
                atk += 2;
                break;
            case 14:
                atk += 2;
                def += 1;
                hp += 1;
                break;
            case 15:
                atk += 2;
                spd += 2;
                break;
            default:
                break;

        }
    }

    public override int LevelUpEffectInfo()//a:1,b:2,ab:3,ac:4,bc:5,abc:6
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




/*[System.Serializable]
public class skillWarrior //이곳에 기능을 넣고. 위의 override public void Skill 에서 호출 하면 될듯.
{
    public void skill1()
    {
        Debug.Log("전사의 스킬1");
    }
    public void skill2()
    {
        Debug.Log("전사의 스킬2");
    }
    public void skill3()
    {
        Debug.Log("전사의 스킬3");
    }
    public void skill4()
    {
        Debug.Log("전사의 스킬4");
    }
}*/


