using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectingSystem : MonoBehaviour //usegivenfunc을 사용해서 선택지를 선택할 수 있게 하는 스크립트.//아직 실사용은 안함.
{
    public GameObject[] selection;
    public int selectionNum;
    public int totalSelectionNum; //선택지의 총 개수. 1부터 시작.
    public bool gotoSlot; //선택지에서 

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
                selection[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255); //해당하는 선택지를 빨간색으로 표시.
            }
            else
            {
                selection[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
