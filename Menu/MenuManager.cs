using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menu;

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
    //������ �޴� ���� �޼����

    public void onClickSettingMenu()
    {

    }

    public void onClickWeaponMenu()
    {

    }
    private void OnEnable()
    {
        menu[0].SetActive(true);
        //���⿡ ���� character slot�� ���� on off �� ���� ����ٸ� setactive ture �� ���ٰ�.
    }
    private void OnDisable()
    {
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].SetActive(false);
        }
    }
}
