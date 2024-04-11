using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;
    public MapData mapData;
    public CombatDisplay combatDisplay; //전투 ui를 담을 변수.

    public List<PlayableC> playerList;//현재 전투에 참혀중인 플레이어(사망시 제외 하지말것.)
    public List<TestMob> monsterList;//전투에 참여할 몬스터들 << 여기에 있는 몬스터를 통해 해당 몬스터의 스킬을 사용
    public Dictionary<TestMob, GameObject> monstersInCombat; //전투에 참여하는 몬스터들과 그 오브젝트를 매칭시키는 딕셔너리.
    public List<GameObject> monsterObject; //몬스터 오브젝트를 담을 리스트.


    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //버프아이템은 한번에 하나만 적용 되도록. //만약에 다른 버프 사용중에 버프아이템을 사용 할 경우 이전 버프는 사라짐.
    public bool isAtkDebuff; //공격력 디버프가 적용중인 경우체크 >> 이를 토대로 전투가 끝나면 디버프 해제.
    //버프 아이템 사용중에는 파티원의 인원변경 불가능.
    public bool isBoss; //보스전투인지 아닌지 판별하는 변수.
    


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
        GoToFightScene();//전투 씬으로 넘어가는 함수. (해당 맵에 맞는 전투 뒷배경으로 이동됨)
        updateMonster();
        //...전투ui 로 넘어가는 함수 추가.

    }
    public void OnCombatEnd() //전투 종료시 저장된 몬스터의 정보들 삭제.
    {
        monstersInCombat.Clear();
        monsterObject.Clear();

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk +10);
            isAtkDebuff = false;
        }

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
    }

    private void updateMonster()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            var obj = Instantiate(monsterList[i].gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            monsterObject.Add(obj);
            monstersInCombat.Add(monsterList[i], monsterObject[i]);
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
    private void StartCombat()
    {

    }
    private void monsterDie()
    {

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
    private void GoToFightScene() //해당 전투 씬은 각 맵에 해당되는 전투 맵으로 이동. (각 스테이지 별로 정해져 있음.)
    {
        //mapdata에서 정보를 받아 해당 씬으로 이동.
    }
}



