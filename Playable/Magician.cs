using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{
   public skillMagician skillMagician;


    private void Awake()
    {
        skillMagician = new skillMagician();
    }

    void Update()
    {
        if(skillMagician == null)
        {
            skillMagician = new skillMagician();
        }
    }
}

[System.Serializable]
public class skillMagician //마법사 스킬
{

    void skill1(Magician magician)
    {
        Debug.Log("마법사 스킬1 사용");
    }
    void skill2(Magician magician)
    {
        Debug.Log("마법사 스킬2 사용");
    }
    void skill3(Magician magician)
    {
        Debug.Log("마법사 스킬3 사용");
    }
    void skill4(Magician magician)
    {
        Debug.Log("마법사 스킬4 사용");
    }
}
