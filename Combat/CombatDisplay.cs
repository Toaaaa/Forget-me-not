using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    public CombatManager combatManager;
    public List<PlayableC> playerList;
    public List<CombatSlot> slotList; //�÷��̾��� �ִϸ��̼� ���� ��µǴ� ��.
    public List<CombatStatus> statusUI;
    public List<MobSlot> mobSlotList; //���Ը� �����
    public List<GameObject> MobList; //���� ���Կ��� ���Ͱ� �ִ°��� ã�Ƽ� ���� (����� ���� ������ ���� ������Ʈ ����Ʈ)

    public CombatSlot selectedSlot; //���õ� ����. ��ų ���� or ������ ���� �� ������ ������� ��.

    public int selectedSlotIndex; //���õ� ������ �ε���.
    public int selectedMobIndex; //���õ� ������ �ε���.
    public bool duringSceneChange; //���� ���� ���� �ִϸ��̼��� ��� ���� ���.
    public float MyturnTime; //�÷��̾��� �� �ð�.
    public float EnemyturnTime; //���� �� �ð�.
    public bool isPlayerTurn; //�÷��̾��� ������ �ƴ��� �Ǻ��ϴ� ����. //�÷��̾��� ���� �ٽ�� ���� ���� ��� false�� ��.

    public PlayableC selectingPlayer;//����,������,��ų ���� ������ �÷��̾�.
    public CombatSelection combatSelection;//���� �÷��̾��� selection�� ����ϴ� ��.

    public bool attackSelected; //������ ���õǾ����� �Ǻ��ϴ� ����.firstSelection���� �⺻���� ���ý�.
    public bool skillSelected; //��ų�� ���õǾ����� �Ǻ��ϴ� ����.firstSelection���� ��ų ���ý�.
    public bool itemSelected; //�������� ���õǾ����� �Ǻ��ϴ� ����.firstSelection���� ������ ���ý�.

    public bool inAction;//�÷��̾ �ൿ ���ΰ��. ���ÿ� ���� �ൿ�� ��ġ�� �ʰԸ��� ����.

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
    }//�÷��̾��� ���϶� �÷��̾� ������ ������ �� �ִ�.
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
    }//�÷��̾��� ���� �������� Ȯ���ϴ� �Լ�.

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
            Debug.Log("������ �Ͽ���.");
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


    IEnumerator inaction() //���� �� �ൿ�� ���ϰ� ������ �ٸ� ��ũ��Ʈ���� �浹�� �����ϱ� ���� �ڷ�ƾ.
    {
        yield return new WaitForSeconds(0.4f);
        inAction = false;
    }
}
