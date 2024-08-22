using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstSelection : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatSelection combatSelection; //여기에 player의 정보가 있음.
    public List<GameObject> selection; // 1.공격 2.스킬 3.아이템 4.도망
    public List<GameObject> selectionBehind; //선택창 뒤에 있는 이미지들. (실제 선택은 아니고 표시용)

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
            for (int i = 0; i < selection.Count; i++)//선택중인 스킬 표시.
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
                    combatManager.combatDisplay.selectingPlayer = combatSelection.player;//행동을 선택할 (공격, 스킬들을 실행할) 플레이어 저장.
                    combatManager.combatDisplay.combatSelection = combatSelection;//현재 selection중인 selection오브젝트 저장.
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
    private void WhenFlee() //fleeSelection의 도망가기 버튼을 눌럿을때 실행되는 함수.
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
                Debug.Log("도망에 성공했습니다.");
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
                Debug.Log("도망에 실패했습니다.");
            }
        }
        else if(!combatManager.isBoss)
        {
            combatManager.TimerShake();//턴시간이 부족할때 타이머 흔드는 효과.
        }
        else//보스 전투시
        {
            return;
        }
    }

}
