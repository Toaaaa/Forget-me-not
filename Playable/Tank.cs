using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "PlayableCharacter/Tank")]
public class Tank : PlayableC
{
    public skillTank skillTank;

    private void Awake()
    {
        skillTank = new skillTank();
    }
    void Update()
    {
        if(skillTank == null)
        {
            skillTank = new skillTank();
        }
    }
    
}
[System.Serializable]
public class skillTank //��Ŀ ��ų
{
    void skill1(Tank tank)
    {
        Debug.Log("��Ŀ ��ų1 ���");
    }
    void skill2(Tank tank)
    {
        Debug.Log("��Ŀ ��ų2 ���");
    }
    void skill3(Tank tank)
    {
        Debug.Log("��Ŀ ��ų3 ���");
    }
    void skill4(Tank tank)
    {
        Debug.Log("��Ŀ ��ų4 ���");
    }
}
