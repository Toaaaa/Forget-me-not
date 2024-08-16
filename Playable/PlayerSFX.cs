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
    public void PlayerSfx3()//스킬3
    {
        SFXs[3].SetActive(false);
        SFXs[3].SetActive(true);
    }
    public void PlayerSfx4()//스킬4
    {
        SFXs[4].SetActive(false);
        SFXs[4].SetActive(true);
    }
    /*
    public void PlayerDie()//빈사상태의 효과 표시
    {
        SFXs[5].SetActive(false);
        SFXs[5].SetActive(true);
    }
    */
}
