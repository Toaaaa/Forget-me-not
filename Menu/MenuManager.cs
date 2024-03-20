using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menu;
    public GameObject menuUI;// �̰� menu selection ������Ʈ��.
    bool canCloseMenu;

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
    //������ �޴� ���� �޼����

    public void onClickSettingMenu()
    {

    }

    public void onClickWeaponMenu()
    {

    }
    private void OnEnable()
    {
        menuUI.SetActive(true);
        //���⿡ ���� character slot�� ���� on off �� ���� ����ٸ� setactive ture �� ���ٰ�.
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
    }
    
}
