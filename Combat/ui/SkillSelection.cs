using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelection : MonoBehaviour
{
    //여기서 스킬ui에서 스킬을 선택하고.
    //대상을 지정하여 combatManager의 selectedPlayer 또는 monsterselected 저장.

    public CombatManager combatManager;
    public List<GameObject> skillSelection;//1~4번 스킬 선택창.

    public int skillIndex = 0;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            skillIndex--;
            if (skillIndex < 0)
            {
                skillIndex = skillSelection.Count - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            skillIndex++;
            if (skillIndex == skillSelection.Count)
            {
                skillIndex = 0;
            }
        }
        for (int i = 0; i < skillSelection.Count; i++)//선택중인 스킬 표시.
        {
            if (i == skillIndex)
            {
                skillSelection[i].SetActive(true);
            }
            else
            {
                skillSelection[i].SetActive(false);
            }
        }


        if (!combatManager.combatDisplay.skillSelected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                combatManager.combatDisplay.skillSelected = true;
                combatManager.combatDisplay.combatSelection.skillSelection.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)&&!combatManager.combatDisplay.skillSelected)//이거 skillonselect와 겹치려나..?
        {
            combatManager.combatDisplay.combatSelection.firstSelection.SetActive(true);
        }
    }

    /*
    public void SkillOnSelect() //스킬을 사용할 대상 선택.
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
        if (Input.GetKeyDown(KeyCode.Space))// 선택된 몬스터 공격
        {
            combatManager.combatDisplay.combatManager.monsterSelected = combatManager.combatDisplay.MobList[combatManager.combatDisplay.selectedMobIndex].GetComponent<TestMob>().gameObject;
            combatManager.combatDisplay.inAction = true;
            switch (skillIndex)
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
                case 3://탱커의 경우 4번스킬이 없고, 3번,4번 스킬은 레벨이 오름에 따라서 해제되는 방식.
                    combatManager.combatDisplay.selectingPlayer.Skill4();
                    break;
            }
            StartCoroutine(inaction());
            combatManager.combatDisplay.combatManager.isFirstSelection = false;
            combatManager.combatDisplay.skillSelected = false;
            combatManager.combatDisplay.combatManager.monsterSelected = null;
            combatManager.combatDisplay.selectedMobIndex = 0;
            Debug.Log("공격을 하였음.");
            //여기서 이제 시간이 남았을 경우 다음 플레이어의 동작 메뉴 오픈.
            if (combatManager.combatDisplay.isPlayerTurn)
            {
                combatManager.combatDisplay.selectedSlotIndex = 0;
                combatManager.combatDisplay.combatSelection = combatManager.combatDisplay.slotList[combatManager.combatDisplay.selectedSlotIndex].combatSelection;
                combatManager.combatDisplay.combatSelection.charSelection.SetActive(true);

            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            combatManager.combatDisplay.skillSelected = false;
            combatManager.combatDisplay.combatSelection.skillSelection.SetActive(true);
        }
    }
    IEnumerator inaction()
    {
        yield return new WaitForSeconds(0.4f);
        combatManager.combatDisplay.inAction = false;
    }*///해당 부분 combatdisplay로 이동.
}
