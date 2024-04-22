using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSelection : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatSelection combatSelection; //여기에 player의 정보가 있음.
    public List<GameObject> selection; // 1.공격 2.스킬 3.아이템 4.도망


    private int selectionIndex = 0;

    int playerAverageSpeed;
    int fastestMonsterSpeed;


    private void Update()
    {
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
            for (int i = 0; i < selection.Count; i++)
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
                    combatManager.combatDisplay.selectingPlayer = combatSelection.player;
                    combatManager.combatDisplay.combatSelection = combatSelection;
                    combatManager.combatDisplay.attackSelected = true;                    
                    this.gameObject.SetActive(false);
                        break;
                    case 1:
                        Debug.Log("스킬");
                        break;
                    case 2:
                        Debug.Log("아이템");
                        break;
                    case 3:
                        WhenFlee();
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                combatSelection.charSelection.SetActive(true);
                combatSelection.firstSelection.SetActive(false);
                combatManager.isFirstSelection = false;
            }      
        
    }
    private void WhenFlee() //fleeSelection의 도망가기 버튼을 눌럿을때 실행되는 함수.
    {
        playerAverageSpeed = 0;
        fastestMonsterSpeed = 0;

        for (int i = 0; i < combatManager.playerList.Count; i++)
        {
            playerAverageSpeed += combatManager.playerList[i].spd;
        }
        playerAverageSpeed = playerAverageSpeed / combatManager.playerList.Count;
        Debug.Log(playerAverageSpeed);

        for (int i = 0; i < combatManager.monsterList.Count; i++)
        {
            fastestMonsterSpeed = Mathf.Max(fastestMonsterSpeed, combatManager.monsterList[i].Speed);
        }

        if (playerAverageSpeed > fastestMonsterSpeed)
        {
            Debug.Log("도망에 성공했습니다.");
            combatManager.OnCombatEnd();
        }
        else
        {
            Debug.Log("도망에 실패했습니다.");
        }
    }

}
