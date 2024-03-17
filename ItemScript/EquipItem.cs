using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Inventory/Items/Equipment")]
public class EquipItem : Item
{
    //���� ������ �巡�� ����, �Һ�� Ŭ��(������ ����) + Ŭ��(ĳ���� ����) ����

    public GameObject OnThisCharacter;//��� ������ ĳ����
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
        //��� �����ϴ� ĳ������ �����Ͱ��� ����
        //���콺�� �������� raycasting�� ĳ������ ���� �޾Ƽ� ������ ����
    }
    public void UnEquip()
    {
        Debug.Log("unEquip"); 
    }
}
