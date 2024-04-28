using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    public Inventory inventory;
    public CombatManager combatManager;
    public List<PlayableC> playerList;
    public List<CombatSlot> slotList; //�÷��̾��� �ִϸ��̼� ���� ��µǴ� ��.
    public List<CombatStatus> statusUI;
    public List<MobSlot> mobSlotList; //���Ը� �����
    public List<GameObject> MobList; //���� ���Կ��� ���Ͱ� �ִ°��� ã�Ƽ� ���� (����� ���� ������ ���� ������Ʈ ����Ʈ)

    public CombatSlot selectedSlot; //���õ� ����. ��ų ���� or ������ ���� �� ������ ������� ��.

    public int tempIndex;
    public int selectedSlotIndex; //���õ� ������ �ε���.
    public int selectedMobIndex; //���õ� ������ �ε���.
    public bool duringSceneChange; //���� ���� ���� �ִϸ��̼��� ��� ���� ���.
    public bool isPlayerTurn; //�÷��̾��� ������ �ƴ��� �Ǻ��ϴ� ����. //�÷��̾��� ���� �ٽ�� ���� ���� ��� false�� ��.

    public PlayableC selectingPlayer;//����,������,��ų ���� ������ �÷��̾�.
    public CombatSelection combatSelection;//���� �÷��̾��� selection�� ����ϴ� ��.
    public Item selectingItem;//���õ� ������.
    public ItemSelection itemInven;//���� �������� �����ϴ� ��.

    private bool selectUp; //selectslot���� ��� upŰ�� ������ ���� �Ǿ����� �Ǻ�.
    public bool attackSelected; //������ ���õǾ����� �Ǻ��ϴ� ����.firstSelection���� �⺻���� ���ý�.
    public bool skillSelected; //��ų�� ���õǾ����� �Ǻ��ϴ� ����.firstSelection���� ��ų ���ý�.
    public bool skillSelectedForPlayer; //��ų�� ���õǾ����� �Ǻ� + �ش� ��ų�� �÷��̾� ����϶� ���.
    public bool skillForAllPlayer; //��� �÷��̾ ������� �ϴ� ��ų���� �Ǻ��ϴ� ����.
    public bool skillForAllMob; //��� ���͸� ������� �ϴ� ��ų���� �Ǻ��ϴ� ����.
    public bool itemSelected; //�������� ���õǾ����� �Ǻ��ϴ� ����.firstSelection���� ������ ���ý�.
    public bool BuffItemSelected; //������ �������� ���õǾ����� �Ǻ��ϴ� ����.

    //�ڷ�ƾ���� �� ����
    public bool inAction;//�÷��̾ �ൿ ���ΰ��. ���ÿ� ���� �ൿ�� ��ġ�� �ʰԸ��� ����.
    public bool noCharObj;//charselection������Ʈ�� ��Ȱ��ȭ �ϱ����� ����ϴ� ����.

    private void Update()
    {
        if(playerList.Count !=0 && slotList[0].player ==null) //������ �ɰ� onenable�� �����.
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                slotList[i].player = playerList[i];
                statusUI[i].player = playerList[i];
                statusUI[i].OnPlayerHpSet(); //�÷��̾��� ü�¹ٸ� ��������.
                statusUI[i].OnPlayerMpSet(); //�÷��̾��� �����ٸ� ��������.
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
        }//�⺻ ����
        if (skillSelected)
        {
            if(selectingPlayer.name == "Magician" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 0)
            {
                SkillOnSelectAll();//������ų.
            }
            else if (selectingPlayer.name == "Magician" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 2)
            {
                SkillOnSelectAll();//������ų.
            }
            else if(selectingPlayer.name == "Tank" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 2)
            {
                SkillOnSelectAll();//������ų.
            }
            else
            {
                SkillOnSelect();
            }
        }//���� ��� ��ų.
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
                SkillOnSelectForAllPlayer();//��� �÷��̾� ��� ��ų.
            }
            else if(selectingPlayer.name == "Healer" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 1)
            {
                SkillOnSelectForAllPlayer();//��� �÷��̾� ��� ��ų.
            }
            else if(selectingPlayer.name == "Tank" && combatSelection.skillSelection.GetComponent<SkillSelection>().skillIndex == 0)
            {
                SkillOnSelectForAllPlayer();//��� �÷��̾� ��� ��ų.
            }
            else
            {
                SkillOnSelectForPlayer();//���� �÷��̾� ��� ��ų.
            }
        }//�÷��̾� ��� ��ų.
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
    }//�÷��̾��� ���϶� �÷��̾� ������ ������ �� �ִ�.

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
        if(Input.GetKeyDown(KeyCode.Space))// ���õ� ���� ����(�⺻����)
        {
            combatManager.monsterSelected = MobList[selectedMobIndex].GetComponent<TestMob>().gameObject;
            inAction = true;
            selectingPlayer.Attack();
            StartCoroutine(inaction());
            combatManager.isFirstSelection = false;
            attackSelected = false;
            combatManager.monsterSelected = null;
            selectedMobIndex = 0;
            Debug.Log("������ �Ͽ���.");
            //���⼭ ���� �ð��� ������ ��� ���� �÷��̾��� ���� �޴� ����.
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

    }//�⺻���ݽ� ���� ����+�����ϴ� �Լ�.
    public void SkillOnSelect() //��ų�� ����� ��� ����.
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
        if (Input.GetKeyDown(KeyCode.Space))// ���õ� ���� ����
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
                case 3://��Ŀ�� ��� 4����ų�� ����, 3��,4�� ��ų�� ������ ������ ���� �����Ǵ� ���.
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
            //���⼭ ���� �ð��� ������ ��� ���� �÷��̾��� ���� �޴� ����.
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
    public void SkillOnSelectAll() //���� ��ų.
    {
        Debug.Log("���� ��ų �����");
        skillForAllMob = true;
        combatManager.monsterSelected = null;
        if (Input.GetKeyDown(KeyCode.Space))// ���õ� ���� ����
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
                case 3://��Ŀ�� ��� 4����ų�� ����, 3��,4�� ��ų�� ������ ������ ���� �����Ǵ� ���.
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
            //���⼭ ���� �ð��� ������ ��� ���� �÷��̾��� ���� �޴� ����.
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
    private void SkillOnSelectForPlayer()//���� �÷��̾ ������� ���� ��ų. (�����ۿ� ��� ���Ѵٴ� �����Ͽ� ����� ���� �ٸ� ĳ���Ϳ� ����� ���� �ʿ�)
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
        if (Input.GetKeyDown(KeyCode.Space))// ���õ� ���� ����
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
                case 3://��Ŀ�� ��� 4����ų�� ����, 3��,4�� ��ų�� ������ ������ ���� �����Ǵ� ���.
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
            //���⼭ ���� �ð��� ������ ��� ���� �÷��̾��� ���� �޴� ����.
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
    private void SkillOnSelectForAllPlayer()//��� �÷��̾ ������� ���� ��ų.
    {       
        skillForAllPlayer = true;
        if (Input.GetKeyDown(KeyCode.Space))// ���õ� ���� ����
        {
            CharAllOf();//ĳ���� ȭ��ǥ ���� ����.
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
                case 3://��Ŀ�� ��� 4����ų�� ����, 3��,4�� ��ų�� ������ ������ ���� �����Ǵ� ���.
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
            //���⼭ ���� �ð��� ������ ��� ���� �÷��̾��� ���� �޴� ����.
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

    private void ItemOnSelect()//�������� ����� ��� ����
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
        Debug.Log("������ �����");
    }//�������� ����� �÷��̾� ��� ����
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
        //seletingitem�� ���.+selectedplayer���� ���.
        StartCoroutine(inaction());
        inventory.Container[selectingItem.itemID].amount--;
        ConsumeItem consumeItem = (ConsumeItem)selectingItem;
        consumeItem.OnUse(selectingPlayer);
        combatSelection.firstSelection.GetComponent<FirstSelection>().selectionIndex = 0;
        combatManager.selectedPlayer = null;//�������� ����� ������ �÷��̾� �ʱ�ȭ.
        selectingItem = null;//�������� ����� ������ ������ �ʱ�ȭ.
        itemSelected = false;//������ ����� ���� �ʱ�ȭ.
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

    }//�÷��̾��� �ൿ�� �������� ����Ǵ� �Լ� (������ ���Ȯ����, ��� ��� ����, ������ ��ų�� ����Ʈ �����..)

    private void IsNotPlayerTurn()//�÷��̾��� ���� �ƴҶ� ����Ǵ� �Լ�.
    {
        OffUIWhenTurnOver();
        for (int i = 0; i < slotList.Count; i++)//�÷��̾��� ���� �ƴҰ� ������. �÷��̾��� ����â ���� ��Ȱ��ȭ.
        {
            slotList[i].combatSelection.charSelection.SetActive(false);
        }
        //�÷��̾� ���� �ƴϸ� ���� ����� ui���� ���� �ݱ�.
    }
    private void CharAllOf()//ĳ���� ȭ��ǥ ���� ����.
    {
        for(int i = 0; i < slotList.Count; i++)
        {
            slotList[i].combatSelection.charSelection.SetActive(false);
        }
    }
    public void OffUIWhenTurnOver()//�÷��̾��� ���� �������� ui���� ���� �Լ�
    {
        //�������̿��� �����ǿ����� index �ʱ�ȭ + selectedslot �ʱ�ȭ.
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

    IEnumerator inaction() //���� �� �ൿ�� ���ϰ� ������ �ٸ� ��ũ��Ʈ���� �浹�� �����ϱ� ���� �ڷ�ƾ.
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
