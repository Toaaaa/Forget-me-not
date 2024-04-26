using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;
    public MapData mapData;
    public CombatDisplay combatDisplay; //전투 ui를 담을 변수.
    public GameObject mobplace;

    public List<PlayableC> playerList;//현재 전투에 참혀중인 플레이어(사망시 제외 하지말것.)
    public List<TestMob> monsterList;//전투에 참여할 몬스터들 << 여기에 있는 몬스터를 통해 해당 몬스터의 스킬을 사용 (이거는 prefab의 스크립트에 접근 하는것임을 유의)
    public Dictionary<TestMob, GameObject> monstersInCombat = new Dictionary<TestMob, GameObject>(); //전투에 참여하는 몬스터들과 그 오브젝트를 매칭시키는 딕셔너리.
    public List<GameObject> monsterObject; //몬스터 오브젝트를 담을 리스트.
    public List<GameObject> monsterAliveList; //살아있는 몬스터 리스트.


    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //버프아이템은 한번에 하나만 적용 되도록. //만약에 다른 버프 사용중에 버프아이템을 사용 할 경우 이전 버프는 사라짐.
    public bool isAtkDebuff; //공격력 디버프가 적용중인 경우체크 >> 이를 토대로 전투가 끝나면 디버프 해제.
    //버프 아이템 사용중에는 파티원의 인원변경 불가능.
    public bool isBoss; //보스전투인지 아닌지 판별하는 변수.   
    public bool isCombatStart; //전투가 시작되었는지 판별하는 변수.
    public string battleSceneName; //전투 씬의 이름을 저장하는 변수.

    //ui
    public bool isFirstSelection; //처음 선택창이 켜저있는지.
    public PlayableC selectedPlayer; //선택된 플레이어. (스킬+아이템을 사용할때 사용) << 이건 combatdisplay에서 사용.
    public GameObject monsterSelected; //공격,스킬을 사용할 지정된 몬스터.//몬스터의 경우 mob.target 에서 스스로 특정조건에 맞는 대상 판정.

    //turn
    public float playerTurnTime; //플레이어의 턴 시간.
    public float monsterTurnTime; //몬스터의 턴 시간.
    float tempMonst;



    private void Start()
    {
        playerList = new List<PlayableC>();
        playerList = playableManager.joinedPlayer;
        
    }
    public void OnCombatStart()//전투 시작시 호출되는 함수.
    { 
        playerList = playableManager.joinedPlayer;
        monsterList = isBoss ? mapData.specialMonsters : mapData.monsters;
        combatDisplay.playerList = playerList;
        combatDisplay.gameObject.SetActive(true);
        combatDisplay.playerList = playerList;
        isCombatStart = true;
        mapData.GoToBattle();
        Player.Instance.combatPosition = mapData.playerPosition;
        Player.Instance.CombatPositioning();


        //...전투ui 로 넘어가는 함수 추가.

    }
    public void OnCombatEnd() //전투 종료시 저장된 몬스터의 정보들 삭제.
    {
        Debug.Log("전투가 종료되었습니다.");
        for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
        }
        combatDisplay.inAction = false;
        monstersInCombat.Clear();
        monsterObject.Clear();
        monsterAliveList.Clear();       
        selectedPlayer = null;
        monsterSelected = null;

        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].resetStat();
        }//전투가 끝나면 최대 hp, 치명타, 공격력, 방어력 등의 스탯을 초기화.

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk +10);
            isAtkDebuff = false;
        }
        //플레이어의 스킬 버프가 켜져 있을시 해당 버프도 해제. (각종 기타 버프들도 다 해제 되는지 확인.)
        isCombatStart = false;
        SceneManager.LoadScene(Player.Instance.currentMapName);//전투가 끝나면 이전 맵으로 돌아가는 함수.
        combatDisplay.gameObject.SetActive(false);
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
        //몬스터가 전부 죽었을때 oncombatend를 실행시켜주는 함수.
        for(int i = 0; i < monsterObject.Count; i++)
        {
            if (monsterObject[i].GetComponent<TestMob>().Hp <= 0)
            {
                monsterDie(i);
            }
        }//몬스터의 체력이 0이하가 되면 사망판정 + 사망한 몬스터를 리스트,monsterobject에서 제거.
        if(isCombatStart)//전투가 시작되었을때
        {
            if (monsterAliveList.Count == 0)
            {
                Debug.Log("몬스터가 전멸하였습니다.");
                OnCombatEnd();
            }//몬스터 전멸시.           

            PlayerTimerDelta();
            //MoblistSet();
            if(playerTurnTime == 0)
            {
                if(monsterTurnTime == 0)
                {
                    timerSet();
                    combatDisplay.isPlayerTurn = true;
                    combatDisplay.combatSelection = combatDisplay.slotList[0].combatSelection;
                    combatDisplay.combatSelection.charSelection.SetActive(true);
                }//플레이어와 몬스터의 턴이 모두 소모되었을때, 타이머 리셋.
                else
                {
                    combatDisplay.isPlayerTurn = false;
                    //과 함께 몬스터의 나머지턴 소모.
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

    public void updateMonster()//incombatscene에서 해당 함수 호출하여 사용.
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != battleSceneName)
        {
            Debug.Log("다른씬.");
            return;
        }
        for (int i = 0; i < monsterList.Count; i++)
        {
            var obj = Instantiate(monsterList[i].gameObject, combatDisplay.mobSlotList[i].transform.position, Quaternion.identity,mobplace.transform);
            monsterObject.Add(obj);
            combatDisplay.mobSlotList[i].GetComponent<MobSlot>().monster = obj;
            monsterAliveList.Add(obj);
            monstersInCombat.Add(monsterList[i], monsterObject[i]);
        }
        combatDisplay.MobList.Clear();

        for (int i = 0; i < monsterObject.Count; i++) //컴뱃 매니저의 몬스터 오브젝트를 컴뱃 디스플레이의 몬스터리스트에도 추가.
        {
            combatDisplay.MobList.Add(monsterObject[i]);
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
        for (int i = 0; i < monsterList.Count; i++)
        {
            tempMonst +=1.5f* monsterList[i].GetComponent<TestMob>().Speed;
        }
        monsterTurnTime = tempMonst;
    }
    private void timerSet()
    {
        tempMonst = 0;
        playerTurnTime = 0;
        for(int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {               
                playerTurnTime += 1.5f*playerList[i].spd;
            }
        }
        for(int i = 0; i < monsterObject.Count; i++)
        {
            tempMonst += 1.5f*monsterObject[i].GetComponent<TestMob>().Speed;
        }
        monsterTurnTime = tempMonst;
    }//플레이어와 몬스터의 속도에 따른 턴시간을 세팅해주는 함수.
    private void StartCombat()
    {

    }
    private void monsterDie(int num)
    {
        Debug.Log("몬스터가 죽었습니다.");
        monsterObject[num].GetComponent<TestMob>().isDead = true;
        monsterAliveList.Remove(monsterObject[num]);
    }


    private void DebuffDamageCount() //전투 한 싸이클이 끝날때마다 디버프 데미지를 계산하는 함수.
    {
        for(int i=0; i<playerList.Count; i++)
        {
            if (playerList[i].isPoisoned)
            {
                playerList[i].hp -= 10;
            }
        }
    }



   
}



