using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public GameObject[] SFXs; //시전자의 앞에서 나올 sfx

    public void PlayerSfx0()//기본공격 
    {
        SFXs[0].SetActive(false);
        SFXs[0].SetActive(true);
    }
    public void PlayerSfx1()//스킬1
    {
        SFXs[1].SetActive(false);
        SFXs[1].SetActive(true);
    }
    public void PlayerSfx2()//스킬2
    {
        SFXs[2].SetActive(false);
        SFXs[2].SetActive(true);
    }
    public void PlayerSfx2OnMonster(Transform monsterT)//몬스터의 위치에 표시될 스킬
    {
        SFXs[6].transform.position = monsterT.position;
        SFXs[6].SetActive(false);
        SFXs[6].SetActive(true);
    }
    public void PlayerSfx3()//스킬3
    {
        SFXs[3].SetActive(false);
        SFXs[3].SetActive(true);
    }
    public void PlayerSfx3OnMonster(Transform monsterT)//몬스터의 위치에 표시될 스킬
    {
        //SFXs[7].transform.position = monsterT.position;
        //SFXs[7].SetActive(false);
        //SFXs[7].SetActive(true);
        //몬스터 자체의 sfx 실행으로 변경
    }
    public void PlayerSfx4()//스킬4
    {
        SFXs[4].SetActive(false);
        SFXs[4].SetActive(true);
    }
    public void PlayerSfx4OnMonster(Transform monsterT)//몬스터의 위치에 표시될 스킬
    {
        SFXs[8].transform.position = monsterT.position;
        SFXs[8].SetActive(false);
        SFXs[8].SetActive(true);
    }
    /*
    public void PlayerDie()//빈사상태의 효과 표시
    {
        SFXs[5].SetActive(false);
        SFXs[5].SetActive(true);
    }
    */
}
