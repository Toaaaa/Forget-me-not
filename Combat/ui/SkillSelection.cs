using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillSelection : MonoBehaviour
{
    //���⼭ ��ųui���� ��ų�� �����ϰ�.
    //����� �����Ͽ� combatManager�� selectedPlayer �Ǵ� monsterselected ����.

    public CombatManager combatManager;
    public List<GameObject> skillSelection;//1~4�� ��ų ����â.
    public List<GameObject> skillBehind;//��ų ����â �ڿ� �ִ� �̹���. (���� ������ �ƴϰ� ǥ�ÿ�)
    public PlayableC player;
    public int PlayerLevel; //3��° ��ų�� 4��° ��ų�� ���� 5���� 10������ �رݵ�.
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
        //5����,10������ ���� ��ų ���� ���� �Ұ��� ����.
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
        else if(PlayerLevel<10)//5���� �̻� 10���� �̸�.
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
        else //10���� �̻�.
        {
            skillSelection[2].GetComponentInChildren<TextMeshProUGUI>().text = player.skill3Name;
            skillSelection[3].GetComponentInChildren<TextMeshProUGUI>().text = player.skill4Name;

            skillBehind[2].GetComponentInChildren<TextMeshProUGUI>().text = player.skill3Name;
            skillBehind[3].GetComponentInChildren<TextMeshProUGUI>().text = player.skill4Name;

            if (player.name == "Tank")//��Ŀ�� ��� 4����ų�� ����.
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


        //�������� ��ų ǥ��.
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
                        Debug.Log("��Ŀ�� 4�� ��ų�� �����ϴ�.");
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

        if (Input.GetKeyDown(KeyCode.Escape)&&!combatManager.combatDisplay.skillSelected)//�̰� skillonselect�� ��ġ����..?
        {
            if (combatManager.combatDisplay.skillSelectedForPlayer)
            {
                Debug.Log("�̰�");
                combatManager.combatDisplay.combatSelection.firstSelection.SetActive(false);
                combatManager.combatDisplay.combatSelection.skillSelection.SetActive(true);
            }
            else
            {
                Debug.Log("���");
                combatManager.combatDisplay.combatSelection.firstSelection.SetActive(true);
                combatManager.combatDisplay.combatSelection.skillSelection.SetActive(false);
            }
            skillIndex = 0;
        }
    }

    /*
    public void SkillOnSelect() //��ų�� ����� ��� ����.
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
        if (Input.GetKeyDown(KeyCode.Space))// ���õ� ���� ����
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
                case 3://��Ŀ�� ��� 4����ų�� ����, 3��,4�� ��ų�� ������ ������ ���� �����Ǵ� ���.
                    combatManager.combatDisplay.selectingPlayer.Skill4();
                    break;
            }
            StartCoroutine(inaction());
            combatManager.combatDisplay.combatManager.isFirstSelection = false;
            combatManager.combatDisplay.skillSelected = false;
            combatManager.combatDisplay.combatManager.monsterSelected = null;
            combatManager.combatDisplay.selectedMobIndex = 0;
            Debug.Log("������ �Ͽ���.");
            //���⼭ ���� �ð��� ������ ��� ���� �÷��̾��� ���� �޴� ����.
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
    }*///�ش� �κ� combatdisplay�� �̵�.
}
