using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlayableC;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Inventory/Items/Equipment")]
public class EquipItem : Item
{
    //���� ������ �巡�� ����, �Һ�� Ŭ��(������ ����) + Ŭ��(ĳ���� ����) ����

    public GameObject OnThisCharacter;//��� ������ ĳ���� 
    public OptionType[] options;
    public List<CharacterType> characterType;
    public EquipType equipType;
    public bool optionOn;
    

    public bool isAcc;

    
    public enum EquipType
    {
        Weapon,
        Accessory
    }
    public enum OptionType
    {
        Atk,
        Def,
        Hp,
        Mp,
        Spd
    }
    public enum CharacterType
    {
        Warrior,
        Magician,
        Tank,
        Healer
    }

    public float atk;
    public float def;
    public float hp;
    public float mp;
    public float spd;

    private void Awake()
    {
        itemType = ItemType.Equipment;
    }

    public void itemOption(PlayableC character) //ó�� ���� �ɶ� �������� �ɼ��� ĳ���Ϳ��� �����Ű�� �Լ�.
    {
        //characterType ����Ʈ�� ���ԵǾ� �ִ� character ĳ���͸� �ɼ��� �����ϴ� if��
        if (characterType.ToString().Contains(character.name))
        {
            Debug.Log("�ɼ� ���� ����");
        }
        else
        {
            Debug.Log("�ɼ� ���� �Ұ���");
        }
        
    }
}
