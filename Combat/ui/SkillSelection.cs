using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelection : MonoBehaviour
{
    //���⼭ ��ųui���� ��ų�� �����ϰ�.
    //����� �����Ͽ� combatManager�� selectedPlayer �Ǵ� monsterselected ����.

    public CombatManager combatManager;
    public List<GameObject> skillSelection;//1~4�� ��ų ����â.

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
        for (int i = 0; i < skillSelection.Count; i++)//�������� ��ų ǥ��.
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

        if (Input.GetKeyDown(KeyCode.Escape)&&!combatManager.combatDisplay.skillSelected)//�̰� skillonselect�� ��ġ����..?
        {
            combatManager.combatDisplay.combatSelection.firstSelection.SetActive(true);
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
