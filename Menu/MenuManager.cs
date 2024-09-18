using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public PlayableManager playablemanager;
    public List<SlotManager> slots;
    public GameObject[] menu;
    public GameObject[] selectionMenu; //0:stat,1:item, 2:skill, 3:setting.
    public GameObject menuUI;// �̰� menu selection ������Ʈ��.
    int menuselectionNum; //0:stat,1:item, 2:skill, 3:setting.
    bool canCloseMenu;
    bool IsSecondMenuOn; // �̰� true �� �Ǿ������� 1���޴��� ���������� �Ұ����ϰ� �Ұ�.
    bool IsAction;
    public bool isItemUsing;//�������� ����Ϸ��� �ϴ� �����϶�
    //slot
    bool slotOn;//�÷��̾��� ����â�� ���� ���϶�
    int slotTotalNum;
    int slotNum;//���� ���õ� ������ ��ȣ.
    bool inSlotItemSelecting;//����â���� �������� �����Ϸ��� �ϴ� ���϶�
    int slotItemNum;//���� ���õ� ������ ������ ��ȣ. 0>> ���⽽�� 1>> �Ǽ�����.
    
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
        slotOn = false;
        slotNum = 0;
        slotItemNum = 0;
        inSlotItemSelecting = false;
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].SetActive(false);
        }
    }

    private void Update()
    {
        slotTotalNum = playablemanager.joinedPlayer.Count;
        if (menu[4].activeSelf)
            IsSecondMenuOn = true;

        if (!IsSecondMenuOn && !slotOn)
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
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                slotOn = true; //�÷��̾� ���Ը޴��� ����.
            }

            if (Input.GetKeyDown(KeyCode.Space))
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
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].isSelected = false;
                slots[i].scaleDown();
            }
        }//1�� �޴� ���ý� �¿� Ű���弱��. ++ 1�� �޴��� ���ͷ� ���ý� 2���޴� Ȱ��ȭ.
        else if (IsSecondMenuOn && !slotOn)
        {
            switch (menuselectionNum)
            {
                case 0://stat
                    break;
                case 1://item
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && !isItemUsing) //������ ����Ϸ��� �ϴ� ���� �ƴҶ� �¿�Ű�� ������ �����۸޴��� 2���޴��� �����.
                    {
                        /*
                        if (menu[1].activeSelf == true)
                        {
                            clickItemOther();
                        }
                        else if (menu[2].activeSelf == true)
                        {
                            clickItemEquip();
                        }
                        else if (menu[3].activeSelf == true)
                        {
                            clickItemConsumable();
                        }
                        *///��Ÿâ(clickItemOther)�� ����Ҷ��� �ڵ�. ��Ÿâ�� �Ⱦ� �����̶� ����.
                        if (menu[1].activeSelf == true)
                        {
                            clickItemEquip();
                        }
                        else if (menu[2].activeSelf == true)
                        {
                            clickItemConsumable();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && !isItemUsing)
                    {
                        /*
                        if (menu[1].activeSelf == true)
                        {
                            clickItemConsumable();
                        }
                        else if (menu[2].activeSelf == true)
                        {
                            clickItemOther();
                        }
                        else if (menu[3].activeSelf == true)
                        {
                            clickItemEquip();
                        }
                        *///��Ÿâ(clickItemOther)�� ����Ҷ��� �ڵ�. ��Ÿâ�� �Ⱦ� �����̶� ����.
                        if (menu[1].activeSelf == true)
                        {
                            clickItemConsumable();
                        }
                        else if (menu[2].activeSelf == true)
                        {
                            clickItemEquip();
                        }
                    }
                    break;
                case 2://skill
                    break;
                case 3://setting
                    break;
                default:
                    Debug.Log("Menuselect Number Out of range");
                    break;
            }
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].scaleDown();
            }
        }//2�� �޴��� ������������ ���۵�.
        else if (!IsSecondMenuOn && slotOn)
        {
            if (inSlotItemSelecting)//����â���ο��� �������� ���� ���ΰ��.
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    slotItemNum = 0;
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    slotItemNum = 1;
                if (Input.GetKeyDown(KeyCode.Space))
                    slots[slotNum].select(slotItemNum); //���õ� ���� or �Ǽ��� ���Կ� �������� ������� ��������.
                slots[slotNum].scaleup(slotItemNum);
            }
            else //�����߿��� �������� ���.
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && slotNum != 0)
                {
                    slotNum--;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && slotNum == 0)
                {
                    slotNum = slotTotalNum - 1;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && slotNum != slotTotalNum - 1)
                {
                    slotNum++;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && slotNum == slotTotalNum - 1)
                {
                    slotNum = 0;
                }
            }

            for (int i = 0; i < slotTotalNum; i++)
            {
                if (i == slotNum)
                {
                    slots[i].isSelected = true;
                }
                else
                {
                    slots[i].isSelected = false;
                }
            }//ȭ��ǥ ǥ��.
            if (Input.GetKeyDown(KeyCode.Space) && !inSlotItemSelecting)
            {
                inSlotItemSelecting = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                slotOn = false; //�÷��̾� ���Ը޴��� ����.
                inSlotItemSelecting = false;
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                slotOn = false;
                slotItemNum = 0;
                inSlotItemSelecting = false;
            }

        }
        else 
        { 
            for(int i=0; i<slots.Count; i++)
            {
                slots[i].scaleDown();
            }
        }


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
