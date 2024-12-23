using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;
    public DamagePrintManager damagePrintManager;
    public TurnTimeUsedShow turnTimeUsedShow;
    public CombatTimer combatTimer;
    public MonsterAttackManager monsterAttackManager;
    public SFXManager SFXManager;
    public MapData mapData;
    public CombatDisplay combatDisplay; //전투 ui를 담을 변수.
    public GameObject mobplace;
    public SlotPlacement slotPlacement;
    public GameObject EndOfJourney;

    public List<PlayableC> playerList;//현재 전투에 참혀중인 플레이어(사망시 제외 하지말것.)
    //public List<PlayableC> alivePlayerList;//현재 전투에 참여중인 살아있는 플레이어.
    public List<TestMob> monsterList;//전투에 참여할 몬스터들 << 여기에 있는 몬스터를 통해 해당 몬스터의 스킬사용시만 접근 (이거는 prefab의 스크립트에 접근 하는것임을 유의)
    //public Dictionary<TestMob, GameObject> monstersInCombat = new Dictionary<TestMob, GameObject>(); //전투에 참여하는 몬스터들과 그 오브젝트를 매칭시키는 딕셔너리.
    public List<GameObject> monsterObject; //몬스터 오브젝트를 담을 리스트.
    public List<GameObject> monsterAliveList; //살아있는 몬스터 리스트.

    public float DeadMobExpCount; //현재 전투에서 죽은 몬스터들의 총 경험치.
    public int DeadMobGoldCount; //현재 전투에서 죽은 몬스터들의 총 골드.
    public List<Item> DeadMobItemDrop; //현재 전투에서 죽은 몬스터들이 드랍한 아이템들.
    public int alivePlayerCount; //살아있는 플레이어의 수.
    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //버프아이템은 한번에 하나만 적용 되도록. //만약에 다른 버프 사용중에 버프아이템을 사용 할 경우 이전 버프는 사라짐.
    public bool isAtkDebuff; //공격력 디버프가 적용중인 경우체크 >> 이를 토대로 전투가 끝나면 디버프 해제.
    //버프 아이템 사용중에는 파티원의 인원변경 불가능.
    public bool selectedFlee; //도망가기가 선택되었는지.
    public bool isBoss; //보스전투인지 아닌지 판별하는 변수.   
    public bool isCombatStart; //전투가 시작되었는지 판별하는 변수.
    public string battleSceneName; //전투 씬의 이름을 저장하는 변수.
    public PlayableC tank;
    public bool isAggroOn; //탱커의 어그로가 켜져있는지. //만약 켜져있을경우 공격의 대상은 항상 탱커로.

    //ui
    public bool isFirstSelection; //처음 선택창이 켜저있는지.
    public PlayableC selectedPlayer; //선택된 플레이어. (스킬+아이템을 사용할때 사용) << 이건 combatdisplay에서 사용.
    public GameObject monsterSelected; //공격,스킬을 사용할 지정된 몬스터.//몬스터의 경우 mob.target 에서 스스로 특정조건에 맞는 대상 판정.

    //turn
    public float playerTurnTime; //플레이어의 턴 시간.
    public int monsterTurnTime; //몬스터의 턴 시간.
    public float playerNoAttackTime; //플레이어가 아무런 선택없이 3.5초동안 시간을 보낸경우, 자동으로 몬스터 공격 실행을 위한 변수.
    public int attackCostTime; //기본공격의 코스트 시간unw.
    public int skillCostTime; //스킬 사용의 코스트 시간.
    public int itemCostTime; //아이템 사용의 코스트 시간.
    public int fleeCostTime; //도망가기의 코스트 시간.
    public PlayableC lastAction; //마지막으로 행동한 플레이어. (피로도 시스템을 위한 변수)

    public int tempMonst;



    private void Start()
    {
        playerList = new List<PlayableC>();
        playerList = playableManager.joinedPlayer;
    }
    public void OnCombatStart()//전투 시작시 호출되는 함수.
    { 
        selectedFlee = false;
        Player.Instance.wasInCombat = true;
        playerList = playableManager.joinedPlayer;
        //alivePlayerList = playableManager.joinedPlayer;
        /*for(int i = 0; i < alivePlayerList.Count; i++)
        {
            if (alivePlayerList[i].isDead)
            {
                alivePlayerList.Remove(alivePlayerList[i]);
            }
        }//전투 시작시 살아있는 플레이어 리스트를 만드는 함수.*/
        isBoss = mapData.isBossMap;
        monsterList = isBoss ? mapData.specialMonsters : mapData.monsters;
        combatDisplay.playerList = playerList;
        combatDisplay.isPlayerTurn = true;
        combatDisplay.playerList = playerList;
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetStat();
        }
        ResetPlayerBuff();
        mapData.GoToBattle();


        //...전투ui 로 넘어가는 함수 추가.

    }
    public void OnCombatEnd() //전투 종료시 저장된 몬스터의 정보들 삭제. (flee 시에도 바로 해당 함수만 호출)
    {
        Debug.Log("전투가 종료되었습니다.");
        for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
            Debug.Log("몬스터 오브젝트 삭제");
        }
        
        slotPlacement = null;
        combatDisplay.inAction = false;
        //monstersInCombat.Clear();
        monsterObject.Clear();
        monsterAliveList.Clear();       
        selectedPlayer = null;
        monsterSelected = null;

        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetStat();
        }//전투가 끝나면 최대 hp, 치명타, 공격력, 방어력 등의 스탯을 초기화.

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk +10);
            isAtkDebuff = false;
        }
        ResetPlayerBuff();
        ReviveIfDead();
        //플레이어의 스킬 버프가 켜져 있을시 해당 버프도 해제. (각종 기타 버프들도 다 해제 되는지 확인.)
        isCombatStart = false;
        //스킬 알람 강제종료
        combatDisplay.skillUseAlarm.FinishAlarm();
        monsterAttackManager.MonsterAlarm.FinishAlarm();
        //화면 닫힘
        SceneChangeManager.Instance.LeaveBattleScene();
    }
    public void OnCombatLost() //전투에서 패배할시.
    {
        /*for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
        }
        monsterObject.Clear();*///여기서 실행해버리면 combatend도 실행해 버려서 꼬임. scenechangemanager에서 실행하도록 변경.
        DeadMobItemDrop.Clear();
        DeadMobGoldCount = 0;
        DeadMobExpCount = 0;
        combatDisplay.inAction = false;
        //monstersInCombat.Clear();
        monsterAliveList.Clear();
        selectedPlayer = null;
        monsterSelected = null;

        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetStat();
        }//전투가 끝나면 최대 hp, 치명타, 공격력, 방어력 등의 스탯을 초기화.

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk + 10);
            isAtkDebuff = false;
        }
        ResetPlayerBuff();
        //플레이어의 스킬 버프가 켜져 있을시 해당 버프도 해제. (각종 기타 버프들도 다 해제 되는지 확인.)
        isCombatStart = false;
        //스킬 알람 강제종료
        combatDisplay.skillUseAlarm.FinishAlarm();
        monsterAttackManager.MonsterAlarm.FinishAlarm();
        //블랙아웃 후 여정의 끝 화면 활성화.
        SceneChangeManager.Instance.BlackOutEndJourney();

    }

    private void Update()
    {
        timerDelta();
        if(BuffIsOn&&consumeTimer == 0)
        {
            consumeOnUse.OnEnd();
            BuffIsOn = false;
            consumeOnUse = null;
        }
        for(int i = 0; i < monsterObject.Count; i++)
        {
            if (monsterObject[i].GetComponent<TestMob>().Hp <= 0 && !monsterObject[i].GetComponent<TestMob>().isDead)
            {
                monsterDie(i);
            }
        }//몬스터의 체력이 0이하가 되면 사망판정 + 사망한 몬스터를 리스트,monsterobject에서 제거.
        if(isCombatStart) //전투가 시작되었을때
        {
            PlayerDieCheck();//플레이어의 사망여부를 체크하는 함수.+ 사망시 색깔을 임시로 변경
            MonsterAllDeadCount();//몬스터 전멸시 combat 종료시키는 메서드.
            alivePlayerCount = AliveCounting(); //살아있는 플레이어의 수를 세는 함수.
            PlayerTimerDelta();
            PlayerNoAttackTime();
            TankChecking();
            //MoblistSet();
            if(playerTurnTime <= 0)
            {
                if(monsterTurnTime <= 0)
                {
                    combatDisplay.isPlayerTurn = true;
                    timerSet();
                }//플레이어와 몬스터의 턴이 모두 소모되었을때, 타이머 리셋.
                else
                {
                    //시간을 조금 가진뒤 combatdisplay.isplayerturn = false; 으로 바꾸어 몬스터의 턴을 실행.
                    //플레이어의 남은 행동 애니메이션과 겹치지 않도록 하는것.
                    combatDisplay.isPlayerTurn = false;
                    //과 함께 몬스터의 나머지턴 소모., ismonsterturn = true; 과함께 true일때 실행되는 함수를 제작?
                }
            }
            else 
            {
                combatDisplay.isPlayerTurn = true;
            }//playerTurnTime != 0
            if (combatDisplay.attackSelected ||combatDisplay.skillSelected)
            {
                monsterSelected = monsterAliveList[combatDisplay.selectedMobIndex];
            }
            else
            {
                monsterSelected = null;
            }
        }
    }

    public void updateMonster()//incombatscene.start()에서 해당 함수 호출하여 사용. //몬스터의 배치와 저장 정보 업데이트 함수.
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != battleSceneName)
        {
            Debug.Log("다른씬.");
            return;
        }
        for (int i = 0; i < monsterList.Count; i++)
        {
            var obj = Instantiate(monsterList[i].gameObject, slotPlacement.M_slotPlace[i].transform.position, Quaternion.identity);
            monsterObject.Add(obj);
            combatDisplay.mobSlotList[i].GetComponent<MobSlot>().monster = obj;
            monsterAliveList.Add(obj);
            //monstersInCombat.Add(monsterList[i], monsterObject[i]);
        }
        combatDisplay.MobList.Clear();
        monsterAttackManager.monsters.Clear();
        monsterAttackManager.SpecialCardStack = 0;//몬스터의 특수카드 스택 초기화.
        for (int i = 0; i < monsterObject.Count; i++) //컴뱃 매니저의 몬스터 오브젝트를 컴뱃 디스플레이의 몬스터리스트에도 추가.
        {
            combatDisplay.MobList.Add(monsterObject[i]);
        }
        for(int i =0; i< monsterObject.Count; i++)
        {
            monsterAttackManager.monsters.Add(monsterObject[i].GetComponent<TestMob>());
        }
    }

    private void timerDelta() //deltatime으로 매초 consumeTimer의 시간을 깍는 함수.
    {
        if(consumeTimer > 0)
        {
            consumeTimer -= Time.deltaTime;
        }
        else
        {
            consumeTimer = 0;

        }
    }
    private void PlayerTimerDelta()
    {
        if(playerTurnTime > 0)
        {
            playerTurnTime -= Time.deltaTime;
        }
        else
        {
            playerTurnTime = 0;
        }
    }
    private void PlayerNoAttackTime()
    {
        playerNoAttackTime += Time.deltaTime;
        if(
            playerNoAttackTime >= 3.5f && monsterTurnTime>0)
        {
            monsterAttackManager.playerTurnUsed += 3;//플레이어가 3.5초간 아무런 행동을 하지 않으면 playerturnused에 3초를 추가하여 몬스터의 공격 유도.
            playerNoAttackTime = 0;
        }
    }

    public void timerStart()//완전 처음 전투 시작시의 타이머.
    {
        tempMonst = 0;
        playerTurnTime = 0;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {
                playerTurnTime +=1.5f* playerList[i].spd;
            }
        }
        playerTurnTime += 3f;
        if(GameManager.Instance.storyScriptable.isOnStage2 && !GameManager.Instance.storyScriptable.isOnStage3 && !GameManager.Instance.storyScriptable.Stage2Extra3)
        {
            playerTurnTime = playerTurnTime * 0.7f;
        }//만약 바람정령 퀘스트를 마치지 못한상태로 스테이지 2에 있는 경우 speed가 30프로 감소.
        CombatTimerSet();
        for (int i = 0; i < monsterList.Count; i++)
        {
            tempMonst +=1* monsterList[i].GetComponent<TestMob>().monster.mSpeed;
        }
        monsterTurnTime = tempMonst;
        monsterAttackManager.MonsterTurnCardSet();//몬스터의 카드를 세팅.
    }
    public void timerSet()//턴이 재시작 될때 사용하는 함수.
    {
        //몬스터의 속도가 최소속도보다 낮을경우 최소속도로 설정. 마법사의 속도감소 스킬의 보정을 위함.
        for (int i =0; i<monsterAliveList.Count; i++) 
        {
            TestMob m =monsterAliveList[i].GetComponent<TestMob>();
            if (m.Speed < m.MinimumSpeed)
            {
                monsterAliveList[i].GetComponent<TestMob>().Speed = m.MinimumSpeed;
            }
        }

        tempMonst = 0;
        playerTurnTime = 0;
        for(int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {               
                playerTurnTime += 1.5f*playerList[i].spd;
            }
        }
        playerTurnTime += 2.5f;
        if (GameManager.Instance.storyScriptable.isOnStage2 && !GameManager.Instance.storyScriptable.isOnStage3 && !GameManager.Instance.storyScriptable.Stage2Extra3)
        {
            playerTurnTime = playerTurnTime * 0.7f;
        }//만약 바람정령 퀘스트를 마치지 못한상태로 스테이지 2에 있는 경우 speed가 30프로 감소.
        CombatTimerSet();
        for (int i = 0; i < monsterAliveList.Count; i++)
        {
            tempMonst += 1* monsterAliveList[i].GetComponent<TestMob>().Speed;
        }
        monsterTurnTime = tempMonst;
        monsterAttackManager.MonsterTurnCardSet();//몬스터의 카드를 세팅.

        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {
                combatDisplay.selectedSlotIndex = i;
                combatDisplay.slotList[i].combatSelection.charSelection.SetActive(true);
                isFirstSelection = false;
                combatDisplay.attackSelected = false;
                combatDisplay.skillSelected = false;
                combatDisplay.skillSelectedForPlayer = false;
                combatDisplay.skillForAllMob = false;
                combatDisplay.skillForAllPlayer = false;
                combatDisplay.skillForMe = false;
                combatDisplay.itemSelected = false;
                break;
            }
        }//마지막으로 모든 수치 초기화.
    }//플레이어와 몬스터의 속도에 따른 턴시간을 세팅해주는 함수.
    public int GetNewTurnTime()
    {
        int temptime = 0;
        for (int i = 0; i < monsterAliveList.Count; i++)
        {
            if (!monsterAliveList[i].GetComponent<TestMob>().isDead)//죽은 몬스터 제외.
                temptime += 1 * monsterAliveList[i].GetComponent<TestMob>().Speed;
        }
        return temptime;
    }

    private void monsterDie(int num)
    {
        monsterObject[num].GetComponent<TestMob>().Hp = 0.1f;//hp가 0이하가 되면 계속 trigger 호출이 되서 사망 애니메이션이 고장남. 따라서 사망직후 바로 hp 0.1세팅
        monsterObject[num].GetComponent<TestMob>().isDead = true;
        //monsterAttackManager.DeadMonsterTurnCardSet();//몬스터가 죽었을때 죽은 몬스터 분의 턴타임을 잔존 턴카드에서 제거.
        monsterObject[num].GetComponent<MonsterAnimatorController>().Death("DEATH").Forget();//몬스터 사망시의 모든 기능 실행.
        GameObject mob = combatDisplay.MobList.Find(x => x == monsterObject[num]);
        if(mob != null)
        {
            combatDisplay.MobList.Remove(mob);
        }
    }
    private void PlayerDieCheck()//추후 isdead에서의 색깔변화가 아닌. 애니메이션의 파라미터를 isdead로 변경하여 죽은 애니메이션을 실행하도록 변경할것.
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].hp <= 0 /*&& !playerList[i].isDead*/)
            {
                playerList[i].isDead = true;
                /*for (int j = 0; j < alivePlayerList.Count; j++)
                {
                    if (alivePlayerList[j].isDead)
                    {
                        alivePlayerList.Remove(alivePlayerList[j]);
                    }
                }*/
            }
            if (playerList[i].isDead)
            {
                combatDisplay.slotList[i].GetComponent<Image>().color= new Color32(255, 0, 0, 255);
                playerList[i].fatigue = 0;
                playerList[i].isTired = false;
            }
            if (!playerList[i].isDead)
            {
                combatDisplay.slotList[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }//플레이어의 체력이 0이하가 되면 사망판정.
        if(playerList.TrueForAll(x => x.isDead == true))
        {
            Debug.Log("파티가 전멸하였습니다.");
            OnCombatLost();
        }//파티의 모든 인원이 사망시 전투종료.

    }

    private void DebuffDamageCount() //전투 한 싸이클이 끝날때마다 디버프 데미지를 계산하는 함수.
    {
        for(int i=0; i<playerList.Count; i++)
        {
            if (playerList[i].isPoisoned)
            {
                playerList[i].hp -= playerList[i].hp * 0.1f;
            }
        }
    }

    private void ResetPlayerBuff()
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetBUff();
        }
        isAggroOn = false;

    }
    private void ReviveIfDead()//전투중 죽은 플레이어 체력 1로 살려두기.
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].isDead)
            {
                playerList[i].isDead = false;
                playerList[i].hp = 1;
            }
        }
    }
    private void MonsterAllDeadCount()
    {
        if (monsterAliveList.Count == 0&&!playerList.TrueForAll(x => x.isDead == true))//endofjourney가 비활성화 되어있을때만 실행.
        {
            Debug.Log("몬스터가 전멸하였습니다.");
            //여기서 전투종료후 경험치 계산등을 통해 레벨업 확인 + 레벨업시 스텟증가 스킬 해금등의 텍스트 출력 함수 + 이함수가 끝나면 oncombatend실행.
            OnCombatEnd();
        }
    }

    private int AliveCounting()
    {
        int alivePlayerCount=0;

        for(int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {
                alivePlayerCount++;
            }
        }
        return alivePlayerCount;
    }
    private void TankChecking()
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].name == "Tank")
            {
                tank = playerList[i];
            }
        }
    }
    private void CombatTimerSet()
    {
        combatTimer.maxTime = playerTurnTime;
    }
    public void LostCombatMobClear()
    {
        for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
        }
        monsterObject.Clear();
    }
    public void TimerShake()//액션 코스트 대비 턴타임이 부족할때 타이머 흔들림 효과.
    {
        combatTimer.gameObject.transform.DOPunchPosition(new Vector3(5.5f, 0, 0), 0.6f, 10, 1);
    }
}



