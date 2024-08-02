using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //애는 프리팹으로 만들것.
{

    public Monster monster;
    public MobSlot thisSlot;//이 몹위치에 대응되는 Canvas 상의 슬롯.
    public List<skills> monsterSkill;
    public List<skills> monsterOnlyAttack; //회복 등의 버프형 스킬이 아닌 공격형 스킬만 모와놓은 리스트.
    public PlayableC target; //스킬을 사용할 대상.
    public bool isslowed; //스킬에 의해 속도가 감소되었는지 판별하는 변수.
    public SkillType stackedElement; // 공격에 의해서 쌓인 속성 스택.
    //skillType 이 none일 경우 >>//따로 속성이 안 붙어있는 경우. (전투 시작단계 or 역속성 공격을 하였을 경우 속성 스택 초기화)
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
        stackedElement = SkillType.none;
    }
    private void OnDisable()
    {
        isslowed = false;
        CombatManager.Instance.DeadMobExpCount += monster.ExpReward;
        CombatManager.Instance.DeadMobGoldCount += monster.GoldReward;
        Item mitem = GetItemFromList();
        if(mitem != null)
        {
            CombatManager.Instance.DeadMobItemDrop.Add(mitem);
        }//드랍된 아이템이 있을경우(null이 아닐경우) list에 추가
    }
    private Item GetItemFromList()//일정 확률에 따라, 해당 몬스터가 소지하고 있는 아이템을 드랍한다.
    {
        int randomValue = Random.Range(0, 100);
        int sum = 0;
        for (int i = 0; i < monster.mItems.Count; i++)
        {
            sum += monster.mItems[i].dropRate;
            if (randomValue < sum)
            {
                return monster.mItems[i].item;
            }
        }
        return null;
    }
    


    private void Update()
    {
        //(Input.GetKeyDown(KeyCode.Space))
            //monsterSkill[0].UseSkill(this);
    }
    //죽으면 monster.whenDie() 호출
}
