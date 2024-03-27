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
public class skillHealer //���� ��ų
{
    void skill1(Healer healer)
    {
        Debug.Log("���� ��ų1 ���");
    }
    void skill2(Healer healer)
    {
        Debug.Log("���� ��ų2 ���");
    }
    void skill3(Healer healer)
    {
        Debug.Log("���� ��ų3 ���");
    }
    void skill4(Healer healer)
    {
        Debug.Log("���� ��ų4 ���");
    }
}
