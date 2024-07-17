using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    private Player player;

    [SerializeField] private int encounterStep=10; //처음 몬스터 타일에 진입후 몬스터 인카운터가 발생할수 있는 최소 스텝(1블록)횟수.
    public int encounterCount=0; //현재 스텝 횟수.
    public int encounterRate ; //몬스터 인카운터 확률. random(1~100) < encounterrate이면 몬스터 인카운터.
    private int extraEncounterRate; //엔카운터 실패시 인카운터 확률을 올리기 위한 변수.

    public float timeMoved;
    private float encounterTime = 1.2f; //몬스터 인카운터 시간 간격.

    private bool checkEncounter; //몬스터 인카운터 체크.
    private bool isMonsterZone; //인카운터 발생하는 타일인지 체크.

    private TileManager tilemanager;

    private void Awake()
    {
        player = GetComponent<Player>();
        tilemanager = FindObjectOfType<TileManager>();
    }


    private void GetTimeMoved() //플레이어가 움직일때 시간을 카운팅.
    {
        float deltaTime = Time.deltaTime;
        timeMoved += deltaTime;
        if(timeMoved >= encounterTime) //움직인 시간이 encountetTime 보다 크면
        {
            timeMoved = 0;
            encounterCount++; //every "encounterTime" seconds, it counts as 1 step.

            if (encounterCount >= encounterStep) //it starts to check encounterrate.
            {
                checkEncounter = true;//몬스터 인카운터 함수를 실행.
            }
        }
    }

    private void EncounterCheck()
    {
        if (checkEncounter)//조건이 달성 되면 매 encounterTime 만큼 움직일때 마다 인카운터 체크를 함.
        {
            if(encounterRate != 0) //혹시 몬스터가 등장하지 않는 곳일 경우를 대비하여 0일때만 확률 올림.
            {
                extraEncounterRate += 1; //몬스터 인카운터 실패시 추가 확률을 올림.
            }
            int random = Random.Range(1, 100);
            if (random < encounterRate + extraEncounterRate)
                Encountered();

            checkEncounter = false;
        }
    }


    private void Encountered()//encounter monster
    {
        if(encounterRate != 0 && CombatManager.Instance.mapData.encounterRate !=0) //사전에 축적된 extraEncounterRate때문에 0이 아닌곳에서 인카운터 발생가능성 방지.
        {
            Debug.Log(encounterRate +"_"+ extraEncounterRate);
            encounterCount = 0; //몬스터 인카운터가 발생하면 스텝을 초기화.
            extraEncounterRate = 0; //몬스터 인카운터가 발생하면 추가 확률을 초기화.
            GameManager.Instance.combatManager.OnCombatStart();
        }
    }


    private void Update()
    {
        if(tilemanager != null)
        {
            isMonsterZone = tilemanager.IsMonsterZone(player.transform.position); //플레이어가 있는 타일이 몬스터 타일인지 체크.
        }
         else 
        {
            tilemanager = FindObjectOfType<TileManager>();
        }

        if (player.isMoving && isMonsterZone)
        {
            GetTimeMoved(); //플레이어가 움직이면 시간을 카운팅함.
            EncounterCheck(); //몬스터 인카운터 체크.
        }

        /////////////////////////// 이 밑은 스토리 이벤트 타일.
        if(tilemanager != null)
        {
            tilemanager.IsStoryTile(player.transform.position); //플레이어가 있는 타일이 스토리 타일인지 체크.
        }
         else
        {
            tilemanager = FindObjectOfType<TileManager>();
        }
    }

}
