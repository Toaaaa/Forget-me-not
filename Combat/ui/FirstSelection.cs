using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstSelection : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatSelection combatSelection; //���⿡ player�� ������ ����.
    public List<GameObject> selection; // 1.���� 2.��ų 3.������ 4.����
    public List<GameObject> selectionBehind; //����â �ڿ� �ִ� �̹�����. (���� ������ �ƴϰ� ǥ�ÿ�)

    public GameObject selectCostText;

    public int selectionIndex = 0;

    float playerAverageSpeed;
    float fastestMonsterSpeed;


    private void Update()
    {
        switch (selectionIndex)
        {
            case 0:
                selectCostText.GetComponentInChildren<TextMeshProUGUI>().text = combatManager.attackCostTime.ToString();
                break;
            case 1:
                selectCostText.GetComponentInChildren<TextMeshProUGUI>().text = "";
                break;
            case 2:
                selectCostText.GetComponentInChildren<TextMeshProUGUI>().text = combatManager.itemCostTime.ToString();
                break;
            case 3:
                selectCostText.GetComponentInChildren<TextMeshProUGUI>().text = combatManager.fleeCostTime.ToString();
                break;
        }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectionIndex--;
                if (selectionIndex < 0)
                {
                    selectionIndex = selection.Count - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectionIndex++;
                if (selectionIndex == selection.Count)
                {
                    selectionIndex = 0;
                }
            }
            for (int i = 0; i < selection.Count; i++)//�������� ��ų ǥ��.
        {
                if (i == selectionIndex)
                {
                    selection[i].SetActive(true);
                }
                else
                {
                    selection[i].SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (selectionIndex)
                {
                    case 0:
                    combatManager.combatDisplay.selectingPlayer = combatSelection.player;//�ൿ�� ������ (����, ��ų���� ������) �÷��̾� ����.
                    combatManager.combatDisplay.combatSelection = combatSelection;//���� selection���� selection������Ʈ ����.
                    combatManager.combatDisplay.attackSelected = true;                    
                    this.gameObject.SetActive(false);
                        break;
                    case 1:
                    combatManager.combatDisplay.selectingPlayer = combatSelection.player;
                    combatManager.combatDisplay.combatSelection = combatSelection;
                    combatSelection.skillSelection.SetActive(true);
                    this.gameObject.SetActive(false);
                        break;
                    case 2:
                    combatManager.combatDisplay.selectingPlayer = combatSelection.player;
                    combatManager.combatDisplay.combatSelection = combatSelection;
                    combatSelection.itemSelection.SetActive(true);
                    this.gameObject.SetActive(false);
                        break;
                    case 3:
                        WhenFlee();
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                selectionIndex = 0;
                combatSelection.charSelection.SetActive(true);
                combatSelection.firstSelection.SetActive(false);
                combatManager.isFirstSelection = false;
            }      
        
    }
    private void WhenFlee() //fleeSelection�� �������� ��ư�� �������� ����Ǵ� �Լ�.
    {
        playerAverageSpeed = 0;
        fastestMonsterSpeed = 0;
        if(combatManager.playerTurnTime >= combatManager.fleeCostTime &&!combatManager.isBoss)
        {
            for (int i = 0; i < combatManager.playerList.Count; i++)
            {
                playerAverageSpeed += combatManager.playerList[i].spd;
            }
            playerAverageSpeed = playerAverageSpeed / combatManager.playerList.Count;
            for (int i = 0; i < combatManager.monsterObject.Count; i++)
            {
                fastestMonsterSpeed = Mathf.Max(fastestMonsterSpeed, combatManager.monsterObject[i].GetComponent<TestMob>().Speed);
            }

            if (playerAverageSpeed > fastestMonsterSpeed)
            {
                combatManager.turnTimeUsedShow.PrintUsedTime(combatManager.fleeCostTime);
                Debug.Log("������ �����߽��ϴ�.");
                combatManager.DeadMobExpCount = 0;
                combatManager.DeadMobGoldCount = 0;
                combatManager.DeadMobItemDrop.Clear();
                combatManager.selectedFlee = true;
                combatManager.OnCombatEnd();
            }
            else
            {
                combatManager.playerTurnTime -= combatManager.fleeCostTime;
                combatManager.monsterAttackManager.playerTurnUsed += combatManager.fleeCostTime;
                Debug.Log("������ �����߽��ϴ�.");
            }
        }
        else if(!combatManager.isBoss)
        {
            combatManager.TimerShake();//�Ͻð��� �����Ҷ� Ÿ�̸� ���� ȿ��.
        }
        else//���� ������
        {
            return;
        }
    }

}
