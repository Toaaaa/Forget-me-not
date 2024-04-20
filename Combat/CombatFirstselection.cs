using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFirstselection : MonoBehaviour
{
    int playerAverageSpeed;
    int fastestMonsterSpeed;

    public CombatManager combatManager;
    //1.���� , 2.��ų , 3.������ , 4.����
    public GameObject attackSelection;
    public GameObject skillSelection;
    public GameObject itemSelection;
    public GameObject fleeSelection;


    private void WhenFlee() //fleeSelection�� �������� ��ư�� �������� ����Ǵ� �Լ�.
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
            Debug.Log("������ �����߽��ϴ�.");
            combatManager.OnCombatEnd();
        }
        else
        {
            Debug.Log("������ �����߽��ϴ�.");
        }
    }

}
