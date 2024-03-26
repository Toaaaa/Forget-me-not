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
public class skillWarrior //전사 스킬
{

    void skill1(Warrior warrior)
    {
        Debug.Log("전사 스킬1 사용");
    }
    void skill2(Warrior warrior)
    {
        Debug.Log("전사 스킬2 사용");
    }
    void skill3(Warrior warrior)
    {
        Debug.Log("전사 스킬3 사용");
    }
    void skill4(Warrior warrior)
    {
        Debug.Log("전사 스킬4 사용");
    }
}
