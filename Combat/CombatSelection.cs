using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSelection : MonoBehaviour //�����ÿ� �� ĳ���͸��� ����ִ� �ൿ ���� ui ��Ʈ�ѷ�
{
    public CombatManager combatManager;

    public GameObject charSelection; //ó�� �������� ĳ���� ����.
    public GameObject firstSelection; //1.���� , 2.��ų , 3.������ , 4.����
    public GameObject skillSelection; //��ų����â
    public GameObject itemSelection; //�����ۼ���â
    public GameObject fleeSelectUI; //���� ����â

    public List<GameObject> selectionList; //���� ������� ����Ʈ�� �־��ٰ�.
    private int indexForList; //���� ���õ� �ε���.

    public int inSelectIndex; //�� selection���ο��� ����� �ε���.

    private void OnEnable()
    {
        charSelection.SetActive(true);
        firstSelection.SetActive(false);
        skillSelection.SetActive(false);
        itemSelection.SetActive(false);
        fleeSelectUI.SetActive(false);
        indexForList = 0;
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (charSelection.activeSelf)
            {
                charSelection.SetActive(false);
                firstSelection.SetActive(true);
            }

            
        }
    }
}
