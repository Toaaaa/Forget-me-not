using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class MonsterAttackManager : MonoBehaviour
{
    Scene currentScene;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public List<TestMob> monsters;

    public bool monsterAttackAvailable; //몬스터의 공격이 가능한지 판별하는 변수. (플레이어가 3턴시간 이상을 사용하였을때)
    public float playerTurnUsed; //플레이어의 턴시간 누적 사용시간. 몬스터 공격을 하며 차감하기.

    public bool isAttacking;//공격 중일때사용하는 bool.

    private void Update()
    {
        if (combatManager.isCombatStart)
        {
            if (monsterAttackAvailable)
            {
                if (playerTurnUsed >= 6)
                {
                    if (!isAttacking)
                    {
                        MonsterSpecialAttack();//is attacking을 적용하려면 monsterSpecialAttack안에 monsterattack을 넣어야 할듯.
                        playerTurnUsed -= 6;
                        monsterAttackAvailable = false;
                    }                   
                }
                else//3~5턴시간 사용시 일반 공격패턴.
                {
                    if (!isAttacking)
                    {
                        MonsterAttack();
                        playerTurnUsed -= 3;
                        monsterAttackAvailable = false;
                    }
                }
            }
            else if (!combatDisplay.isPlayerTurn && combatManager.isCombatStart)//전투가 시작되었는데 플레이어이 턴이 아니게 되었을때
            {
                if (combatManager.monsterTurnTime > 0 && !isAttacking)
                    MonsterExtraAttack();

                if (combatManager.monsterTurnTime <= 0)
                {
                    combatDisplay.isPlayerTurn = true;
                    combatManager.timerSet();

                }
            }

            if (playerTurnUsed >= 3 && combatManager.monsterTurnTime > 0)
            {
                monsterAttackAvailable = true;
            }
            else if(playerTurnUsed >= 3 && combatManager.monsterTurnTime <= 0) //플레이어의 턴이 리필 되자말자 누적된턴의 공격이 바로 실행되는것 방지.
            {
                playerTurnUsed = 0;
            }
        }
    }
    private async void AttackPattern(TestMob monster)
    {
        monster.target = null;
        if (!combatManager.isAggroOn)
        {
            //플레이어들중 체력이 30프로 이하인 타겟을 가장먼저 타겟으로.
            for (int i = 0; i < combatManager.playerList.Count; i++)
            {
                if (combatManager.playerList[i].hp <= combatManager.playerList[i].maxHp * 0.3f)
                {
                    if (!combatManager.playerList[i].isDead)
                    {
                        monster.target = combatManager.playerList[i];
                        break;
                    }
                }
            }
            //만약 30프로 이하의 타겟이 없다면.
            if (monster.target == null)
            {
                monster.target = combatManager.playerList[Random.Range(0, combatManager.playerList.Count)];
            }
        }
        else if(combatManager.isAggroOn&&combatManager.tank.isDead)//어그로가 켜져있고 탱커가 죽어있을때
        {
            combatManager.isAggroOn = false;
            if (monster.target == null)
            {
                monster.target = combatManager.playerList[Random.Range(0, combatManager.playerList.Count)];
            }
        }
        else
        {
            monster.target = combatManager.tank; //어그로가 켜져있을때 탱커를 타겟으로.
        }
        if(monster.target.isDead)//만약 선택된 플레이어가 이미 사망한 상태라면 다시 타겟 선정.
        {
            for(int i = 0; i<combatManager.playerList.Count; i++)
            {
                if (!combatManager.playerList[i].isDead)
                {
                    monster.target = combatManager.playerList[i];
                    break;
                }
            }
            return;
        }

        if(monster.Hp >= monster.MaxHp * 0.8f)//몬스터의 체력이 80% 이상일때는 공격형 스킬만 사용.
        {
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            await AttackingDone();
        }
        else if(combatManager.alivePlayerCount == 1)//플레이어가 1명만 살아있을때 공격형 스킬만사용
        {
            Debug.Log("플레이어가 1명만 살아있을때");
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            await AttackingDone();
        }
        else
        {
            monster.monsterSkill[Random.Range(0, monster.monsterSkill.Count)].UseSkill(monster);
            await AttackingDone();
        }
    }


    private async void MonsterAttack()
    {
        Debug.Log("MonsterAttack");
        isAttacking = true;
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        if(!monster.isDead)
        {
            combatManager.monsterTurnTime -= 3;
            await AttackStartDelay(monster);
            return;
        }
        else
        {
            MonsterAttack();
        }

    }
    private async void MonsterExtraAttack()//플레이어의 턴이 끝나고 남은 몬스터 턴에서 재생되는 공격.
    {
        Debug.Log("MonsterExtraAttack");
        isAttacking = true;
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        if (!monster.isDead)
        {
            combatManager.monsterTurnTime -= 3;
            await AttackStartExtraDelay(monster);
            return;
        }
        else
        {
            MonsterExtraAttack();
        }
    }

    private async void MonsterSpecialAttack()
    {
        Debug.Log("몬스터의 특수패턴");
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        currentScene = SceneManager.GetActiveScene();
        if (combatManager.isBoss)//보스전의 특수패턴
        {
            switch (currentScene.name)
            {
                case "bossbattle in stage1"://1번 맵 보스
                    break;
                case "bossbattle in stage2":
                    break;
                case "bossbattle in stage3":
                    break;
                case "bossbattle in stage4"://마왕성 보스
                    break;
            }
        }
        else //일반 몬스터의 특수 패턴
        {
            await SpecialAttackStartDelay(0.7f, monster); //턴타임 6초 이상일시 0.7초 딜레이 후 특수공격 시작.
           
        }
    }

    private async UniTask SpecialAttackStartDelay(float delay, TestMob monster)
    {
        await UniTask.Delay((int)(delay * 1000));
        switch (currentScene.name)
        {
            case "battle in stage0"://튜토리얼맵
                monster.monsterSkill[0].BattleCry(monster);//전투의 함성
                break;
            case "battle in stage1":
                break;
            case "battle in stage2":
                break;
            case "battle in stage3":
                break;
            case "battle in stage4"://마왕성
                break;
        }

    }

    private void OnDisable()
    {
        monsters.Clear();
    }
    private async UniTask AttackStartDelay(TestMob monster)
    {
        await UniTask.Delay(600);
        AttackPattern(monster);
    }
    private async UniTask AttackStartExtraDelay(TestMob monster)//플레이어의 턴이 끝나고 남은 몬스터 턴에서 재생되는 공격.
    {
        await UniTask.Delay(900);
        AttackPattern(monster);
    }


    private async UniTask AttackingDone() //몬스터의 useskill이 실행과 동시에 재생되는 태스크.
    {
        await UniTask.Delay(800);
        isAttacking = false;
    }
    
}
