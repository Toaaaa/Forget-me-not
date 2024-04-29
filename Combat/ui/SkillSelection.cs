using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillSelection : MonoBehaviour
{
    //여기서 스킬ui에서 스킬을 선택하고.
    //대상을 지정하여 combatManager의 selectedPlayer 또는 monsterselected 저장.

    public CombatManager combatManager;
    public List<GameObject> skillSelection;//1~4번 스킬 선택창.
    public List<GameObject> skillBehind;//스킬 선택창 뒤에 있는 이미지. (실제 선택은 아니고 표시용)
    public PlayableC player;
    public int PlayerLevel; //3번째 스킬과 4번째 스킬은 각각 5레벨 10레벨때 해금됨.
    private bool selectUp;
    public int skillIndex = 0;

    private void OnEnable()
    {
        skillSelection[0].GetComponentInChildren<TextMeshProUGUI>().text = player.skill1Name;
        skillSelection[1].GetComponentInChildren<TextMeshProUGUI>().text = player.skill2Name;

        skillBehind[0].GetComponentInChildren<TextMeshProUGUI>().text = player.skill1Name;
        skillBehind[1].GetComponentInChildren<TextMeshProUGUI>().text = player.skill2Name;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectUp = true;
            skillIndex--;
            if (skillIndex < 0)
            {
                skillIndex = skillSelection.Count - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectUp = false;
            skillIndex++;
            if (skillIndex == skillSelection.Count)
            {
                skillIndex = 0;
            }
        }
        //5레벨,10레벨에 따른 스킬 선택 가능 불가능 조절.
        if (PlayerLevel<5)
        {
            skillSelection[2].GetComponentInChildren<TextMeshProUGUI>().text = "Skill Locked";
            skillSelection[3].GetComponentInChildren<TextMeshProUGUI>().text = "Skill Locked";

            skillBehind[2].GetComponentInChildren<TextMeshProUGUI>().text = "Skill Locked";
            skillBehind[3].GetComponentInChildren<TextMeshProUGUI>().text = "Skill Locked";

            if (skillIndex == 2 || skillIndex == 3)
            {
                if(selectUp)
                {
                    skillIndex = 1;
                }
                else
                {
                    skillIndex = 0;
                }
            }
        }
        else if(PlayerLevel<10)//5레벨 이상 10레벨 미만.
        {
            skillSelection[2].GetComponentInChildren<TextMeshProUGUI>().text = player.skill3Name;
            skillSelection[3].GetComponentInChildren<TextMeshProUGUI>().text = "Skill Locked";

            skillBehind[2].GetComponentInChildren<TextMeshProUGUI>().text = player.skill3Name;
            skillBehind[3].GetComponentInChildren<TextMeshProUGUI>().text = "Skill Locked";
            if (skillIndex == 3)
            {
                if(selectUp)
                {
                    skillIndex = 2;
                }
                else
                {
                    skillIndex = 0;
                }
            }
        }
        else //10레벨 이상.
        {
            skillSelection[2].GetComponentInChildren<TextMeshProUGUI>().text = player.skill3Name;
            skillSelection[3].GetComponentInChildren<TextMeshProUGUI>().text = player.skill4Name;

            skillBehind[2].GetComponentInChildren<TextMeshProUGUI>().text = player.skill3Name;
            skillBehind[3].GetComponentInChildren<TextMeshProUGUI>().text = player.skill4Name;

            if (player.name == "Tank")//탱커의 경우 4번스킬이 없음.
            {
                if(skillIndex == 3)
                {
                    if(selectUp)
                    {
                        skillIndex = 2;
                    }
                    else
                    {
                        skillIndex = 0;
                    }
                }
            }
        }


        //선택중인 스킬 표시.
        for (int i = 0; i < skillSelection.Count; i++)
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
                if(combatManager.combatDisplay.selectingPlayer.name =="Healer")
                {
                    if(skillIndex == 0 || skillIndex == 2 || skillIndex == 3)
                    {
                        combatManager.combatDisplay.selectedSlotIndex = 0;
                        combatManager.combatDisplay.slotList[0].combatSelection.charSelection.SetActive(true);
                    }
                    combatManager.combatDisplay.skillSelectedForPlayer = true;
                }
                if(combatManager.combatDisplay.selectingPlayer.name == "Tank")
                {
                    if (skillIndex == 0)
                    {
                        combatManager.combatDisplay.skillSelectedForPlayer = true;
                    }
                    else if (skillIndex == 3)
                    {
                        Debug.Log("탱커의 4번 스킬은 없습니다.");
                    }
                    else
                    {
                        combatManager.combatDisplay.skillSelected = true;
                    }
                }
                if(combatManager.combatDisplay.selectingPlayer.name =="Warrior")
                {
                    combatManager.combatDisplay.skillSelected = true;
                }
                if(combatManager.combatDisplay.selectingPlayer.name == "Magician")
                {
                    if(skillIndex == 1)
                    {
                        combatManager.combatDisplay.skillSelectedForPlayer = true;
                    }
                    else
                    {
                        combatManager.combatDisplay.skillSelected = true;
                    }
                }
                combatManager.combatDisplay.combatSelection.skillSelection.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)&&!combatManager.combatDisplay.skillSelected)//이거 skillonselect와 겹치려나..?
        {
            if (combatManager.combatDisplay.skillSelectedForPlayer)
            {
                Debug.Log("이거");
                combatManager.combatDisplay.combatSelection.firstSelection.SetActive(false);
                combatManager.combatDisplay.combatSelection.skillSelection.SetActive(true);
            }
            else
            {
                Debug.Log("요거");
                combatManager.combatDisplay.combatSelection.firstSelection.SetActive(true);
                combatManager.combatDisplay.combatSelection.skillSelection.SetActive(false);
            }
            skillIndex = 0;
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
