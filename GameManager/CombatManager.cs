using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;

    public List<PlayableC> playerList;
    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //버프아이템은 한번에 하나만 적용 되도록. //만약에 다른 버프 사용중에 버프아이템을 사용 할 경우 이전 버프는 사라짐.
    //버프 아이템 사용중에는 파티원의 인원변경 불가능.
    


    private void Start()
    {
        playerList = new List<PlayableC>();
        playerList = playableManager.joinedPlayer;

    }
    public void OnCombatStart()//전투 시작시 호출되는 함수.
    {
        playerList = new List<PlayableC>();//플레이어 리스트 초기화
        playerList = playableManager.joinedPlayer;

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

}
