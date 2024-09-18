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
    public GameObject menuUI;// 이거 menu selection 오브젝트임.
    int menuselectionNum; //0:stat,1:item, 2:skill, 3:setting.
    bool canCloseMenu;
    bool IsSecondMenuOn; // 이게 true 로 되어있으면 1차메뉴의 종류선택이 불가능하게 할것.
    bool IsAction;
    public bool isItemUsing;//아이템을 사용하려고 하는 도중일때
    //slot
    bool slotOn;//플레이어의 슬롯창을 조작 중일때
    int slotTotalNum;
    int slotNum;//현재 선택된 슬롯의 번호.
    bool inSlotItemSelecting;//슬롯창에서 아이템을 선택하려고 하는 중일때
    int slotItemNum;//현재 선택된 슬롯의 아이템 번호. 0>> 무기슬롯 1>> 악세슬롯.
    
    //스텟창에서의 추가정보 or 아이템의 장착/ 사용 ui가 활성화 되어있거나 등의 상황에서 esc를 눌렀을때 메뉴창이 닫히지 않도록 하기위한 bool값. 각각의 ui에서 menuManager의 객체를 참조하여 해당 bool값을 변경시킬것.

    public void onClickExit()
    {
        //모바일환경의 경우 해당 버튼을 눌러서 메뉴닫기 기능구현.
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
    //아이템 메뉴 관련 메서드들

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
        //>> 이거는 나중에 메뉴쪽이 전부 완성되면 주석 해제할것.

        menuselectionNum = 0;
        IsSecondMenuOn = false;
        menuUI.SetActive(true);
        //여기에 만약 character slot도 추후 on off 할 일이 생기다면 set_active true 로 해줄것.
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
                slotOn = true; //플레이어 슬롯메뉴에 접근.
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
                        clickItemEquip(); //아이템메뉴의 제일 첫번째인 equip 메뉴를 활성화 시켜줌.
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
        }//1차 메뉴 선택시 좌우 키보드선택. ++ 1차 메뉴를 엔터로 선택시 2차메뉴 활성화.
        else if (IsSecondMenuOn && !slotOn)
        {
            switch (menuselectionNum)
            {
                case 0://stat
                    break;
                case 1://item
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && !isItemUsing) //아이템 사용하려고 하는 중이 아닐때 좌우키를 누르면 아이템메뉴의 2차메뉴가 변경됨.
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
                        *///기타창(clickItemOther)을 사용할때의 코드. 기타창을 안쓸 예정이라 수정.
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
                        *///기타창(clickItemOther)을 사용할때의 코드. 기타창을 안쓸 예정이라 수정.
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
        }//2차 메뉴가 켜져있을때의 동작들.
        else if (!IsSecondMenuOn && slotOn)
        {
            if (inSlotItemSelecting)//슬롯창내부에서 아이템을 선택 중인경우.
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    slotItemNum = 0;
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    slotItemNum = 1;
                if (Input.GetKeyDown(KeyCode.Space))
                    slots[slotNum].select(slotItemNum); //선택된 무기 or 악세의 슬롯에 아이템이 있을경우 장착해제.
                slots[slotNum].scaleup(slotItemNum);
            }
            else //슬롯중에서 선택중인 경우.
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
            }//화살표 표시.
            if (Input.GetKeyDown(KeyCode.Space) && !inSlotItemSelecting)
            {
                inSlotItemSelecting = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                slotOn = false; //플레이어 슬롯메뉴에 접근.
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


        if (Input.GetKeyDown(KeyCode.Escape) && canCloseMenu)//menuselection만 활성화되어있는 경우해당.
        {
            this.gameObject.SetActive(false); 
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !canCloseMenu&&! IsAction)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].SetActive(false); //menu[] 에있는 2번째 뎁스의 메뉴들을 전부 비활성화 시킴.
            }
            IsSecondMenuOn = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !canCloseMenu && IsAction)
        {
            //isAction이 true일때는 아무런 동작을 하지 않음.
        }
        if (menuUI.activeSelf == true)
        {
            MenuSelectHighlight(menuselectionNum);
        }


        foreach (GameObject obj in menu)//만약다른 ui가 활성화 되어있을 경우 해당 ui가 전부 비활성화 되어 있어야지 escape를 눌렀을때 다시 비활성화를 시킬수 있음.
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
        } //추후 이 방식으로 상위의 메뉴가 활성화 되어 있으면 해당 메뉴 순서대로 esc를 동해 비활성화 시킬수 있도록 할것.
        if(menuUI.activeSelf == false)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].SetActive(false);
            } //만약 제일 첫메뉴가 비활성화 상태이면, 혹시모를 메뉴가 활성화 되어있는것을 방지하기 위해 전부 비활성화 시킴.
        }

        
    }//update end
    void MenuSelectHighlight(int i) //메뉴 선택시 하이라이트 효과를 주기위한 메서드. i는 menuselctionNum을 받아옴.
    {
        switch(i)
        { //해당 rgb 값으로 바꾸는 방식은 추후 변경될 가능성 있음.//지금은 임시 테스트용. (추후엔 아마 키보드로 바꾸면 해당하는 버튼이 눌러지는 애니메이션 도트이미지ui를 넣을것)
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
