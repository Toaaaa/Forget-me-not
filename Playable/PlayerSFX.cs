using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public GameObject[] SFXs; //�������� �տ��� ���� sfx

    public void PlayerSfx0()//�⺻���� 
    {
        SFXs[0].SetActive(false);
        SFXs[0].SetActive(true);
    }
    public void PlayerSfx1()//��ų1
    {
        SFXs[1].SetActive(false);
        SFXs[1].SetActive(true);
    }
    public void PlayerSfx2()//��ų2
    {
        SFXs[2].SetActive(false);
        SFXs[2].SetActive(true);
    }
    public void PlayerSfx3()//��ų3
    {
        SFXs[3].SetActive(false);
        SFXs[3].SetActive(true);
    }
    public void PlayerSfx4()//��ų4
    {
        SFXs[4].SetActive(false);
        SFXs[4].SetActive(true);
    }
    /*
    public void PlayerDie()//�������� ȿ�� ǥ��
    {
        SFXs[5].SetActive(false);
        SFXs[5].SetActive(true);
    }
    */
}
