using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //애는 프리팹으로 만들것.
{
    /////피격시 플래시 이펙트 변수/////
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;
    /////
    public Monster monster;
    public MobSlot thisSlot;//이 몹위치에 대응되는 Canvas 상의 슬롯.
    public Animator shadowAnimator;//그림자 애니메이터
    public List<skills> monsterSkill;
    public List<skills> monsterOnlyAttack; //회복 등의 버프형 스킬이 아닌 공격형 스킬만 모와놓은 리스트.
    public PlayableC target; //스킬을 사용할 대상.
    public bool isslowed; //스킬에 의해 속도가 감소되었는지 판별하는 변수.
    public int slowStack; //속도 감소 스킬에 의해 쌓인 스택.
    public SkillType stackedElement; // 공격에 의해서 쌓인 속성 스택.
    //skillType 이 none일 경우 >>//따로 속성이 안 붙어있는 경우. (전투 시작단계 or 역속성 공격을 하였을 경우 속성 스택 초기화)
    public float Hp;
    public float MaxHp;
    public float Atk;
    public float Def;
    public int Speed;
    public int MinimumSpeed;

    public bool isDead;

    public List<GameObject> SFXInfo;//몬스터가 사용하는 스킬에 대한 이펙트정보
    public List<GameObject> SFX;//몬스터가 사용하는 버프에 대한 실제로 사용할 이펙트들.
    public List<GameObject> playerSFX;//플레이어가 사용하는 스킬에 대한 이펙트정보(디버프 계열등.. 다중 공격시에 몬스터에 표시될 sfx)


    //몬스터 상태 ui용 변수
    public bool isAtkBuffed;
    public bool isDefBuffed;
    public bool isDefDebuffed;
    public bool isSpeedDebuffed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 원래 색상 저장
    }

    private void OnEnable()
    {
        ResetAnimator();
        Hp = monster.mHp;
        MaxHp = monster.mHp;
        Atk = monster.mAtk;
        Def = monster.mDef;
        Speed = monster.mSpeed;
        slowStack = 0;
        MinimumSpeed = monster.mMinimumSpeed;
        stackedElement = SkillType.none;
        isDead = false;
        isAtkBuffed = false;
        isDefBuffed = false;
        isDefDebuffed = false;
        isSpeedDebuffed = false;

        SFX = new List<GameObject>();
        for (int i = 0; i < SFXInfo.Count; i++)
        {
            GameObject obj = Instantiate(SFXInfo[i], transform.position, Quaternion.identity);
            SFX.Add(obj);
            obj.SetActive(false);
        }//몬스터가 사용하는 스킬에 대한 이펙트를 생성.
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
    public void TakeDamage()// 피격 시 반짝임 효과
    {
        StartCoroutine(Flash());
    }

    private void ResetAnimator()//본인과 그림자의 애니메이터 초기화
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("attacking", false);
        anim.SetBool("death", false);
        shadowAnimator.SetBool("attacking", false);
        shadowAnimator.SetBool("death", false);
    }

    private void Update()
    {
        //몬스터 상태 ui 업데이트
        if(Atk > monster.mAtk)
        {
            isAtkBuffed= true;
        }
        else
        {
            isAtkBuffed = false;
        }
        if(Def > monster.mDef)
        {
            isDefBuffed = true;
        }
        else
        {
            isDefBuffed = false;
        }
        if(Def < monster.mDef)
        {
            isDefDebuffed = true;
        }
        else
        {
            isDefDebuffed = false;
        }
        if(Speed < monster.mSpeed)
        {
            isslowed = true;
            isSpeedDebuffed = true;
        }
        else
        {
            isslowed = false;
            isSpeedDebuffed = false;
        }
        //

    }
    //죽으면 monster.whenDie() 호출


    private IEnumerator Flash()
    {
        spriteRenderer.color = Color.red; // 피격 시 빨간색으로 변경
        yield return new WaitForSeconds(flashDuration); // 일정 시간 대기
        spriteRenderer.color = originalColor; // 원래 색상으로 복원
    }
}
