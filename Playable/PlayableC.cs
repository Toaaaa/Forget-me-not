using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableC : ScriptableObject
{
    public int level;
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;
    public int atk;
    public int def;
    public int spd;
    public int crit;
    public int exp;
    public int maxExp;
    public int priority;
    public int fatigue;
    public int maxFatigue;

    //debuff
    public bool isPoisoned;
    public bool isSkillSealed;//스킬 봉인

    public int jobNum; // 0:전사 1:마법사 2:탱커 3:힐러
    public Item equipedWeapon;
    public Item equipedAcc;



    //일단은 여기까지만

    // Update is called once per frame
    void Update()
    {
        
    }
}

