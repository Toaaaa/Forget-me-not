using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //����� �� �̸�


    private void Start()
    {
        if (linkedMapName == Player.Instance.currentMapName) //�̵��� ���� �� �̸��� transferMapName�� ���ٸ�
        {
            Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
            
        }
    }
}