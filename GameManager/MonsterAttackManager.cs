using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(monsterAttackAvailable)
        {
                if(playerTurnUsed>=6)
                {
                    MonsterSpecialAttack();//is attacking을 적용하려면 monsterSpecialAttack안에 monsterattack을 넣어야 할듯.
                    MonsterAttack();
                    playerTurnUsed -= 6;
                    monsterAttackAvailable = false;
                }
                else//3~5턴시간 사용시 일반 공격패턴.
                {
                    MonsterAttack();
                    playerTurnUsed -= 3;
                    monsterAttackAvailable = false;
                }
        }
        else if(!combatDisplay.isPlayerTurn && combatManager.isCombatStart)//전투가 시작되었는데 플레이어이 턴이 아니게 되었을때
        {
            if(combatManager.monsterTurnTime >0)
                MonsterAttack();
            if(combatManager.monsterTurnTime <= 0)
            {
                combatDisplay.isPlayerTurn = true;
                combatManager.timerSet();

            }
        }

        if(playerTurnUsed >= 3)
        {
            monsterAttackAvailable = true;
        }
    }
    private void AttackPattern(TestMob monster)
    {
        monster.target = null;
        if (!combatManager.isAggroOn)
        {
            //플레이어들중 체력이 30프로 이하인 타겟을 가장먼저 타겟으로.
            for (int i = 0; i < combatManager.playerList.Count; i++)
            {
                if (combatManager.playerList[i].hp <= combatManager.playerList[i].maxHp * 0.3f)
                {
                    monster.target = combatManager.playerList[i];
                    break;
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
            AttackPattern(monster);
            return;
        }
        if(monster.Hp >= monster.MaxHp * 0.8f)//몬스터의 체력이 80% 이상일때는 공격형 스킬만 사용.
        {
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
        }
        else if(combatManager.alivePlayerCount == 1)//플레이어가 1명만 살아있을때 공격형 스킬만사용
        {
            Debug.Log("플레이어가 1명만 살아있을때");
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
        }
        else
        {
            monster.monsterSkill[Random.Range(0, monster.monsterSkill.Count)].UseSkill(monster);
        }
    }


    private void MonsterAttack()
    {
        combatManager.monsterTurnTime -= 3;
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        Debug.Log("몬스터의 공격");
        if(!monster.isDead)
        {
            AttackPattern(monster);
            return;
        }
        else
        {
            MonsterAttack();
        }

    }
    private void MonsterSpecialAttack()
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
        //여기에 코루틴..?
    }


}
