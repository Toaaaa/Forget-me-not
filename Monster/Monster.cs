using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : ScriptableObject
{
    public GameObject prefab; //전투시스템에서 몬스터의 형상등을 실직적으로 불러낼때 사용.
    public int Id; // 처음 만들때 id를 부여해줘야함 (순서대로)
    public string Name;
    public Sprite sprite;

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
}
