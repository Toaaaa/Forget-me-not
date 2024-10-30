using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Normal,
    Boss
}

[System.Serializable]
public class MonsterItem
{
    public int dropRate;
    public Item item;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class Monster : ScriptableObject
{
    public GameObject prefab; //전투시스템에서 몬스터의 형상등을 실직적으로 불러낼때 사용.
    public int Id; // 처음 만들때 id를 부여해줘야함 (순서대로)
    public string Name;
    public Sprite sprite;
    public MonsterType monsterType; //normal의 경우 도주가능, boss의 경우 도주 불가능.
    public List<skills> monsterSkill;
    public List<skills> monsterOnlyAttack; //회복 등의 버프형 스킬이 아닌 공격형 스킬만 모아놓은 리스트.


    public string SpecialSkillName;
    public string SpecialSkillDesc;

    public float mHp;
    public float mAtk;
    public float mDef;
    public int mSpeed;
    public int mMinimumSpeed;
    

    public float ExpReward;
    public int GoldReward;
    public List<MonsterItem> mItems;



    /*public void WhenDie()
    {
        int count =GameManager.Instance.playableManager.joinedPlayer.Count;
        for (int i = 0; i < count; i++)
        {
            GameManager.Instance.playableManager.joinedPlayer[i].exp += GiveExp()/count;
            GameManager.Instance.inventory.goldHave += GiveGold();
        }
    }*/
    //해당 코드는 rewardDisplay에서 처리하도록 변경함.

    public int GiveGold()
    {
        int gold = GoldReward*(int)Random.Range(0.8f, 1.2f);
        return gold;
    }
    public int GiveExp()
    {
        //int exp = ExpReward * (int)Random.Range(0.9f, 1.1f);
        //return exp;
        return 0;
    }

    public void Attack()
    {

    }
    public void Skill()
    {

    }
}
