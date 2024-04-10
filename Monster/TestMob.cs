using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //애는 프리팹으로 만들것.
{
    public Monster monster;
    public List<skills> monsterSkill;
    public PlayableC target; //스킬을 사용할 대상.

    public int Hp;
    public int MaxHp;
    public int Atk;
    public int Def;
    public int Speed;


    private void OnEnable()
    {
        Hp = monster.mHp;
        MaxHp = monster.mHp;
        Atk = monster.mAtk;
        Def = monster.mDef;
        Speed = monster.mSpeed;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            monsterSkill[0].UseSkill(this);
    }
    //죽으면 monster.whenDie() 호출
}
