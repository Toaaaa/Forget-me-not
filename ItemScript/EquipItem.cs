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
    public SkillType element; //�ش� �Ǽ��縮�� �Ӽ�.

    public bool isAcc; //�Ǽ��縮���� �Ǻ��ϴ� bool. (�Ӽ���ȯ�� �Ǽ��縮�� ����)
    public bool isElemental;//�Ӽ� ��ȯ �Ǽ��縮�� ���, �������� ������ �ش�Ӽ����� ������Ŵ.
    
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
    public void itemOptionAcc(PlayableC character) //�Ǽ��縮 ������ ȿ�� ����.
    {
        character.hp += hp;
        character.mp += mp;
        character.atk += atk;
        character.def += def;
        character.spd += spd;

        if(this.isElemental) //�ش� �Ǽ��縮�� �Ӽ���ȯ �Ǽ��縮�϶�, ĳ������ ���� �Ӽ��� �ش� �Ӽ����� ������Ŵ.
            character.SetElement(this.element);
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
        character.equipedWeapon = null;
    }
    public void itemOptionOffAcc(PlayableC character)
    {
        character.hp -= hp;
        character.mp -= mp;
        character.atk -= atk;
        character.def -= def;
        character.spd -= spd;
        if (this.isElemental) //�ش� �Ǽ��縮�� �Ӽ���ȯ �Ǽ��縮�϶�, ĳ������ ���� �Ӽ��� ������� ��������.
        {
            character.ResetElement();
        }

        character.equipedAcc = null;
    }

    
}
