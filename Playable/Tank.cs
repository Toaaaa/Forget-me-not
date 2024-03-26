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
public class skillTank //탱커 스킬
{
    void skill1(Tank tank)
    {
        Debug.Log("탱커 스킬1 사용");
    }
    void skill2(Tank tank)
    {
        Debug.Log("탱커 스킬2 사용");
    }
    void skill3(Tank tank)
    {
        Debug.Log("탱커 스킬3 사용");
    }
    void skill4(Tank tank)
    {
        Debug.Log("탱커 스킬4 사용");
    }
}
