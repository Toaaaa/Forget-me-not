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
    public List<GameObject> MobList; //위의 슬롯에서 몬스터가 있는곳만 찾아서 저장 (사망시 제거 가능한 몬스터 오브젝트 리스트)

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

    public bool inAction;//플레이어가 행동 중인경우. 동시에 여러 행동이 겹치지 않게만든 변수.

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
        WhenDie();
        if (attackSelected)
        {
            selectEnemy();
        }
        if (skillSelected)
        {
            SkillOnSelect();
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
            inAction = true;
            selectingPlayer.Attack();
            StartCoroutine(inaction());
            combatManager.isFirstSelection = false;
            attackSelected = false;
            combatManager.monsterSelected = null;
            selectedMobIndex = 0;
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

    }//기본공격시 몬스터 선택+공격하는 함수.
    public void SkillOnSelect() //스킬을 사용할 대상 선택.
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (combatManager.combatDisplay.selectedMobIndex < combatManager.combatDisplay.MobList.Count - 1)
            {
                combatManager.combatDisplay.selectedMobIndex++;
            }
            else
            {
                combatManager.combatDisplay.selectedMobIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (combatManager.combatDisplay.selectedMobIndex > 0)
            {
                combatManager.combatDisplay.selectedMobIndex--;
            }
            else
            {
                combatManager.combatDisplay.selectedMobIndex = combatManager.combatDisplay.MobList.Count - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))// 선택된 몬스터 공격
        {
            combatManager.combatDisplay.combatManager.monsterSelected = combatManager.combatDisplay.MobList[combatManager.combatDisplay.selectedMobIndex].GetComponent<TestMob>().gameObject;
            combatManager.combatDisplay.inAction = true;
            switch (combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex)
            {
                case 0:
                    combatManager.combatDisplay.selectingPlayer.Skill1();
                    break;
                case 1:
                    combatManager.combatDisplay.selectingPlayer.Skill2();
                    break;
                case 2:
                    combatManager.combatDisplay.selectingPlayer.Skill3();
                    break;
                case 3://탱커의 경우 4번스킬이 없고, 3번,4번 스킬은 레벨이 오름에 따라서 해제되는 방식.
                    combatManager.combatDisplay.selectingPlayer.Skill4();
                    break;
            }
            StartCoroutine(inaction());
            combatManager.combatDisplay.combatManager.isFirstSelection = false;
            combatManager.combatDisplay.skillSelected = false;
            combatManager.combatDisplay.combatManager.monsterSelected = null;
            combatManager.combatDisplay.selectedMobIndex = 0;
            Debug.Log("공격을 하였음.");
            //여기서 이제 시간이 남았을 경우 다음 플레이어의 동작 메뉴 오픈.
            if (combatManager.combatDisplay.isPlayerTurn)
            {
                combatManager.combatDisplay.selectedSlotIndex = 0;
                combatManager.combatDisplay.combatSelection = combatManager.combatDisplay.slotList[combatManager.combatDisplay.selectedSlotIndex].combatSelection;
                combatManager.combatDisplay.combatSelection.charSelection.SetActive(true);

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            combatManager.combatDisplay.skillSelected = false;
            combatManager.combatDisplay.combatSelection.skillSelection.SetActive(true);
        }
    }
    private void WhenDie()
    {
        if (combatManager.isCombatStart)
        {
            for(int i = 0; i < MobList.Count; i++)
            {
                if (MobList[i].GetComponent<TestMob>().Hp <= 0)
                {
                    MobList[i].SetActive(false);
                    MobList.RemoveAt(i);
                }
            }
        }
    }
    public void AtActionEnd()
    {

    }//플레이어의 행동이 끝났을때 실행되는 함수 (몬스터의 사망확인후, 사망 모션 실행, 버프형 스킬의 이펙트 실행등..)


    IEnumerator inaction() //전투 중 행동을 취하고 있을때 다른 스크립트와의 충돌을 방지하기 위한 코루틴.
    {
        yield return new WaitForSeconds(0.4f);
        inAction = false;
    }
}
