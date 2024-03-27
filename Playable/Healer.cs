using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healer", menuName = "PlayableCharacter/Healer")]
public class Healer : PlayableC
{
    public skillHealer skillHealer;


    private void Awake()
    {
        skillHealer = new skillHealer();
    }
    void Update()
    {
        if(skillHealer == null)
        {
            skillHealer = new skillHealer();
        }
    }   

}
[System.Serializable]
public class skillHealer //힐러 스킬
{
    void skill1(Healer healer)
    {
        Debug.Log("힐러 스킬1 사용");
    }
    void skill2(Healer healer)
    {
        Debug.Log("힐러 스킬2 사용");
    }
    void skill3(Healer healer)
    {
        Debug.Log("힐러 스킬3 사용");
    }
    void skill4(Healer healer)
    {
        Debug.Log("힐러 스킬4 사용");
    }
}
