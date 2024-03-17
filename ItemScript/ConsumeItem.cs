using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consume Item", menuName = "Inventory/Items/Consume")]
public class ConsumeItem : Item
{
    public int restoreAmount;
    public ConsumeType consumeType;
    public BuffType buffType;
    public enum ConsumeType
    {
        Health,
        Mana,
        Stamina,
        Buff
    }
    public enum BuffType
    {
        None, //Consumetype이 Buff가 아닌경우
        Attack,
        Defence,
        Speed,
        Special
    }
    private void Awake()
    {
        itemType = ItemType.Consumable;
    }

    public void OnUse() 
    { 
        switch (consumeType)
        {
            case ConsumeType.Health:
                Debug.Log("Health");
                break;
            case ConsumeType.Mana:
                Debug.Log("Mana");
                break;
            case ConsumeType.Stamina:
                Debug.Log("Stamina");
                break;
            case ConsumeType.Buff:
                Debug.Log("Buff");
                BuffUse();
                break;
            default:
                break;
        }
    }

    private void BuffUse()
    {
        switch (buffType)
        {
            case BuffType.Attack:
                BuffAtt();
                break;
            case BuffType.Defence:
                BuffDef();
                break;
            case BuffType.Speed:
                BuffSpeed();
                break;
            case BuffType.Special:
                BuffSpecial();
                break;
            default:
                break;
        }
    }

    private void BuffAtt()
    {
        Debug.Log("Attack");
    }
    private void BuffDef()
    {
        Debug.Log("Defence");
    }
    private void BuffSpeed()
    {
        Debug.Log("Speed");
    }
    private void BuffSpecial()
    {
        Debug.Log("Special");
    }
}
