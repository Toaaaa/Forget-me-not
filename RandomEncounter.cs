using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    private Player player;

    [SerializeField] private int encounterStep=10; //처음 몬스터 타일에 진입후 몬스터 인카운터가 발생할수 있는 최소 스텝(1블록)횟수.
    [SerializeField] private int encounterCount=0; //현재 스텝 횟수.
    [SerializeField] private int encounterRate = 9; //몬스터 인카운터 확률. random(1~100) < encounterrate이면 몬스터 인카운터.

    private float timeMoved;
    private float encounterTime = 1.2f; //몬스터 인카운터 시간 간격.

    private bool checkEncounter; //몬스터 인카운터 체크.
    private bool isMonsterZone; //인카운터 발생하는 타일인지 체크.

    private TileManager tilemanager;

    private void Awake()
    {
        player = GetComponent<Player>();
        tilemanager = FindObjectOfType<TileManager>();
        encounterRate = GameManager.Instance.eventManager.encounterRate;
    }


    private void GetTimeMoved() //플레이어가 움직일때 시간을 카운팅.
    {
        float deltaTime = Time.deltaTime;
        timeMoved += deltaTime;
        if(timeMoved >= encounterTime)
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
        if (checkEncounter)
        {
            int random = Random.Range(1, 100);
            if (random < encounterRate)
                Encountered();

            checkEncounter = false;
        }
    }


    private void Encountered()//encounter monster
    {
        encounterCount = 0; //몬스터 인카운터가 발생하면 스텝을 초기화.
        GameManager.Instance.eventManager.encounterEvent(); //Start the encounter event.
    }


    private void Update()
    {
        
        if(tilemanager != null)
        {
            isMonsterZone = tilemanager.IsMonsterZone(player.transform.position); //플레이어가 있는 타일이 몬스터 타일인지 체크.
        }
         else { Debug.Log("TileManager is null");}

        if (player.isMoving && isMonsterZone)
        {
            GetTimeMoved(); //플레이어가 움직이면 시간을 카운팅함.
            EncounterCheck(); //몬스터 인카운터 체크.
        }

    }

}
