using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public PlayableC currentCharacter;
    public GameObject weaponSlot;
    public GameObject accSlot;

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
    }
    //해당하는 장비를 장착중이 아닐경우 해당 오브젝트를 비활성화 시키도록 하기.
}
