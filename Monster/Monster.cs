using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Normal,
    Boss
}

[System.Serializable]
public class Monster : ScriptableObject
{
    public GameObject prefab; //전투시스템에서 몬스터의 형상등을 실직적으로 불러낼때 사용.
    public int Id; // 처음 만들때 id를 부여해줘야함 (순서대로)
    public string Name;
    public Sprite sprite;
    public MonsterType monsterType; //normal의 경우 도주가능, boss의 경우 도주 불가능.

    public int Hp;
    public int Atk;
    public int Def;
    public int Speed;

    public int ExpReward;
    public int GoldReward;

    public void Attack()
    {
        UseSkill();
    }

    protected virtual void UseSkill()
    {

    } 

    public void WhenDie()
    {
        int count =GameManager.Instance.playableManager.joinedPlayer.Count;
        for (int i = 0; i < count; i++)
        {
            GameManager.Instance.playableManager.joinedPlayer[i].exp += GiveExp()/count;
            GameManager.Instance.inventory.goldHave += GiveGold();
        }
        DropItem();
    }

    public int GiveGold()
    {
        int gold = GoldReward*(int)Random.Range(0.8f, 1.2f);
        return gold;
    }
    public int GiveExp()
    {
        int exp = ExpReward * (int)Random.Range(0.9f, 1.1f);
        return exp;
    }

    protected virtual void DropItem()
    {

    }


}
