using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menu;
    public GameObject menuUI;// 이거 menu selection 오브젝트임.
    bool canCloseMenu;

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
        menu[5].SetActive(true);
    }
    public void clickItemConsumable()
    {
        menu[6].SetActive(true);
    }
    public void clickItemOther()
    {
        menu[7].SetActive(true);
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
        menuUI.SetActive(true);
        //여기에 만약 character slot도 추후 on off 할 일이 생기다면 setactive ture 로 해줄것.
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
        if (Input.GetKeyDown(KeyCode.Escape) && canCloseMenu)
        {
            this.gameObject.SetActive(false);
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
    }
    
}
