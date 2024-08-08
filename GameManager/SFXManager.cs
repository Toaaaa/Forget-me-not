using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SFXManager : MonoBehaviour
    //��ų�� ����Ʈ������, ��ų�� ������ ��ġ��, +�¿�������
{
    public GameObject[] SFXInfo;
    public GameObject[] PlayerSFX;//�÷��̾��� ��ų ���� ǥ�õ� ����Ʈ��(����,����,����,����� ����)
    //0~7 ������ .8~
    public GameObject[] ItemBuff;//������ ���� �÷��̾��� ��ġ�� ǥ�õ� ���� ����Ʈ��
    public GameObject[] DebuffOnPlayer;//�÷��̾��� ��ġ�� ǥ�õ� ����� ����Ʈ��

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

    //////���� ��ų//////
    ///





    //////������ ��ų//////
    public void NormalAttack(Transform m_Position)//������ġ
    {
        PlayerSFX[7].transform.position = m_Position.position + new Vector3(2, 0, 0);
        PlayerSFX[7].SetActive(false);//����>>�� �� ���� ���
        PlayerSFX[7].SetActive(true);//��ų ����Ʈ
    }
    public void Blaze(Transform m_Position)//����ġ
    {
        PlayerSFX[0].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[0].SetActive(false);
        PlayerSFX[0].SetActive(true);//��ų ����Ʈ
    }
    public void DementionCrack(Transform m_Position)//����ġ
    {
        PlayerSFX[1].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[1].SetActive(false);
        PlayerSFX[1].SetActive(true);//��ų ����Ʈ
    }
    public async void TimeAsync(Transform m_Position)//����ġ
    {
        PlayerSFX[2].transform.position = m_Position.position + new Vector3(0, 3, 0);
        PlayerSFX[2].SetActive(false);
        PlayerSFX[2].SetActive(true);//�ð� ����Ʈ
        await UniTask.Delay(1000);
        PlayerSFX[3].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[3].SetActive(false);
        PlayerSFX[3].SetActive(true);//�ӵ����� ����Ʈ
    }
    public async void PiercingL(Transform m_Position)//����ġ
    {
        PlayerSFX[4].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[4].SetActive(false);
        PlayerSFX[4].SetActive(true);//��ų ����Ʈ
        PlayerSFX[4].transform.position = m_Position.position + new Vector3(0, 0, 0);
        ////
        PlayerSFX[5].transform.position = m_Position.position + new Vector3(0, 0.5f, 0);
        PlayerSFX[5].SetActive(false);
        PlayerSFX[5].SetActive(true);//��ų ����Ʈ
        PlayerSFX[5].transform.position = m_Position.position + new Vector3(0, 0.5f, 0);
        ////
        await UniTask.Delay(800);
        PlayerSFX[4].SetActive(false);
        PlayerSFX[5].SetActive(false);
        PlayerSFX[6].transform.position = m_Position.position + new Vector3(0, 0, 0);
        PlayerSFX[6].SetActive(false);
        PlayerSFX[6].SetActive(true);//��ų ����Ʈ
    }

}
