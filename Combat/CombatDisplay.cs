using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    public Inventory inventory;
    public CombatManager combatManager;
    public List<PlayableC> playerList;
    public List<CombatSlot> slotList; //플레이어의 애니메이션 등이 출력되는 곳.
    public List<CombatStatus> statusUI;
    public List<MobSlot> mobSlotList; //슬롯만 저장됨
    public List<GameObject> MobList; //위의 슬롯에서 몬스터가 있는곳만 찾아서 저장 (사망시 제거 가능한 몬스터 오브젝트 리스트)

    public CombatSlot selectedSlot; //선택된 슬롯. 스킬 사용시 or 아이템 사용시 이 슬롯을 대상으로 함.

    public int tempIndex;
    public int selectedSlotIndex; //선택된 슬롯의 인덱스.
    public int selectedMobIndex; //선택된 몬스터의 인덱스.
    public bool duringSceneChange; //현재 전투 입장 애니메이션이 출력 중일 경우.
    public bool isPlayerTurn; //플레이어의 턴인지 아닌지 판별하는 변수. //플레이어의 턴을 다썼고 적의 턴일 경우 false가 됨.

    public PlayableC selectingPlayer;//공격,아이템,스킬 등을 실행할 플레이어.
    public CombatSelection combatSelection;//위의 플레이어의 selection을 담당하는 곳.
    public Item selectingItem;//선택된 아이템.
    public ItemSelection itemInven;//위의 아이템을 선택하는 곳.

    private bool selectUp; //selectslot에서 방금 up키를 눌러서 변경 되었는지 판별.
    public bool attackSelected; //공격이 선택되었는지 판별하는 변수.firstSelection에서 기본공격 선택시.
    public bool skillSelected; //스킬이 선택되었는지 판별하는 변수.firstSelection에서 스킬 선택시.
    public bool skillSelectedForPlayer; //스킬이 선택되었는지 판별 + 해당 스킬이 플레이어 대상일때 사용.
    public bool skillForAllPlayer; //모든 플레이어를 대상으로 하는 스킬인지 판별하는 변수.
    public bool skillForAllMob; //모든 몬스터를 대상으로 하는 스킬인지 판별하는 변수.
    public bool itemSelected; //아이템이 선택되었는지 판별하는 변수.firstSelection에서 아이템 선택시.
    public bool BuffItemSelected; //버프형 아이템이 선택되었는지 판별하는 변수.

    //코루틴에서 쓸 변수
    public bool inAction;//플레이어가 행동 중인경우. 동시에 여러 행동이 겹치지 않게만든 변수.
    public bool noCharObj;//charselection오브젝트를 비활성화 하기위해 사용하는 변수.

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
        if (!isPlayerTurn)
        {
            IsNotPlayerTurn();           
        }
        selectSlot();
        WhenDie();
        if (attackSelected)
        {
            selectEnemy();
        }//기본 공격
        if (skillSelected)
        {
            if(selectingPlayer.name == "Magician" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 0)
            {
                SkillOnSelectAll();//광역스킬.
            }
            else if (selectingPlayer.name == "Magician" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 2)
            {
                SkillOnSelectAll();//광역스킬.
            }
            else if(selectingPlayer.name == "Tank" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 2)
            {
                SkillOnSelectAll();//광역스킬.
            }
            else
            {
                SkillOnSelect();
            }
        }//몬스터 대상 스킬.
        if(itemSelected)
        {
            ItemOnSelect();
        }
        if(BuffItemSelected)
        {
            BuffItemOnselect();
        }
        if(skillSelectedForPlayer)
        {
            if(selectingPlayer.name == "Magician" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 1)
            {
                SkillOnSelectForAllPlayer();//모든 플레이어 대상 스킬.
            }
            else if(selectingPlayer.name == "Healer" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 1)
            {
                SkillOnSelectForAllPlayer();//모든 플레이어 대상 스킬.
            }
            else if(selectingPlayer.name == "Tank" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 0)
            {
                SkillOnSelectForAllPlayer();//모든 플레이어 대상 스킬.
            }
            else
            {
                SkillOnSelectForPlayer();//단일 플레이어 대상 스킬.
            }
        }//플레이어 대상 스킬.
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
        if (isPlayerTurn && !duringSceneChange&& !combatManager. isFirstSelection)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectUp = false;
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
                selectUp = true;
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
        if (slotList[selectedSlotIndex].player.isDead)
        {
            if(selectingPlayer != null)
            {
                if (selectingPlayer.name == "Healer" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 3)
                {
                    selectedSlot = slotList[selectedSlotIndex];
                }
                else
                {
                    if (selectUp)
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
                    else
                    {
                        if (selectedSlotIndex < slotList.Count - 1)
                        {
                            selectedSlotIndex++;
                        }
                        else
                        {
                            selectedSlotIndex = 0;
                        }
                    }
                }
            }
            else
            {
                if (selectUp)
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
                else
                {
                    if (selectedSlotIndex < slotList.Count - 1)
                    {
                        selectedSlotIndex++;
                    }
                    else
                    {
                        selectedSlotIndex = 0;
                    }
                }
            }
        }
        else
        {
            if(isPlayerTurn)
                 selectedSlot = slotList[selectedSlotIndex];
        }
    }//플레이어의 턴일때 플레이어 슬롯을 선택할 수 있다.

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
            combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex = 0;
            combatSelection.firstSelection.GetComponent<FirstSelection>().selectionIndex = 0;
            for (int i = 1; i < 4; i++)
            {
                combatSelection.firstSelection.GetComponent<FirstSelection>().selection[i].SetActive(false);
            }
            combatSelection.firstSelection.GetComponent<FirstSelection>().selection[0].SetActive(true);
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
    public void SkillOnSelectAll() //광역 스킬.
    {
        Debug.Log("광역 스킬 사용중");
        skillForAllMob = true;
        combatManager.monsterSelected = null;
        if (Input.GetKeyDown(KeyCode.Space))// 선택된 몬스터 공격
        {
            combatManager.monsterSelected = MobList[selectedMobIndex].GetComponent<TestMob>().gameObject;
            inAction = true;
            switch (combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex)
            {
                case 0:
                    selectingPlayer.Skill1();
                    break;
                case 1:
                    selectingPlayer.Skill2();
                    break;
                case 2:
                    selectingPlayer.Skill3();
                    break;
                case 3://탱커의 경우 4번스킬이 없고, 3번,4번 스킬은 레벨이 오름에 따라서 해제되는 방식.
                    selectingPlayer.Skill4();
                    break;
            }
            StartCoroutine(inaction());
            combatManager.isFirstSelection = false;
            skillSelected = false;
            combatManager.monsterSelected = null;
            skillForAllMob = false;
            selectedMobIndex = 0;
            combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex = 0;
            combatSelection.firstSelection.GetComponent<FirstSelection>().selectionIndex = 0;
            for (int i = 1; i < 4; i++)
            {
                combatSelection.firstSelection.GetComponent<FirstSelection>().selection[i].SetActive(false);
            }
            combatSelection.firstSelection.GetComponent<FirstSelection>().selection[0].SetActive(true);
            //여기서 이제 시간이 남았을 경우 다음 플레이어의 동작 메뉴 오픈.
            if (isPlayerTurn)
            {
                selectedSlotIndex = 0;
                combatSelection = slotList[selectedSlotIndex].combatSelection;
                combatSelection.charSelection.SetActive(true);

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skillForAllMob = false;
            skillSelected = false;
            combatSelection.skillSelection.SetActive(true);
        }
    }
    private void SkillOnSelectForPlayer()//단일 플레이어를 대상으로 쓰는 스킬. (힐러밖에 사용 안한다는 가정하에 만들어 져서 다른 캐릭터에 적용시 수정 필요)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectUp = false;
            if (selectedSlotIndex < slotList.Count - 1)
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
            selectUp = true;
            if (selectedSlotIndex > 0)
            {
                selectedSlotIndex--;
            }
            else
            {
                selectedSlotIndex = slotList.Count - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))// 선택된 몬스터 공격
        {

            combatManager.selectedPlayer= slotList[selectedSlotIndex].player;
            inAction = true;
            switch (combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex)
            {
                case 0:
                    selectingPlayer.Skill1();
                    break;
                case 1:
                    selectingPlayer.Skill2();
                    break;
                case 2:
                    selectingPlayer.Skill3();
                    break;
                case 3://탱커의 경우 4번스킬이 없고, 3번,4번 스킬은 레벨이 오름에 따라서 해제되는 방식.
                    selectingPlayer.Skill4();
                    break;
            }
            StartCoroutine(inaction());
            combatManager.isFirstSelection = false;
            skillSelected = false;
            combatManager.monsterSelected = null;
            selectedSlotIndex = 0;
            skillSelectedForPlayer = false;
            combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex = 0;
            combatSelection.firstSelection.GetComponent<FirstSelection>().selectionIndex = 0;
            for (int i = 1; i < 4; i++)
            {
                combatSelection.firstSelection.GetComponent<FirstSelection>().selection[i].SetActive(false);
            }
            combatSelection.firstSelection.GetComponent<FirstSelection>().selection[0].SetActive(true);
            //여기서 이제 시간이 남았을 경우 다음 플레이어의 동작 메뉴 오픈.
            if (isPlayerTurn)
            {
                selectedSlotIndex = 0;
                combatSelection = slotList[selectedSlotIndex].combatSelection;
                combatSelection.charSelection.SetActive(true);

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for(int i=0; i < slotList.Count; i++)
            {
                if (slotList[i].player.name =="Healer")
                {
                    selectedSlotIndex = i;
                }
            }
            combatSelection = slotList[selectedSlotIndex].combatSelection;
            combatSelection.skillSelection.SetActive(true);
            noCharObj = true;
            combatSelection.charSelection.SetActive(false);
            StartCoroutine(CoroutineForSkillSelection());
            skillSelectedForPlayer = false;
            skillSelected = false;
        }
    }
    private void SkillOnSelectForAllPlayer()//모든 플레이어를 대상으로 쓰는 스킬.
    {       
        skillForAllPlayer = true;
        if (Input.GetKeyDown(KeyCode.Space))// 선택된 몬스터 공격
        {
            CharAllOf();//캐릭터 화살표 전부 끄기.
            combatManager.selectedPlayer = slotList[selectedSlotIndex].player;
            inAction = true;
            switch (combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex)
            {
                case 0:
                    selectingPlayer.Skill1();
                    break;
                case 1:
                    selectingPlayer.Skill2();
                    break;
                case 2:
                    selectingPlayer.Skill3();
                    break;
                case 3://탱커의 경우 4번스킬이 없고, 3번,4번 스킬은 레벨이 오름에 따라서 해제되는 방식.
                    selectingPlayer.Skill4();
                    break;
            }
            StartCoroutine(inaction());
            combatManager.isFirstSelection = false;
            skillSelected = false;
            combatManager.monsterSelected = null;
            selectedSlotIndex = 0;
            skillSelectedForPlayer = false;
            skillForAllPlayer = false;
            combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex = 0;
            combatSelection.firstSelection.GetComponent<FirstSelection>().selectionIndex = 0;
            for(int i=1; i<4; i++)
            {
                combatSelection.firstSelection.GetComponent<FirstSelection>().selection[i].SetActive(false);
            }
            combatSelection.firstSelection.GetComponent<FirstSelection>().selection[0].SetActive(true);
            //여기서 이제 시간이 남았을 경우 다음 플레이어의 동작 메뉴 오픈.
            if (isPlayerTurn)
            {
                selectedSlotIndex = 0;
                combatSelection = slotList[selectedSlotIndex].combatSelection;
                combatSelection.charSelection.SetActive(true);

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skillForAllPlayer = false;
            skillSelectedForPlayer = false;
            skillSelected = false;
            selectedSlot.combatSelection.charSelection.SetActive(false);
            combatSelection.skillSelection.SetActive(true);
        }
    }

    private void ItemOnSelect()//아이템을 사용할 대상 선택
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inAction = true;
            combatSelection.itemSelection.SetActive(false);
            combatSelection.charSelection.SetActive(true);
            combatManager.isFirstSelection = false;
            selectingPlayer = slotList[selectedSlotIndex].player;
            selectedSlotIndex = 0;
            itemSelected = false;
            UseItem();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedSlotIndex = tempIndex;
            itemSelected = false;
            selectingItem = null;
            combatSelection.itemSelection.SetActive(true);
            noCharObj = true;
            StartCoroutine(CoroutineForNoChar());
            combatSelection.charSelection.SetActive(false);
        }
        Debug.Log("아이템 사용중");
    }//아이템을 사용할 플레이어 대상 선택
    private void BuffItemOnselect()
    {
        for(int i = 0; i < slotList.Count; i++)
        {
            slotList[i].combatSelection.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inAction = true;
            combatSelection.itemSelection.SetActive(false);
            combatSelection.charSelection.SetActive(true);
            selectingPlayer = slotList[selectedSlotIndex].player;
            selectedSlotIndex = 0;
            combatManager.isFirstSelection = false;
            BuffItemSelected = false;
            UseItem();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedSlotIndex = tempIndex;
            BuffItemSelected = false;
            for (int i = 0; i < slotList.Count; i++)
            {
                slotList[i].combatSelection.charSelection.SetActive(false);
                slotList[i].combatSelection.gameObject.SetActive(false);
            }
            noCharObj = true;

            itemSelected = false;
            selectingItem = null;
            noCharObj = true;
            StartCoroutine(CoroutineForselection());
            combatSelection.itemSelection.SetActive(true);
        }
    }
    private void UseItem()
    {
        //seletingitem을 사용.+selectedplayer에게 사용.
        StartCoroutine(inaction());
        inventory.Container[selectingItem.itemID].amount--;
        ConsumeItem consumeItem = (ConsumeItem)selectingItem;
        consumeItem.OnUse(selectingPlayer);
        combatSelection.firstSelection.GetComponent<FirstSelection>().selectionIndex = 0;
        combatManager.selectedPlayer = null;//아이템을 사용후 지정된 플레이어 초기화.
        selectingItem = null;//아이템을 사용후 지정된 아이템 초기화.
        itemSelected = false;//아이템 사용중 변수 초기화.
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

    private void IsNotPlayerTurn()//플레이어의 턴이 아닐때 실행되는 함수.
    {
        OffUIWhenTurnOver();
        for (int i = 0; i < slotList.Count; i++)//플레이어의 턴이 아닐게 됫을때. 플레이어의 선택창 전부 비활성화.
        {
            slotList[i].combatSelection.charSelection.SetActive(false);
        }
        //플레이어 턴이 아니면 현재 띄워진 ui들을 전부 닫기.
    }
    private void CharAllOf()//캐릭터 화살표 전부 끄기.
    {
        for(int i = 0; i < slotList.Count; i++)
        {
            slotList[i].combatSelection.charSelection.SetActive(false);
        }
    }
    public void OffUIWhenTurnOver()//플레이어의 턴이 끝났을때 ui전부 끄는 함수
    {
        //선택중이였던 셀렉션에서의 index 초기화 + selectedslot 초기화.
        if(selectedSlot != null)
        {
            selectedSlot.combatSelection.firstSelection.GetComponent<FirstSelection>().selectionIndex = 0;
            selectedSlot.combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex = 0;
            selectedSlot.combatSelection.firstSelection.gameObject.SetActive(false);
            selectedSlot.combatSelection.skillSelection.SetActive(false);
        }
        selectedSlot = null;
        for(int i =0; i< mobSlotList.Count; i++)
        {
            mobSlotList[i].selectingArrow.SetActive(false);
        }
        itemInven.gameObject.SetActive(false);
    }
    public void courountineGo()
    {
        StartCoroutine(CoroutineForItemSelection());
    }

    IEnumerator inaction() //전투 중 행동을 취하고 있을때 다른 스크립트와의 충돌을 방지하기 위한 코루틴.
    {
        yield return new WaitForSeconds(0.4f);
        inAction = false;
    }
    IEnumerator CoroutineForSkillSelection()
    {
        yield return new WaitForSeconds(0.1f);
        noCharObj = false;
        combatSelection.skillSelection.SetActive(true);
    }
    IEnumerator CoroutineForItemSelection()
    {
        combatSelection.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        noCharObj = false;
        combatSelection.firstSelection.SetActive(true);
    }
    IEnumerator CoroutineForselection()
    {
        yield return new WaitForSeconds(0.1f);
        noCharObj = false;
        combatSelection.gameObject.SetActive(true);
    }
    IEnumerator CoroutineForNoChar()
    {
        yield return new WaitForSeconds(0.1f);
        noCharObj = false;
    }

}
