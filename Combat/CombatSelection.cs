using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CombatSelection : MonoBehaviour //�����ÿ� �� ĳ���͸��� ����ִ� �ൿ ���� ui ��Ʈ�ѷ�
{
    public CombatManager combatManager;
    public PlayableC player;

    public GameObject charSelection; //ó�� �������� ĳ���� ����.
    public GameObject firstSelection; //1.���� , 2.��ų , 3.������ , 4.����
    public GameObject skillSelection; //��ų����â
    public GameObject itemSelection; //�����ۼ���â


    private void OnEnable()
    {
        if(combatManager.combatDisplay.skillForAllPlayer)//healer�� ��ų�� ������ SkillOnSelectedPlayer�� ��������.
        {
            if(combatManager.combatDisplay.selectingPlayer ==player)
                skillSelection.SetActive(true);
        }
        else
        {
            skillSelection.SetActive(false);
        }
        if (!combatManager.combatDisplay.noCharObj)//healer�� ��ų�� ������ SkillOnSelectedPlayer�� ��������.
        {
            charSelection.SetActive(true);
        }
        firstSelection.SetActive(false);
        //itemSelection.SetActive(false);
        skillSelection.GetComponent<SkillSelection>().player = player;
        if(player !=null)
            skillSelection.GetComponent<SkillSelection>().PlayerLevel = player.level;
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space)&&!combatManager.combatDisplay.attackSelected)
        {
            if (charSelection.activeSelf&&!combatManager.combatDisplay.inAction)//�׼����� �ƴҶ�.
            {
                charSelection.SetActive(false);
                firstSelection.SetActive(true);
                combatManager.isFirstSelection = true;
            }
           
        }
        if (combatManager.combatDisplay.skillForAllPlayer)
        {
            charSelection.SetActive(true);
        }


    }
}
