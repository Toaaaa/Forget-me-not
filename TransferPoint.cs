using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //����� �� �̸�
    public bool isDown; //���� 2�� �϶� �Ʒ��ʰ� ����� ������? //�ϳ��� ���� ��� up.
    //���� 


    private void Start()
    {

        if (linkedMapName == Player.Instance.currentMapName) //�̵��� ���� �� �̸��� transferMapName�� ���ٸ�
        {
            if(Player.Instance.isDown)
            {
                if (isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
                    return;
                }    
            }
            else //�÷��̾��� isdown�� false �� ���
            {
                if(!isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
                    return;
                }
            }
            
        }
    }
}