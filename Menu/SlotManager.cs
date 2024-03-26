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
    //�ش��ϴ� ��� �������� �ƴҰ�� �ش� ������Ʈ�� ��Ȱ��ȭ ��Ű���� �ϱ�.
}
