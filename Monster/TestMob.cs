using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //�ִ� ���������� �����.
{
    public enum Element
    {
        Fire,
        Water,
        Wood,
        none,//���� �Ӽ��� �� �پ��ִ� ���. (���� ���۴ܰ� or ���Ӽ� ������ �Ͽ��� ��� �Ӽ� ���� �ʱ�ȭ)
    }

    public Monster monster;
    public MobSlot thisSlot;//�� ����ġ�� �����Ǵ� Canvas ���� ����.
    public List<skills> monsterSkill;
    public List<skills> monsterOnlyAttack; //ȸ�� ���� ������ ��ų�� �ƴ� ������ ��ų�� ��ͳ��� ����Ʈ.
    public PlayableC target; //��ų�� ����� ���.
    public bool isslowed; //��ų�� ���� �ӵ��� ���ҵǾ����� �Ǻ��ϴ� ����.
    public Element stackedElement; // ���ݿ� ���ؼ� ���� �Ӽ� ����.

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
    //������ monster.whenDie() ȣ��
}
