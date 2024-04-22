using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    public CombatManager combatManager;
    public List<PlayableC> playerList;
    public List<CombatSlot> slotList; //플레이어의 애니메이션 등이 출력되는 곳.
    public List<CombatStatus> statusUI;
    public List<MobSlot> mobSlotList; //슬롯만 저장됨
    public List<GameObject> MobList; //위의 슬롯에서 몬스터가 있는곳만 찾아서 저장 

    public CombatSlot selectedSlot; //선택된 슬롯. 스킬 사용시 or 아이템 사용시 이 슬롯을 대상으로 함.

    public int selectedSlotIndex; //선택된 슬롯의 인덱스.
    public int selectedMobIndex; //선택된 몬스터의 인덱스.
    public bool duringSceneChange; //현재 전투 입장 애니메이션이 출력 중일 경우.
    public float MyturnTime; //플레이어의 턴 시간.
    public float EnemyturnTime; //적의 턴 시간.
    public bool isPlayerTurn; //플레이어의 턴인지 아닌지 판별하는 변수. //플레이어의 턴을 다썼고 적의 턴일 경우 false가 됨.

    public PlayableC selectingPlayer;//공격,아이템,스킬 등을 실행할 플레이어.
    public CombatSelection combatSelection;//위의 플레이어의 selection을 담당하는 곳.

    public bool attackSelected; //공격이 선택되었는지 판별하는 변수.firstSelection에서 기본공격 선택시.
    public bool skillSelected; //스킬이 선택되었는지 판별하는 변수.firstSelection에서 스킬 선택시.
    public bool itemSelected; //아이템이 선택되었는지 판별하는 변수.firstSelection에서 아이템 선택시.

    private void Update()
    {
        if(playerList.Count !=0 && slotList[0].player ==null) //조건을 걸고 onenable을 대신함.
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                slotList[i].player = playerList[i];
                statusUI[i].player = playerList[i];
                statusUI[i].OnPlayerHpSet(); //플레이어의 체력바를 세팅해줌.
                statusUI[i].OnPlayerMpSet(); //플레이어의 마나바를 세팅해줌.
            }
        }
        for(int i = 0; i < statusUI.Count; i++)
        {
            if (statusUI[i].player != null)
            {
                statusUI[i].gameObject.SetActive(true);
            }
            else
            {
                statusUI[i].gameObject.SetActive(false);
            }
        }

        selectSlot();
        TurnTimeCheck();
        if (attackSelected)
        {
            selectEnemy();
        }
    }

    private void OnEnable()
    {
        selectedSlotIndex = 0;
        for(int i=0; i< statusUI.Count; i++)
        {
            statusUI[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            slotList[i].player = null;
            statusUI[i].player = null;
        }
        statusUI.ForEach(x => x.playerImage.sprite = null);
    }
    private void selectSlot()
    {
        if (isPlayerTurn && !duringSceneChange&& !combatManager.isFirstSelection)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(selectedSlotIndex < slotList.Count-1)
                {
                    selectedSlotIndex++;
                }
                else
                {
                    selectedSlotIndex = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (selectedSlotIndex > 0)
                {
                    selectedSlotIndex--;
                }
                else
                {
                    selectedSlotIndex = slotList.Count - 1;
                }
            }
        }
        selectedSlot = slotList[selectedSlotIndex];
    }//플레이어의 턴일때 플레이어 슬롯을 선택할 수 있다.
    private void TurnTimeCheck()
    {
        if(MyturnTime<=0)
        {
            isPlayerTurn = false;
        }
        else
        {
            isPlayerTurn = true;
        }
    }//플레이어의 턴이 끝났는지 확인하는 함수.

    private void selectEnemy()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(selectedMobIndex < MobList.Count-1)
            {
                selectedMobIndex++;
            }
            else
            {
                selectedMobIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedMobIndex > 0)
            {
                selectedMobIndex--;
            }
            else
            {
                selectedMobIndex = MobList.Count - 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))// 선택된 몬스터 공격(기본공격)
        {
            combatManager.monsterSelected = MobList[selectedMobIndex].GetComponent<TestMob>().gameObject;
            selectingPlayer.Attack();
            combatManager.isFirstSelection = false;
            attackSelected = false;
            Debug.Log("공격을 하였음.");
            //여기서 이제 시간이 남았을 경우 다음 플레이어의 동작 메뉴 오픈.
            if (isPlayerTurn)
            {
                selectedSlotIndex = 0;
                combatSelection = slotList[selectedSlotIndex].combatSelection;
                combatSelection.charSelection.SetActive(true);

            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            attackSelected = false;
            combatSelection.firstSelection.SetActive(true);
        }

    }
}
