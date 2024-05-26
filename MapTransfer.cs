using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransfer : MonoBehaviour
{
    public string transferMapName; //�̵��� ���� �̸� 
    public bool isDown; //���� 2�� �϶� �Ʒ��ʰ� ����� ������? //�ϳ��� ���� ��� false.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //�̵��� ���̸� �޾��ֱ�
            Player.Instance.isDown = isDown;
            SceneChangeManager.Instance.transferMapName = transferMapName;
            SceneChangeManager.Instance.ChangeScene();
        }
    }
}
