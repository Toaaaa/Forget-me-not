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
public class skillMagician //������ ��ų
{

    void skill1(Magician magician)
    {
        Debug.Log("������ ��ų1 ���");
    }
    void skill2(Magician magician)
    {
        Debug.Log("������ ��ų2 ���");
    }
    void skill3(Magician magician)
    {
        Debug.Log("������ ��ų3 ���");
    }
    void skill4(Magician magician)
    {
        Debug.Log("������ ��ų4 ���");
    }
}
