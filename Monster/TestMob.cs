using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //애는 프리팹으로 만들것.
{
    public enum Element
    {
        Fire,
        Water,
        Wood,
        none,//따로 속성이 안 붙어있는 경우. (전투 시작단계 or 역속성 공격을 하였을 경우 속성 스택 초기화)
    }

    public Monster monster;
    public MobSlot thisSlot;//이 몹위치에 대응되는 Canvas 상의 슬롯.
    public List<skills> monsterSkill;
    public List<skills> monsterOnlyAttack; //회복 등의 버프형 스킬이 아닌 공격형 스킬만 모와놓은 리스트.
    public PlayableC target; //스킬을 사용할 대상.
    public bool isslowed; //스킬에 의해 속도가 감소되었는지 판별하는 변수.
    public Element stackedElement; // 공격에 의해서 쌓인 속성 스택.

    public float Hp;
    public float MaxHp;
    public float Atk;
    public float Def;
    public int Speed;
    public int MinimumSpeed;

    public bool isDead;


    private void OnEnable()
    {
        Hp = monster.mHp;
        MaxHp = monster.mHp;
        Atk = monster.mAtk;
        Def = monster.mDef;
        Speed = monster.mSpeed;
        MinimumSpeed = monster.mMinimumSpeed;
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
