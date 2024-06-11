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
                BuffUse(character);//������������ ��� ��Ƽ�� ��ü�� ����ȴ�. //���� �������� special�� ��� ������ ĳ���Ϳ��� �������� ȿ���� �ֱ⿡ character�� �޾ƿ´�.
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
                    //�������� ������������ ��� �ƹ��͵� ���� �ʴ´�.
                    break;
                default:
                    break;
            }
        }
    }
    private void BuffUse(PlayableC c)
    {
        switch (buffType)
        {
            case BuffType.Attack:
                CombatManager.Instance.consumeTimer = buffDuration;
                CombatManager.Instance.BuffIsOn = true;
                CombatManager.Instance.consumeOnUse = this;
                BuffAtt(true);
                break;
            case BuffType.Defence:
                CombatManager.Instance.consumeTimer = buffDuration;
                CombatManager.Instance.BuffIsOn = true;
                CombatManager.Instance.consumeOnUse = this;
                BuffDef(true);
                break;
            case BuffType.Speed:
                CombatManager.Instance.consumeTimer = buffDuration;
                CombatManager.Instance.BuffIsOn = true;
                CombatManager.Instance.consumeOnUse = this;
                BuffSpeed(true);
                break;
            case BuffType.Special:
                BuffSpecial(c);
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
                GameManager.Instance.playableManager.joinedPlayer[i].isBuffed = true;
                GameManager.Instance.playableManager.joinedPlayer[i].attackBuff = true;
            }
        }
        else
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].atk -= effectAmount;
                GameManager.Instance.playableManager.joinedPlayer[i].isBuffed = false;
                GameManager.Instance.playableManager.joinedPlayer[i].attackBuff = false;
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
                GameManager.Instance.playableManager.joinedPlayer[i].isBuffed = true;
                GameManager.Instance.playableManager.joinedPlayer[i].defenseBuff = true;
            }
        }
        else
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].def -= effectAmount;
                GameManager.Instance.playableManager.joinedPlayer[i].isBuffed = false;
                GameManager.Instance.playableManager.joinedPlayer[i].defenseBuff = false;
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
                GameManager.Instance.playableManager.joinedPlayer[i].isBuffed = true;
                GameManager.Instance.playableManager.joinedPlayer[i].speedBuff = true;
            }
        }
        else
        {
            for (int i = 0; GameManager.Instance.playableManager.joinedPlayer.Count > i; i++)
            {
                GameManager.Instance.playableManager.joinedPlayer[i].spd -= effectAmount;
                GameManager.Instance.playableManager.joinedPlayer[i].isBuffed = false;
                GameManager.Instance.playableManager.joinedPlayer[i].speedBuff = false;
            }
        }
    }
    private void BuffSpecial(PlayableC c) //�������� �ɷ�ġ ���.
    {
        switch (this.name)
        {
            case "CRITICAL":
                Debug.Log("Potion");
                break;
            case "Blood Elixir":
                Debug.Log("������ ü�� 10 ����. + ü�� �ִ�ġ�� ȸ��.");
                c.maxHp += 10;
                c.hp = c.maxHp;
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
