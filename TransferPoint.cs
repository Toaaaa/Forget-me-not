using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //����� �� �̸�


    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Stage1-1")
            return;

        if (linkedMapName == Player.Instance.currentMapName) //�̵��� ���� �� �̸��� transferMapName�� ���ٸ�
        {
            Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
            
        }
    }
}