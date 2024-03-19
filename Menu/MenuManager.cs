using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menu;

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
        menu[2].SetActive(true);
    }
    public void clickItemEquip()
    {
        menu[6].SetActive(true);
    }
    public void clickItemConsumable()
    {
        menu[7].SetActive(true);
    }
    public void clickItemOther()
    {
        menu[8].SetActive(true);
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
        menu[0].SetActive(true);
        //여기에 만약 character slot도 추후 on off 할 일이 생기다면 setactive ture 로 해줄것.
    }
    private void OnDisable()
    {
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].SetActive(false);
        }
    }
}
