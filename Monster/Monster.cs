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
    public GameObject prefab; //�����ý��ۿ��� ������ ������� ���������� �ҷ����� ���.
    public int Id; // ó�� ���鶧 id�� �ο�������� (�������)
    public string Name;
    public Sprite sprite;
    public MonsterType monsterType; //normal�� ��� ���ְ���, boss�� ��� ���� �Ұ���.
    public List<skills> monsterSkill;
    public List<skills> monsterOnlyAttack; //ȸ�� ���� ������ ��ų�� �ƴ� ������ ��ų�� ��Ƴ��� ����Ʈ.


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
    //�ش� �ڵ�� rewardDisplay���� ó���ϵ��� ������.

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
