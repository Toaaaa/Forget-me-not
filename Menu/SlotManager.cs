using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
