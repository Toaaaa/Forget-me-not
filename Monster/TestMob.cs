using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //�ִ� ���������� �����.
{
    public Monster monster;
    public List<skills> monsterSkill;
    public PlayableC target; //��ų�� ����� ���.

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
    //������ monster.whenDie() ȣ��
}
