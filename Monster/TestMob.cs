using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //�ִ� ���������� �����.
{

    public Monster monster;
    public MobSlot thisSlot;//�� ����ġ�� �����Ǵ� Canvas ���� ����.
    public List<skills> monsterSkill;
    public List<skills> monsterOnlyAttack; //ȸ�� ���� ������ ��ų�� �ƴ� ������ ��ų�� ��ͳ��� ����Ʈ.
    public PlayableC target; //��ų�� ����� ���.
    public bool isslowed; //��ų�� ���� �ӵ��� ���ҵǾ����� �Ǻ��ϴ� ����.
    public SkillType stackedElement; // ���ݿ� ���ؼ� ���� �Ӽ� ����.
    //skillType �� none�� ��� >>//���� �Ӽ��� �� �پ��ִ� ���. (���� ���۴ܰ� or ���Ӽ� ������ �Ͽ��� ��� �Ӽ� ���� �ʱ�ȭ)
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
        }//����� �������� �������(null�� �ƴҰ��) list�� �߰�
    }
    private Item GetItemFromList()//���� Ȯ���� ����, �ش� ���Ͱ� �����ϰ� �ִ� �������� ����Ѵ�.
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
    //������ monster.whenDie() ȣ��
}
