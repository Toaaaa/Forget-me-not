using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public PlayableC currentCharacter;
    public Inventory inventory;
    public GameObject weaponSlot;//���̴� ���⽽�� 
    public GameObject accSlot;//���̴� �Ǽ��縮����
    public Item w_slot; //������ ������ ����� ����
    public Item a_slot; //������ ������ ����� ����

    public GameObject selectingImage;
    public bool isSelected;

    private void Update()
    {
        if(currentCharacter != null)
        {
            if (currentCharacter.equipedWeapon != null)
            {
                w_slot = currentCharacter.equipedWeapon;
                weaponSlot.GetComponent<Image>().sprite = currentCharacter.equipedWeapon.sprite;
            }
            else
            {
                w_slot = null;
                weaponSlot.GetComponent<Image>().sprite = null;
            }
                
            if (currentCharacter.equipedAcc != null)
            {
                a_slot = currentCharacter.equipedAcc;
                accSlot.GetComponent<Image>().sprite = currentCharacter.equipedAcc.sprite;
            }
            else
            {
                a_slot = null;
                accSlot.GetComponent<Image>().sprite = null;
            }
                
        }
        else
        {
            w_slot = null;
            a_slot = null;
            weaponSlot.GetComponent<Image>().sprite = null;
            accSlot.GetComponent<Image>().sprite = null;
        }


        if(currentCharacter != null)
        {
            this.GetComponentInChildren<TextMeshProUGUI>().text = currentCharacter.name;
        }
        else
        {
            this.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
        }

        if (isSelected)
            selectingImage.SetActive(true);
        else
            selectingImage.SetActive(false);


    }

    public void scaleup(int num)
    {
        if(num == 0)
        {
            weaponSlot.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            accSlot.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            weaponSlot.transform.localScale = new Vector3(1, 1, 1);
            accSlot.transform.localScale = new Vector3(1.1f, 1.1f, 1);
        }

    }
    public void scaleDown()
    {
        weaponSlot.transform.localScale = new Vector3(1, 1, 1);
        accSlot.transform.localScale = new Vector3(1, 1, 1);
    }
    public void select(int num)
    {
        if (num == 0)
        {
            EquipItem equipitem = (EquipItem)w_slot;
            if(w_slot != null)
            {
                inventory.Container[w_slot.itemID].amount++;
                equipitem.itemOptionOff(currentCharacter); //������ ������ ĳ���Ϳ��� ����� �ɼ��� �����ϴ� �Լ�.
                w_slot = null;
            }
        }
        else
        {
            EquipItem equipitem = (EquipItem)a_slot;
            if(a_slot != null)
            {
                inventory.Container[a_slot.itemID].amount++;
                equipitem.itemOptionOff(currentCharacter); //������ ������ ĳ���Ϳ��� ����� �ɼ��� �����ϴ� �Լ�.
                a_slot = null;
            }
        }
        //���õ� ��� ������� �ش� ��� ���� ����.
    }
    //�ش��ϴ� ��� �������� �ƴҰ�� �ش� ������Ʈ�� ��Ȱ��ȭ ��Ű���� �ϱ�.
}
