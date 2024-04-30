using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //애는 프리팹으로 만들것.
{
    public MobSlot mobSlot;
    public Monster monster;
    public List<skills> monsterSkill;
    public PlayableC target; //스킬을 사용할 대상.
    public bool isslowed; //스킬에 의해 속도가 감소되었는지 판별하는 변수.

    public float Hp;
    public float MaxHp;
    public float Atk;
    public float Def;
    public int Speed;

    public bool isDead;


    private void OnEnable()
    {
        Hp = monster.mHp;
        MaxHp = monster.mHp;
        Atk = monster.mAtk;
        Def = monster.mDef;
        Speed = monster.mSpeed;
    }
    private void OnDisable()
    {
        isslowed = false;
    
    }

    private void Update()
    {
        //(Input.GetKeyDown(KeyCode.Space))
            //monsterSkill[0].UseSkill(this);
    }
    //죽으면 monster.whenDie() 호출
}
