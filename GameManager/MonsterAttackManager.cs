using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using Cysharp.Threading.Tasks.Triggers;
using System.Linq;

public class MonsterAttackManager : MonoBehaviour
{
    Scene currentScene;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public List<TestMob> monsters;

    [SerializeField]
    List<Image> monsterTurnCard;//몬스터의 남은 턴 횟수를 시각적으로 보여주는 카드. 0~9까지
    int monsterTurnCount;//몬스터의 턴 카드 갯수 int값.
    public int SpecialCardStack;//만약 잔여 턴 카드가 없을 경우 특수카드 변환 스택을 저장하여 턴카드가 생성될때 사용함.

    public bool monsterAttackAvailable; //몬스터의 공격이 가능한지 판별하는 변수. (플레이어가 3턴시간 이상을 사용하였을때)
    public float playerTurnUsed; //플레이어의 턴시간 누적 사용시간. 몬스터 공격을 하며 차감하기.

    public bool isAttacking;//공격 중일때사용하는 bool. >> 공격 애니메이션이 끝날때 false로 변경함. +특수 공격의 경우 일단은 MonsterSkill.@@@ 에서 false로 설정함.

    private void Update()
    {

        if(monsters.Count == 0) //등록된 몬스터가 누락되었을 경우 다시 등록
        {
            for (int i = 0; i < combatManager.monsterObject.Count; i++)
            {
                monsters.Add(combatManager.monsterObject[i].GetComponent<TestMob>());
            }
        }

        if (combatManager.isCombatStart)
        {
            if (monsterAttackAvailable)
            {
                if (playerTurnUsed >= 6)
                {
                    if (!isAttacking)
                    {
                        //MonsterSpecialAttack();//is attacking을 적용하려면 monsterSpecialAttack안에 monsterattack을 넣어야 할듯.
                        MonsterSpecialCard();//몬스터의 턴 카드중 하나를 특수카드로 변경.
                        playerTurnUsed -= 3;
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

            if (playerTurnUsed >= 3 && monsterTurnCount > 0) //combatManager.monsterTurnTime 에서 monsterturncount 로 변경. 카드가 생성된 이후에 몬스터가 행동을 진행해줫으면 함.
            {
                monsterAttackAvailable = true;
            }
            else if(playerTurnUsed >= 3 && combatManager.monsterTurnTime <= 0) //플레이어의 턴이 리필 되자말자 누적된턴의 공격이 바로 실행되는것 방지.
            {
                playerTurnUsed = 0;
            }
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
        else if (combatManager.isAggroOn && combatManager.tank.isDead)//어그로가 켜져있고 탱커가 죽어있을때
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
        if (monster.target.isDead)//만약 선택된 플레이어가 이미 사망한 상태라면 다시 타겟 선정.
        {
            for (int i = 0; i < combatManager.playerList.Count; i++)
            {
                if (!combatManager.playerList[i].isDead)
                {
                    monster.target = combatManager.playerList[i];
                    break;
                }
            }
            return;
        }

        if (monster.Hp >= monster.MaxHp * 0.8f)//몬스터의 체력이 80% 이상일때는 공격형 스킬만 사용.
        {
            CardUse();//몬스터의 턴 카드 효과.
            if (monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardIsSpecial)//특수카드가 사용되었을때
            {
                MonsterSpecialAttack();//특수 공격
            }
            else
            {
                monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            }
            monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().IsCardOn = false;//카드 사용후 카드를 사용된 상태로 변경.
        }
        else if (combatManager.alivePlayerCount == 1)//플레이어가 1명만 살아있을때 공격형 스킬만사용
        {
            CardUse();//몬스터의 턴 카드 효과.
            if (monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardIsSpecial)//특수카드가 사용되었을때
            {
                MonsterSpecialAttack();//특수 공격
            }
            else
            {
                monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            }
            monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().IsCardOn = false;//카드 사용후 카드를 사용된 상태로 변경.
        }
        else//그외의 상황에서는 모든 스킬을 사용.
        {
            CardUse();//몬스터의 턴 카드 효과.
            if (monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardIsSpecial)//특수카드가 사용되었을때
            {
                MonsterSpecialAttack();//특수 공격
            }
            else
            {
                monster.monsterSkill[Random.Range(0, monster.monsterSkill.Count)].UseSkill(monster);
            }
            monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().IsCardOn = false;//카드 사용후 카드를 사용된 상태로 변경.
        }
    }


    private async void MonsterAttack()
    {
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
            MonsterAttack();//죽은 몬스터가 선택되었을 경우, 재시도.
        }

    }
    private async void MonsterExtraAttack()//플레이어의 턴이 끝나고 남은 몬스터 턴에서 재생되는 공격.
    {
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
            MonsterExtraAttack();//죽은 몬스터가 선택되었을 경우, 재시도.
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
        else //보스전이 아닐때의 특수패턴
        {
            await SpecialAttackStartDelay(0.3f, monster);
           
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
                monster.monsterSkill[0].BattleCry(monster);//전투의 함성
                break;
            case "battle in stage2":

                break;
            case "battle in stage3":
                break;
            case "battle in stage4"://마왕성
                break;
        }

    }


    public async void MonsterTurnCardSet()//새로 턴타임을 리필 할때 카드가 세팅되는 효과.
    {
        await UniTask.Delay(1800);//이전에 실행된 카드가 사용되는 효과와 겹치지 않게 딜레이를 줌.
        monsterTurnCount = combatManager.monsterTurnTime / 3;
        monsterTurnCount += (combatManager.monsterTurnTime%3) == 0 ? 0 : 1;//턴타임이 3의 배수일 경우 딱맞게, 그 이상일 경우 카드 한장 추가.
        for(int i=0; i< monsterTurnCount; i++)
        {
            monsterTurnCard[i].GetComponent<MonsterCardEffect>().CardReset();//카드가 생성되는 효과.
        }
        SpecialCardStackUse();//특수카드 스택을 사용하는 함수.
    }
    public async void DeadMonsterTurnCardSet()//몬스터가 죽었을때 잔여 턴 카드의 개수를 조정하는 함수.
    {
        await UniTask.Delay(600);
        int tempCount = combatManager.GetNewTurnTime() / 3;//GetNewTurnTime() == 죽은 몬스터의 턴타임을 제외한 나머지 턴타임.
        tempCount += (combatManager.GetNewTurnTime() % 3) == 0 ? 0 : 1;
        if(tempCount < monsterTurnCount)
        {
            for (int i = tempCount; i < monsterTurnCount; i++)
            {
                monsterTurnCard[i].GetComponent<MonsterCardEffect>().CardUsed();//카드가 사용되는 효과.
            }
            monsterTurnCount = tempCount;
        }
    }
    public void CardUse()//몬스터의 턴을 사용할때 카드가 사용되는 효과.
    {
        monsterTurnCount--;
        monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardUsed();//카드가 사용되는 효과.
    }
    private void MonsterSpecialCard()//잔여 몬스터 턴 카드중 하나를 특수카드로 변경.
    {
        if (monsterTurnCount == 0)
        {
            SpecialCardStack++; // 몬스터의 잔여 턴 카드가 없을 경우 스택을 쌓아둠
            return;
        }

        //잔여 턴 카드중 특수카드가 아닌 카드만 필터링
        var nonSpecialCards = monsterTurnCard
            .Where(card => !card.GetComponent<MonsterCardEffect>().CardIsSpecial) //특수카드가 아닌 카드만 필터링
            .ToList(); // 필터링된 카드를 리스트로 변환

        if (nonSpecialCards.Count == 0)
        {
            SpecialCardStack++; // 변경할 수 있는 잔여 카드가 없을 경우 스택 쌓고 종료
            return;
        }

        // 랜덤으로 하나의 카드를 특수 카드로 변경
        int x = Random.Range(0, nonSpecialCards.Count);
        nonSpecialCards[x].GetComponent<MonsterCardEffect>().SpecialCard();
    }
    private void SpecialCardStackUse()//특수카드 스택을 사용하는 함수. (턴 카드 리셋시 사용하는 함수)
    {
        for(int i=0; i<SpecialCardStack; i++)
        {
            SpecialCardStack--;
            MonsterSpecialCard();
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
        await UniTask.Delay(600);
        AttackPattern(monster);
    }


    
}
