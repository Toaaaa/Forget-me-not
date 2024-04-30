using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAttackManager : MonoBehaviour
{
    Scene currentScene;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public List<TestMob> monsters;

    public bool monsterAttackAvailable; //몬스터의 공격이 가능한지 판별하는 변수. (플레이어가 3턴시간 이상을 사용하였을때)
    public int playerTurnUsed; //플레이어의 턴시간 누적 사용시간. 몬스터 공격을 하며 차감하기.

    private void Update()
    {
        if(monsterAttackAvailable)
        {
                if(playerTurnUsed<6)
                {
                    MonsterSpecialAttack();
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
            MonsterAttack();
        }

        if(playerTurnUsed >= 3)
        {
            monsterAttackAvailable = true;
        }
    }

    private void MonsterAttack()
    {
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        Debug.Log("몬스터의 공격");
        if(!monster.isDead)
        {
            monster.target = combatManager.playerList[Random.Range(0, combatManager.playerList.Count)];
            monster.monsterSkill[Random.Range(0,monster.monsterSkill.Count)].UseSkill(monster);
            return;
        }
        else
        {
            MonsterAttack();
            Debug.Log("공격을 시도하였습니다. 다시 공격연산을 시작합니다.");
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
