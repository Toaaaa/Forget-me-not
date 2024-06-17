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
    override public void Skill1(Transform trans)//단일 회복
    {
        var obj = Instantiate(skillEffect1, trans.transform.position, Quaternion.identity);
        obj.GetComponent<HealSkill1>().player = this;
        obj.GetComponent<HealSkill1>().targetPlayer = CombatManager.Instance.selectedPlayer;
        obj.GetComponent<HealSkill1>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[CombatManager.Instance.combatDisplay.selectedSlotIndex];
        obj.GetComponent<HealSkill1>().targetLocked();

    }
    override public void Skill2(Transform trans)//광역 회복 //Earth Blessing
    {
        Debug.Log("광역 회복");
        for(int i =0; i<CombatManager.Instance.playerList.Count; i++)
        {
            var obj = Instantiate(skillEffect2, trans.transform.position, Quaternion.identity);
            obj.GetComponent<HealSkill2>().player = this;
            obj.GetComponent<HealSkill2>().targetPlayer = CombatManager.Instance.playerList[i];
            obj.GetComponent<HealSkill2>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[i];
            obj.GetComponent<HealSkill2>().targetLocked();
        }
    }
    override public void Skill3(Transform trans) //홀리 레이 (5번 연속 공격,0.1초에 한번씩)
    {
        StartSpawning(trans);
    }
    override public void Skill4(Transform trans) //레저렉션.
    {//중 정도의 코스트에 모든 마나 소모. (기본적으로 1스킬 = 1마나)
        var obj = Instantiate(skillEffect4, trans.transform.position, Quaternion.identity);
        obj.GetComponent<HealSkill4>().player = this;
        obj.GetComponent<HealSkill4>().targetPlayer = CombatManager.Instance.selectedPlayer;
        obj.GetComponent<HealSkill4>().targetplayerPlace = CombatManager.Instance.combatDisplay.slotList[CombatManager.Instance.combatDisplay.selectedSlotIndex];
        obj.GetComponent<HealSkill4>().targetLocked();

    }

    public override void AttackDmgCalc(GameObject g)
    {
        Debug.Log("홀리 레이 피격");
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
        float critatk = CheckCrit(atk*0.5f, this.crit); //데미지 계산에 치명타 연산.
        bool isCrit = IsCritical(critatk, atk); // 해당 데미지가 치명타인지 확인.
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
        Debug.Log("홀리 레이 데미지는 :" + critatk + "입니다.");
        Destroy(g);
    }
    override public void SkillDmgCalc4()
    {
        
    }


    public async void StartSpawning(Transform trans)
    {
        await SpawnSkillEffectRepeatedly(trans, 5, 200); //0.2초 간격으로 5번 콜라이더를 발사
    }

    private async UniTask SpawnSkillEffectRepeatedly(Transform trans, int repetitions, int delayMilliseconds)//0.2초 간격으로 5번 콜라이더를 발사할 함수
    {
        for (int i = 0; i < repetitions; i++)
        {
            Debug.Log("홀리 레이 발사");
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<HealSkill3>().player = this;
            obj.GetComponent<HealSkill3>().targetMob = this.singleTarget.GetComponent<TestMob>();
            obj.GetComponent<HealSkill3>().targetLocked();
            await UniTask.Delay(delayMilliseconds);
        }
    }

    private float WhenMaxHpPrint(PlayableC player) //힐량이 최대 체력을 넘어갈때, 얼마나 회복되는지 출력.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}

