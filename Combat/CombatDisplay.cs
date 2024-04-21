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
    public List<GameObject> MobList; //���� ���Կ��� ���Ͱ� �ִ°��� ã�Ƽ� ���� 

    public CombatSlot selectedSlot; //���õ� ����. ��ų ���� or ������ ���� �� ������ ������� ��.

    private int selectedSlotIndex; //���õ� ������ �ε���.
    private int selectedMobIndex; //���õ� ������ �ε���.
    public bool duringSceneChange; //���� ���� ���� �ִϸ��̼��� ��� ���� ���.
    public float MyturnTime; //�÷��̾��� �� �ð�.
    public float EnemyturnTime; //���� �� �ð�.
    public bool isPlayerTurn; //�÷��̾��� ������ �ƴ��� �Ǻ��ϴ� ����. //�÷��̾��� ���� �ٽ�� ���� ���� ��� false�� ��.

    public PlayableC selectingPlayer;//����,������,��ų ���� ������ �÷��̾�.
    public CombatSelection combatSelection;//���� �÷��̾��� selection�� ����ϴ� ��.

    public bool attackSelected; //������ ���õǾ����� �Ǻ��ϴ� ����.firstSelection���� �⺻���� ���ý�.

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            combatManager.monsterSelected = MobList[selectedMobIndex].GetComponent<TestMob>().gameObject;
            selectingPlayer.Attack();
            attackSelected = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            attackSelected = false;
            combatSelection.firstSelection.SetActive(true);
        }

    }
}
