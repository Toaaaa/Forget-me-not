using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MonoBehaviour //�ִ� ���������� �����.
{
    public Monster monster;

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

    //������ monster.whenDie() ȣ��
}
