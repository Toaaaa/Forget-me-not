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

    public List<GameObject> selectionList; //위의 순서대로 리스트에 넣어줄것.
    private int indexForList; //현재 선택된 인덱스.

    public int inSelectIndex; //각 selection내부에서 사용할 인덱스.

    private void OnEnable()
    {
        charSelection.SetActive(true);
        firstSelection.SetActive(false);
        skillSelection.SetActive(false);
        //itemSelection.SetActive(false);
        indexForList = 0;
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space)&&!combatManager.combatDisplay.attackSelected)
        {
            if (charSelection.activeSelf)
            {
                charSelection.SetActive(false);
                firstSelection.SetActive(true);
                combatManager.isFirstSelection = true;
            }

            
        }
    }
}
