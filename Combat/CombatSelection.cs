using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSelection : MonoBehaviour //전투시에 각 캐릭터마다 들어있는 행동 선택 ui 컨트롤러
{
    public CombatManager combatManager;
    public PlayableC player;

    public GameObject charSelection; //처음 조작을할 캐릭터 선택.
    public GameObject firstSelection; //1.공격 , 2.스킬 , 3.아이템 , 4.도망
    public GameObject skillSelection; //스킬선택창
    public GameObject itemSelection; //아이템선택창


    private void OnEnable()
    {
        charSelection.SetActive(true);
        firstSelection.SetActive(false);
        skillSelection.SetActive(false);
        //itemSelection.SetActive(false);
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space)&&!combatManager.combatDisplay.attackSelected)
        {
            if (charSelection.activeSelf&&!combatManager.combatDisplay.inAction)//액션중이 아닐때.
            {
                charSelection.SetActive(false);
                firstSelection.SetActive(true);
                combatManager.isFirstSelection = true;
            }

            
        }
    }
}
