using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectingSystem : MonoBehaviour //usegivenfunc�� ����ؼ� �������� ������ �� �ְ� �ϴ� ��ũ��Ʈ.//���� �ǻ���� ����.
{
    public GameObject[] selection;
    public int selectionNum;
    public int totalSelectionNum; //�������� �� ����. 1���� ����.
    public bool gotoSlot; //���������� 

    private void OnEnable()
    {
        FindObjectOfType<DisplayInventory>().uISelectingSystem = this;
    }

    private void Update()
    {
        totalSelectionNum = selection.Length;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(selectionNum > 0)
            {
                selectionNum--;
            }
            else if(selectionNum == 0)
            {
                selectionNum = totalSelectionNum - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(selectionNum < totalSelectionNum - 1)
            {
                selectionNum++;
            }
            else if(selectionNum == totalSelectionNum - 1)
            {
                selectionNum = 0;
            }
        }
        highlightSelection(selectionNum);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            selection[selectionNum].GetComponent<UseGivenFunc>().UseFunc();
        }
    }


    void highlightSelection(int number)
    {
        for (int i = 0; i < selection.Length; i++)
        {
            if (i == number)
            {
                selection[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255); //�ش��ϴ� �������� ���������� ǥ��.
            }
            else
            {
                selection[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
