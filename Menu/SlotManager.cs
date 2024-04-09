using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public PlayableC currentCharacter;
    public Inventory inventory;
    public GameObject weaponSlot;//보이는 무기슬롯 
    public GameObject accSlot;//보이는 악세사리슬롯
    public Item w_slot; //아이템 정보를 담아줄 슬롯
    public Item a_slot; //아이템 정보를 담아줄 슬롯

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
                equipitem.itemOptionOff(currentCharacter); //아이템 해제시 캐릭터에게 적용된 옵션을 해제하는 함수.
                w_slot = null;
            }
        }
        else
        {
            EquipItem equipitem = (EquipItem)a_slot;
            if(a_slot != null)
            {
                inventory.Container[a_slot.itemID].amount++;
                equipitem.itemOptionOff(currentCharacter); //아이템 해제시 캐릭터에게 적용된 옵션을 해제하는 함수.
                a_slot = null;
            }
        }
        //선택된 장비가 있을경우 해당 장비 장착 해제.
    }
    //해당하는 장비를 장착중이 아닐경우 해당 오브젝트를 비활성화 시키도록 하기.
}
