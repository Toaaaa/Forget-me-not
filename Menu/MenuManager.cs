using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menu;
    public GameObject[] selectionMenu; //0:stat,1:item, 2:skill, 3:setting.
    public GameObject menuUI;// �̰� menu selection ������Ʈ��.
    int menuselectionNum; //0:stat,1:item, 2:skill, 3:setting.
    bool canCloseMenu;
    bool IsSecondMenuOn; // �̰� true �� �Ǿ������� 1���޴��� ���������� �Ұ����ϰ� �Ұ�.
    bool IsAction; 
    //����â������ �߰����� or �������� ����/ ��� ui�� Ȱ��ȭ �Ǿ��ְų� ���� ��Ȳ���� esc�� �������� �޴�â�� ������ �ʵ��� �ϱ����� bool��. ������ ui���� menuManager�� ��ü�� �����Ͽ� �ش� bool���� �����ų��.

    public void onClickExit()
    {
        //�����ȯ���� ��� �ش� ��ư�� ������ �޴��ݱ� ��ɱ���.
    }
    public void onClickStatMenu()
    {

    }

    public void onClickSkillMenu()
    {
        menu[3].SetActive(true);
    }
    #region 
    public void onClickItemMenu()
    {
        menu[1].SetActive(true);
    }
    public void clickItemEquip()
    {
        menu[1].SetActive(true);
        menu[2].SetActive(false);
        menu[3].SetActive(false);
    }
    public void clickItemConsumable()
    {
        menu[2].SetActive(true);
        menu[1].SetActive(false);
        menu[3].SetActive(false);
    }
    public void clickItemOther()
    {
        menu[3].SetActive(true);
        menu[1].SetActive(false);
        menu[2].SetActive(false);
    }
    #endregion 
    //������ �޴� ���� �޼����

    public void onClickSettingMenu()
    {

    }

    public void onClickWeaponMenu()
    {

    }
    private void OnEnable()
    {
        //for (int i = 0; i < menu.Length; i++)
        //{
        //    menu[i].SetActive(false);
        //}
        //>> �̰Ŵ� ���߿� �޴����� ���� �ϼ��Ǹ� �ּ� �����Ұ�.

        menuselectionNum = 0;
        IsSecondMenuOn = false;
        menuUI.SetActive(true);
        //���⿡ ���� character slot�� ���� on off �� ���� ����ٸ� set_active true �� ���ٰ�.
    }
    private void OnDisable()
    {
        
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (!IsSecondMenuOn) 
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && menuselectionNum != 0)
            {
                menuselectionNum--;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && menuselectionNum == 0)
            {
                menuselectionNum = 3;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && menuselectionNum != 3)
            {
                menuselectionNum++;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && menuselectionNum == 3)
            {
                menuselectionNum = 0;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (menuselectionNum)
                {
                    case 0://stat
                        menu[0].SetActive(true);
                        IsSecondMenuOn = true;
                        break;
                    case 1://item
                        clickItemEquip(); //�����۸޴��� ���� ù��°�� equip �޴��� Ȱ��ȭ ������.
                        menu[4].SetActive(true);
                        IsSecondMenuOn = true;
                        break;
                    case 2://skill
                        menu[5].SetActive(true);
                        IsSecondMenuOn = true;
                        break;
                    case 3://setting
                        menu[6].SetActive(true);
                        IsSecondMenuOn = true;
                        break;
                    default:
                        Debug.Log("Menuselect Number Out of range");
                        break;
                }
            }
        }//1�� �޴� ���ý� �¿� Ű���弱��. ++ 1�� �޴��� ���ͷ� ���ý� 2���޴� Ȱ��ȭ.


        if (Input.GetKeyDown(KeyCode.Escape) && canCloseMenu)//menuselection�� Ȱ��ȭ�Ǿ��ִ� ����ش�.
        {
            this.gameObject.SetActive(false); 
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !canCloseMenu&&! IsAction)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].SetActive(false); //menu[] ���ִ� 2��° ������ �޴����� ���� ��Ȱ��ȭ ��Ŵ.
            }
            IsSecondMenuOn = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !canCloseMenu && IsAction)
        {
            //isAction�� true�϶��� �ƹ��� ������ ���� ����.
        }
        if (menuUI.activeSelf == true)
        {
            MenuSelectHighlight(menuselectionNum);
        }


        foreach (GameObject obj in menu)//����ٸ� ui�� Ȱ��ȭ �Ǿ����� ��� �ش� ui�� ���� ��Ȱ��ȭ �Ǿ� �־���� escape�� �������� �ٽ� ��Ȱ��ȭ�� ��ų�� ����.
        {
            if (obj.activeSelf == true)
            {
                canCloseMenu = false;
                break;
            }
            else
            {
                canCloseMenu = true;
            }
        } //���� �� ������� ������ �޴��� Ȱ��ȭ �Ǿ� ������ �ش� �޴� ������� esc�� ���� ��Ȱ��ȭ ��ų�� �ֵ��� �Ұ�.
        if(menuUI.activeSelf == false)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].SetActive(false);
            } //���� ���� ù�޴��� ��Ȱ��ȭ �����̸�, Ȥ�ø� �޴��� Ȱ��ȭ �Ǿ��ִ°��� �����ϱ� ���� ���� ��Ȱ��ȭ ��Ŵ.
        }

        
    }//update end
    void MenuSelectHighlight(int i) //�޴� ���ý� ���̶���Ʈ ȿ���� �ֱ����� �޼���. i�� menuselctionNum�� �޾ƿ�.
    {
        switch(i)
        { //�ش� rgb ������ �ٲٴ� ����� ���� ����� ���ɼ� ����.//������ �ӽ� �׽�Ʈ��. (���Ŀ� �Ƹ� Ű����� �ٲٸ� �ش��ϴ� ��ư�� �������� �ִϸ��̼� ��Ʈ�̹���ui�� ������)
            case 0:
                selectionMenu[0].GetComponent<Text>().color = new Color32(255, 0, 150, 255);
                selectionMenu[1].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[2].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[3].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                break;
            case 1:
                selectionMenu[0].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[1].GetComponent<Text>().color = new Color32(255, 0, 150, 255);
                selectionMenu[2].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[3].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                break;
            case 2:
                selectionMenu[0].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[1].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[2].GetComponent<Text>().color = new Color32(255, 0, 150, 255);
                selectionMenu[3].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                break;
            case 3:
                selectionMenu[0].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[1].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[2].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                selectionMenu[3].GetComponent<Text>().color = new Color32(255, 0, 150, 255);
                break;
            default:
                Debug.Log("Menuselect Number Out of range");
                break;
        }
    }

}
