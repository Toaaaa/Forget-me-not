using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menu;

    public void onClickStatMenu()
    {

    }

    public void onClickSkillMenu()
    {
        menu[2].SetActive(true);
    }

    public void onClickItemMenu()
    {

    }

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
