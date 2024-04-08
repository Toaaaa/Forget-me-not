using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;
    public MapData mapData;

    public List<PlayableC> playerList;
    public List<Monster> monsterList;//전투에 참여할 몬스터들 << 여기에 있는 몬스터를 통해 해당 몬스터의 스킬을 사용
    public Dictionary<Monster, GameObject> monstersInCombat; //전투에 참여하는 몬스터들과 그 오브젝트를 매칭시키는 딕셔너리.
    public List<GameObject> monsterObject; //몬스터 오브젝트를 담을 리스트.


    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //버프아이템은 한번에 하나만 적용 되도록. //만약에 다른 버프 사용중에 버프아이템을 사용 할 경우 이전 버프는 사라짐.
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
        updateMonster();
        //...전투ui 로 넘어가는 함수 추가.

    }
    public void OnCombatEnd() //전투 종료시 저장된 몬스터의 정보들 삭제.
    {
        monstersInCombat.Clear();
        monsterObject.Clear();
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
            var obj = Instantiate(monsterList[i].prefab, new Vector3(0, 0, 0), Quaternion.identity);
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
    private void tesq()
    {
        Monster monster = new Monster();
        monster = monsterList[0];
    }
}

public class monsterDummy : Monster
{

}
