using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFirstselection : MonoBehaviour
{
    int playerAverageSpeed;
    int fastestMonsterSpeed;

    public CombatManager combatManager;
    //1.공격 , 2.스킬 , 3.아이템 , 4.도망
    public GameObject attackSelection;
    public GameObject skillSelection;
    public GameObject itemSelection;
    public GameObject fleeSelection;


    private void WhenFlee() //fleeSelection의 도망가기 버튼을 눌럿을때 실행되는 함수.
    {
        playerAverageSpeed = 0;
        fastestMonsterSpeed = 0;

        for (int i = 0; i < combatManager.playerList.Count; i++)
        {
            playerAverageSpeed += combatManager.playerList[i].spd;
        }
        playerAverageSpeed = playerAverageSpeed / combatManager.playerList.Count;

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
