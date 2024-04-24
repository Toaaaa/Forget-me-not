using System.Collections;
using System.Collections.Generic;
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
        charSelection.SetActive(true);
        firstSelection.SetActive(false);
        skillSelection.SetActive(false);
        //itemSelection.SetActive(false);
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
    }
}
