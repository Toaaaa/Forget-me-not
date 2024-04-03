using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : ScriptableObject
{
    public GameObject prefab; //�����ý��ۿ��� ������ ������� ���������� �ҷ����� ���.
    public int Id; // ó�� ���鶧 id�� �ο�������� (�������)
    public string Name;
    public Sprite sprite;

    public int Hp;
    public int Atk;
    public int Def;
    public int Speed;

    public int ExpReward;
    public int GoldReward;

    public void Attack()
    {
        UseSkill();
    }

    protected virtual void UseSkill()
    {

    } 
}
