using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consume Item", menuName = "Inventory/Items/Consume")]
public class ConsumeItem : Item
{
    //�Ҹ�ǰ �������� ��� ������ �Ҹ�ǰ ȿ���� ����ִ� ������ Ŭ������ ���� potionNum2(int i, intj) ���̸���� ����, ����Ҽ� �ְ� �Ұ�.
    public int effectAmount; //ȿ���� ��ġ
    public ConsumeType consumeType;
    public BuffType buffType;
    public int buffDuration; //������������ ��� ���ӽð�.
    public enum ConsumeType
    {
        Health,
        Mana,
        Stamina,
        Buff
    }
    public enum BuffType
    {
        None, //Consumetype�� Buff�� �ƴѰ��
        Attack,
        Defence,
        Speed,
        Special
    }
    private void Awake()
    {
        itemType = ItemType.Consumable;
    }

    public void OnUse(PlayableC character) 
    { 
        switch (consumeType)
        {
            case ConsumeType.Health:
                restoreHp(character);
                break;
            case ConsumeType.Mana:
                restoreMp(character);
                break;
            case ConsumeType.Stamina:
                restoreStamina(character);
                break;
            case ConsumeType.Buff:
                BuffUse();//������������ ��� ��Ƽ�� ��ü�� ����ȴ�.
                break;
            default:
                break;
        }
    }
    public void OnEnd() //������������ �ð��� �� �Ǿ����� ����� ������ �Լ�.
    {
        if(consumeType == ConsumeType.Buff)
        {
            switch (buffType)
            {
                case BuffType.Attack:
                    BuffAtt(false);
                    break;
                case BuffType.Defence:
                    BuffDef(false);
                    break;
                case BuffType.Speed:
                    BuffSpeed(false);
                    break;
                case BuffType.Special:
                    BuffSpecial();
                    break;
                default:
                    break;
            }
        }
    }
    private void BuffUse()
    {
        Debug.Log("���������� ���");
        CombatManager.Instance.consumeTimer = buffDuration;
        CombatManager.Instance.BuffIsOn = true;
        CombatManager.Instance.consumeOnUse = this;

        switch (buffType)
        {
            case BuffType.Attack:
                BuffAtt(true);
                break;
            case BuffType.Defence:
                BuffDef(true);
                break;
            case BuffType.Speed:
                BuffSpeed(true);
                break;
            case BuffType.Special:
                BuffSpecial();
                break;
            default:
                break;
        }
    }

    private void BuffAtt(bool b)
    {
        if (b)
        {
            for(int i =0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].atk += effectAmount;
            }
        }
        else
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].atk -= effectAmount;
            }
        }
    }
    private void BuffDef(bool b)
    {
        if(b)
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].def += effectAmount;
            }
        }
        else
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].def -= effectAmount;
            }
        }
    }
    private void BuffSpeed(bool b)
    {
        if (b)
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].spd += effectAmount;
            }
        }
        else
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].spd -= effectAmount;
            }
        }
    }
    private void BuffSpecial() //�������� �ɷ�ġ ���.
    {
        switch (this.name)
        {
            case "CRITICAL":
                Debug.Log("Potion");
                break;
            case "Elixir":
                Debug.Log("Elixir");
                break;
            case "Ether":
                Debug.Log("Ether");
                break;
            case "StaminaPotion":
                Debug.Log("StaminaPotion");
                break;
            default:
                break;
        }

    }

    private void restoreHp(PlayableC character)
    {
        character.hp = (character.hp+effectAmount > character.maxHp)? character.maxHp : character.hp + effectAmount;
    }
    private void restoreMp(PlayableC character)
    {
        character.mp = (character.mp + effectAmount > character.maxMp) ? character.maxMp : character.mp + effectAmount;
    }
    private void restoreStamina(PlayableC character)
    {
        character.fatigue = (character.fatigue - effectAmount < 0) ? 0 : character.fatigue - effectAmount;
    }
}
