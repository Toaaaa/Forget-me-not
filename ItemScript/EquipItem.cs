using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
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

    public int atk;
    public int def;
    public int hp;
    public int mp;
    public int spd;

    private void Awake()
    {
        itemType = ItemType.Equipment;
    }

    public void itemOption(PlayableC character) //ó�� ���� �ɶ� �������� �ɼ��� ĳ���Ϳ��� �����Ű�� �Լ�.
    {
        //characterType ����Ʈ�� ���ԵǾ� �ִ� character ĳ���͸� �ɼ��� �����ϴ� if��.
        CharacterType charatype = (CharacterType)Enum.Parse(typeof(CharacterType), character.name);
        if (characterType.Contains(charatype))
        {
            character.hp += hp;
            character.mp += mp;
            character.atk += atk;
            character.def += def;
            character.spd += spd;
        }
        else
        {
            Debug.Log("�ɼ� ���� �Ұ���");
        }
        
    }
    public void itemOptionAcc(PlayableC character)
    {
        character.hp += hp;
        character.mp += mp;
        character.atk += atk;
        character.def += def;
        character.spd += spd;
    }


    public void itemOptionOff(PlayableC character) //�������� �����Ҷ� ĳ���Ϳ��� ����� �ɼ��� �����ϴ� �Լ�.
    {
        CharacterType charatype = (CharacterType)Enum.Parse(typeof(CharacterType), character.name);
        if (characterType.Contains(charatype))
        {
            character.hp -= hp;
            character.mp -= mp;
            character.atk -= atk;
            character.def -= def;
            character.spd -= spd;
        }
        else
        {
            Debug.Log("�ɼ� ���� �Ұ���");
        }
    }
    public void itemOptionOffAcc(PlayableC character)
    {
        character.hp -= hp;
        character.mp -= mp;
        character.atk -= atk;
        character.def -= def;
        character.spd -= spd;
    }
}
