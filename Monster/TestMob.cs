using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //�ִ� ���������� �����.
{
    public MobSlot mobSlot;
    public Monster monster;
    public List<skills> monsterSkill;
    public PlayableC target; //��ų�� ����� ���.
    public bool isslowed; //��ų�� ���� �ӵ��� ���ҵǾ����� �Ǻ��ϴ� ����.

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
    //������ monster.whenDie() ȣ��
}
