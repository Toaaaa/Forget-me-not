using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SFXManager : MonoBehaviour
    //스킬의 이펙트정보와, 스킬이 생성될 위치값, +온오프관리
{
    public GameObject[] SFXInfo;
    public GameObject[] PlayerSFX;//플레이어의 스킬 사용시 표시될 이펙트들(공격,광역,버프,디버프 전부)
    //0~7 마법사 .8~
    public GameObject[] ItemBuff;//아이템 사용시 플레이어의 위치에 표시될 버프 이펙트들
    public GameObject[] DebuffOnPlayer;//플레이어의 위치에 표시될 디버프 이펙트들

    public void SetTheSFX()
    {
        PlayerSFX = new GameObject[SFXInfo.Length];
        for(int i = 0; i < SFXInfo.Length; i++)
        {
            var obj = Instantiate(SFXInfo[i]);
            obj.SetActive(false);
            PlayerSFX[i] = obj;
        }
    }

    //////전사 스킬//////
    ///





    //////마법사 스킬//////
    public void NormalAttack(Transform m_Position)//본인위치
    {
        PlayerSFX[7].transform.position = m_Position.position + new Vector3(2, 0, 0);
        PlayerSFX[7].SetActive(false);//오프>>온 을 통해 재생
        PlayerSFX[7].SetActive(true);//스킬 이펙트
    }
    public void Blaze(Transform m_Position)//적위치
    {
        PlayerSFX[0].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[0].SetActive(false);
        PlayerSFX[0].SetActive(true);//스킬 이펙트
    }
    public void DementionCrack(Transform m_Position)//적위치
    {
        PlayerSFX[1].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[1].SetActive(false);
        PlayerSFX[1].SetActive(true);//스킬 이펙트
    }
    public async void TimeAsync(Transform m_Position)//적위치
    {
        PlayerSFX[2].transform.position = m_Position.position + new Vector3(0, 3, 0);
        PlayerSFX[2].SetActive(false);
        PlayerSFX[2].SetActive(true);//시계 이펙트
        await UniTask.Delay(1000);
        PlayerSFX[3].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[3].SetActive(false);
        PlayerSFX[3].SetActive(true);//속도감소 이펙트
    }
    public async void PiercingL(Transform m_Position)//적위치
    {
        PlayerSFX[4].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[4].SetActive(false);
        PlayerSFX[4].SetActive(true);//스킬 이펙트
        PlayerSFX[4].transform.position = m_Position.position + new Vector3(0, 0, 0);
        ////
        PlayerSFX[5].transform.position = m_Position.position + new Vector3(0, 0.5f, 0);
        PlayerSFX[5].SetActive(false);
        PlayerSFX[5].SetActive(true);//스킬 이펙트
        PlayerSFX[5].transform.position = m_Position.position + new Vector3(0, 0.5f, 0);
        ////
        await UniTask.Delay(800);
        PlayerSFX[4].SetActive(false);
        PlayerSFX[5].SetActive(false);
        PlayerSFX[6].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[6].SetActive(false);
        PlayerSFX[6].SetActive(true);//스킬 이펙트
    }

}
