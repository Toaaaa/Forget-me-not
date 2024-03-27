using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public PlayableC currentCharacter;
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

    //해당하는 장비를 장착중이 아닐경우 해당 오브젝트를 비활성화 시키도록 하기.
}
