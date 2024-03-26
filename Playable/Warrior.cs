using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Warrior", menuName = "PlayableCharacter/Warrior")]
public class Warrior : PlayableC
{
    public skillWarrior skillWarrior;

    private void Awake()
    {
        skillWarrior = new skillWarrior();
    }

    void Update()
    {
        if(skillWarrior == null)
        {
            skillWarrior = new skillWarrior();
        }
    }
}

[System.Serializable]
public class skillWarrior //���� ��ų
{

    void skill1(Warrior warrior)
    {
        Debug.Log("���� ��ų1 ���");
    }
    void skill2(Warrior warrior)
    {
        Debug.Log("���� ��ų2 ���");
    }
    void skill3(Warrior warrior)
    {
        Debug.Log("���� ��ų3 ���");
    }
    void skill4(Warrior warrior)
    {
        Debug.Log("���� ��ų4 ���");
    }
}
