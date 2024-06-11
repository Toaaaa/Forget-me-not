using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consume Item", menuName = "Inventory/Items/Consume")]
public class ConsumeItem : Item
{
    //소모품 아이템의 경우 각각의 소모품 효과가 들어있는 데이터 클래스를 만들어서 potionNum2(int i, intj) 같이만들어 접근, 사용할수 있게 할것.
    public int effectAmount; //효과의 수치
    public ConsumeType consumeType;
    public BuffType buffType;
    public int buffDuration; //버프아이템의 경우 지속시간.
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
                BuffUse(character);//버프아이템의 경우 파티원 전체에 적용된다. //버프 아이템중 special일 경우 개별의 캐릭터에게 영구적인 효과를 주기에 character를 받아온다.
                break;
            default:
                break;
        }
    }
    public void OnEnd() //버프아이템의 시간이 다 되었을때 사용을 끝내는 함수.
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
                    //영구적인 버프아이템의 경우 아무것도 하지 않는다.
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
    private void BuffSpecial(PlayableC c) //영구적인 능력치 상승.
    {
        switch (this.name)
        {
            case "CRITICAL":
                Debug.Log("Potion");
                break;
            case "Blood Elixir":
                Debug.Log("복용자 체력 10 증가. + 체력 최대치로 회복.");
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
