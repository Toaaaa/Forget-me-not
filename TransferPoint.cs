using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //����� �� �̸�
    public bool isDown; //���� 2�� �϶� �Ʒ��ʰ� ����� ������? //�ϳ��� ���� ��� up.
    public int mapBuilding;//�� ������ �������� ������ ��� 0�� �ƴ� ��ȣ�� �Է�


    private void Start()
    {
        if (linkedMapName == Player.Instance.currentMapName) //�̵��� ���� �� �̸��� transferMapName�� ���ٸ�
        {
            //���� ��ȣ�� 0�� �ƴ� ��� �Ʒ��� ��ũ��Ʈ ��� (�� ������ �ǹ����� �̵���)
            if (mapBuilding > 0)
            {
                if(Player.Instance.buildingNum == mapBuilding)//���� ��ȣ�� ������ ���
                {
                    Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
                    return;
                }
                else
               {
                   return;
               }
            }
        }   


        //���� ��ȣ�� 0�� ��� �Ʒ��� ��ũ��Ʈ ���
        if (linkedMapName == Player.Instance.currentMapName) //�̵��� ���� �� �̸��� transferMapName�� ���ٸ�
        {
            if(Player.Instance.isDown)
            {
                if (isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
                    return;
                }    
                else
                {
                    throw new System.Exception("�÷��̾��� isDown�� ��Ż�� isDown�� �ٸ��ϴ�.");
                }
            }
            else //�÷��̾��� isdown�� false �� ���
            {
                if(!isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
                    return;
                }
                else
                {
                    throw new System.Exception("�÷��̾��� isDown�� ��Ż�� isDown�� �ٸ��ϴ�.");
                }
            }
            
        }
    }
}