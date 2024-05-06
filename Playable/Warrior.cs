using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Warrior", menuName = "PlayableCharacter/Warrior")]
public class Warrior : PlayableC
{

    override public void Attack()
    {
        float critatk = CheckCrit(atk, this.crit);
        TestMob monster = CombatManager.Instance.monsterSelected.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
        }
        else
        {
            monster.Hp -= critatk - monster.Def;
        }
        Debug.Log("전사의 기본 공격");
    }
    override public void Skill1(Transform trans)
    {
        var obj=Instantiate(skillEffect1, trans.transform.position, Quaternion.identity,CombatManager.Instance.mobplace.transform);
        obj.GetComponent<TestProjectile>().player = this;
        obj.GetComponent<TestProjectile>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<TestProjectile>().targetLocked();
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
    
    override public void SkillDmgCalc1()
    {
        float critatk = CheckCrit(atk, this.crit);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        if (monster.Def >= critatk)
        {
            monster.Hp -= 1;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, 1);
        }
        else
        {
            monster.Hp -= critatk * 2f - monster.Def;
            CombatManager.Instance.damagePrintManager.PrintDamage(monster.transform.position, critatk * 2f - monster.Def);
        }
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


