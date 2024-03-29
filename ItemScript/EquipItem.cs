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
    //장비는 장착시 드래그 형식, 소비는 클릭(아이템 선택) + 클릭(캐릭터 선택) 형식

    public GameObject OnThisCharacter;//장비를 장착할 캐릭터 
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

    public void itemOption(PlayableC character) //처음 장착 될때 아이템의 옵션을 캐릭터에게 적용시키는 함수.
    {
        //characterType 리스트에 포함되어 있는 character 캐릭터만 옵션을 적용하는 if문.
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
            Debug.Log("옵션 적용 불가능");
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


    public void itemOptionOff(PlayableC character) //아이템을 해제할때 캐릭터에게 적용된 옵션을 해제하는 함수.
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
            Debug.Log("옵션 적용 불가능");
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
