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
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK0",0).Forget();//공격 애니메이션 재생
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx0();//기본 공격 sfx 재생
        var obj = Instantiate(normalAttack, trans.transform.position, Quaternion.identity);
        obj.GetComponent<AttackSkill>().player = this;
        obj.GetComponent<AttackSkill>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<AttackSkill>().targetLocked();
    }
    override public void Skill1(Transform trans) //블레이즈//모든 몬스터에게 1.5배의 공격력으로 공격
    {
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK1",1).Forget();//스킬1 애니메이션 재생
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx1();//스킬1 sfx 재생
        for (int i = 0; i < CombatManager.Instance.monsterAliveList.Count; i++)
        {
            var obj = Instantiate(skillEffect1, trans.transform.position, Quaternion.identity);
            obj.GetComponent<MagiSkill1>().player = this;
            obj.GetComponent<MagiSkill1>().targetMob = CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>();
            obj.GetComponent<MagiSkill1>().targetLocked();
        }
    }
    override public void Skill2(Transform trans) //단일 공격 + 느려진 시간스택(매 속도 감소 사용시 마다 스택 쌓임)에 비례하여 데미지 + 스택 리셋
    { 
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK2",2).Forget();//스킬2 애니메이션 재생
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx2();//스킬2 sfx 재생
        var obj = Instantiate(skillEffect2, trans.transform.position, Quaternion.identity);
        obj.GetComponent<MagiSkill2>().player = this;
        obj.GetComponent<MagiSkill2>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<MagiSkill2>().targetLocked();
    }
    override public void Skill3(Transform trans) //속도 감소 //시간 비동기화
    {//코스트 상(적의 속도를 감소키기기에 밸류가 높음)
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK3",3).Forget();//스킬3 애니메이션 재생
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx3();//스킬3 sfx 재생
        for (int i = 0; i < CombatManager.Instance.monsterAliveList.Count; i++)
        {
            var obj = Instantiate(skillEffect3, trans.transform.position, Quaternion.identity);
            obj.GetComponent<MagiSkill3>().player = this;
            obj.GetComponent<MagiSkill3>().targetMob = CombatManager.Instance.monsterAliveList[i].GetComponent<TestMob>();
            obj.GetComponent<MagiSkill3>().targetLocked();
        }
    }
    override public void Skill4(Transform trans) //피어싱 라이트닝. 3연속 관통 공격. (단일기) 추후 데미지출력 다단히트 3번으로 나오게 수정할수도(비주얼적인 이유로)(홀리레이 참고)
    { //>>높은 데미지 높은 코스트
        InGamePrefab.GetComponent<PlayerAnimatorController>().Attack("ATK4",4).Forget();//스킬3 애니메이션 재생
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4();//스킬4 sfx 재생
        var obj = Instantiate(skillEffect4, trans.transform.position, Quaternion.identity);
        obj.GetComponent<MagiSkill4>().player = this;
        obj.GetComponent<MagiSkill4>().targetMob = this.singleTarget.GetComponent<TestMob>();
        obj.GetComponent<MagiSkill4>().targetLocked();
        TripleLighting(obj);//투사체 이동시간 1.6초중 ,  0.4초 0.8초 1.2초 구간에 이동경로상에 작은 천둥 sfx 출력 하는 함수,
    }


    //DmgCalc 에서 스킬의 데미지가 적용되는 방식 관리.
    public override void AttackDmgCalc(GameObject g)
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(normalAttackType, monster, critatk);//속성 데미지 계산.
        ElementStack(normalAttackType, monster);//속성 스택 쌓기.

        monster.TakeDamage();//피격시 반짝이는 효과
        monster.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject, critatk, isCrit,false);
        Destroy(g);
    }
    override public void SkillDmgCalc1(GameObject g)
    {

    }
    override public void SkillDmgCalc2(GameObject g)//3번 스킬로 쌓은 속도 스택을 추가 데미지로 적용.
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(skill2Type, monster, critatk);//속성 데미지 계산.
        ElementStack(skill2Type, monster);//속성 스택 쌓기.

        float stack = TimeStack(monster);//몬스터의 시간 스택 비례 데미지;
        monster.slowStack = 0;//스택 초기화

        monster.TakeDamage();//피격시 반짝이는 효과
        monster.Hp -= critatk *(1.5f+ stack);//기본 데미지 2배 + 스택(최대4회)만큼 추가 데미지
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject, critatk *stack, isCrit, false);
        Destroy(g);
    }
    override public void SkillDmgCalc3(GameObject g)
    {

    }
    override public void SkillDmgCalc4(GameObject g)//피어싱 라이트닝 데미지 계산.
    {
        float critatk = CheckCrit(atk, this.crit);
        bool isCrit = IsCritical(critatk, atk);
        TestMob monster = this.singleTarget.GetComponent<TestMob>();
        critatk = ElementDamage(skill4Type, monster, critatk);//속성 데미지 계산.
        ElementStack(skill4Type, monster);//속성 스택 쌓기.

        monster.TakeDamage();//피격시 반짝이는 효과
        monster.Hp -= critatk * 3.5f;
        CombatManager.Instance.damagePrintManager.PrintDamage(monster.thisSlot.gameObject, critatk * 3.5f, isCrit, false);
        Destroy(g);
    }
    override public void MultiDmg1(PlayableC player, TestMob mob)//마법사의 경우 전부 방어력 무시 트루데미지.
    {
        float critatk = CheckCrit(atk, this.crit);//데미지 치명타 보정
        bool isCrit = IsCritical(critatk, atk);

        critatk = ElementDamage(skill1Type, mob, critatk);//최종데미지 속성 데미지 계산.
        ElementStack(skill1Type, mob);//속성 스택 쌓기.

        mob.TakeDamage();//피격시 반짝이는 효과
        mob.Hp -= critatk * 1.5f;
        CombatManager.Instance.damagePrintManager.PrintDamage(mob.thisSlot.gameObject, critatk * 1.5f, isCrit, false);
    }
    override public void MultiDmg3(PlayableC player, TestMob mob)//시간 디버프 이기때문에 속성 스텍을 적용 할수 있는 최소한의 데미지 1 만 적용.
    {
        float critatk = CheckCrit(atk, this.crit);//데미지 치명타 보정
        bool isCrit = IsCritical(critatk, atk);
        isCrit =false;//시간 디버프는 최소 데미지를 적용하기에 치명타 없이 only 속성 추가 데미지만..
        critatk = ElementDamage(skill3Type, mob, 1);//기본데미지 1을 기반으로 속성 데미지 계산.
        ElementStack(skill3Type, mob);//속성 스택 쌓기.

        TimeAsynchronization(mob);//몬스터의 speed 감소 효과.
        mob.TakeDamage();//피격시 반짝이는 효과
        mob.Hp -= critatk;
        CombatManager.Instance.damagePrintManager.PrintDamage(mob.thisSlot.gameObject, critatk, isCrit, false);//최소 데미지 1
    }
    public void TimeAsynchronization(TestMob mob)//스킬 3의 속도 디버프 효과//slowstack은 최대 4번까지
    {
         if (mob.isslowed == false)
         {
            mob.Speed -= 3;
            mob.slowStack++;
         }
         else
         {
            Debug.Log("이미 속도가 감소되어 있습니다.");
            mob.Speed -= 1;
            mob.slowStack++;
         }
         if (mob.slowStack >= 4)
         {
             mob.slowStack = 4;
         }//최대 4 스택 까지만 적용
    }
    public float TimeStack(TestMob monster)
    {
        int speedStack = monster.slowStack;
        return (float)speedStack;
    }

    private async void TripleLighting(GameObject obj)//피어싱 라이트닝이 몬스터 타격전 경로에 3번(0.4,0.8,1.2초마다) 작은 번개를 소환하는 효과
    {
        /*
        for(int i=0; i<3; i++)
        {
            await UniTask.Delay(400);
            InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(i, obj.transform);//투사체 이동중에 번개 효과
        }*/
        await UniTask.Delay(800);
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(0, obj.transform);//투사체 이동중에 번개 효과
        await UniTask.Delay(200);
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(1, obj.transform);//투사체 이동중에 번개 효과
        await UniTask.Delay(200);
        InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4_starting(2, obj.transform);//투사체 이동중에 번개 효과

    }
    override public void LevelUpStat()//마법사의 경우 공격력 증가 위주의 스탯.
    {
        switch (level)//2레벨부터 15레벨까지의 레벨업시 스텟 증가량
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
            case 10: //10레벨때 전스텟2
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
        //1레벨 시작 스텟
        //atk: 6, def: 6, hp: 15, spd: 4, crit: 5
    }
    public override int LevelUpEffectInfo()//atk는 a, def와 hp는 b, spd는 c에 해당함
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

