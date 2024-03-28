using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public PlayableC currentCharacter;
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

    //�ش��ϴ� ��� �������� �ƴҰ�� �ش� ������Ʈ�� ��Ȱ��ȭ ��Ű���� �ϱ�.
}
