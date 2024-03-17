using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Inventory/Items/Equipment")]
public class EquipItem : Item
{
    //장비는 장착시 드래그 형식, 소비는 클릭(아이템 선택) + 클릭(캐릭터 선택) 형식

    public GameObject OnThisCharacter;//장비를 장착할 캐릭터
    public EquipType equipType;
    public enum EquipType
    {
        Weapon,
        Accessory
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

    public void Equip()
    {
        //장비를 장착하는 캐릭터의 데이터값을 변경
        //마우스를 놓을때의 raycasting된 캐릭터의 값을 받아서 데이터 수정
    }
    public void UnEquip()
    {
        Debug.Log("unEquip"); 
    }
}
