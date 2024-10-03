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
        //몬스터 자체의 sfx 실행으로 변경 (몬스터의 크기에 효과 맞춤) >> targetMob.playerSFX[0] 활성화.
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
    public void PlayerSfx4_starting(int i,Transform prj)//스킬4의 시전중 작은 번개, 이동 중인 스킬 투사체의 위치에 스킬 표시
    {
        switch (i)
        {
            case 0:
                SFXs[9].transform.position = prj.position;
                SFXs[9].SetActive(false);
                SFXs[9].SetActive(true);
                break;
            case 1:
                SFXs[10].transform.position = prj.position;
                SFXs[10].SetActive(false);
                SFXs[10].SetActive(true);
                break;
            case 2:
                SFXs[11].transform.position = prj.position;
                SFXs[11].SetActive(false);
                SFXs[11].SetActive(true);
                break;
        }
    }


    /*
    public void PlayerDie()//빈사상태의 효과 표시
    {
        SFXs[5].SetActive(false);
        SFXs[5].SetActive(true);
    }
    */
}
