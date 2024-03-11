using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransfer : MonoBehaviour
{
    public string transferMapName; //�̵��� ���� �̸� 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //�̵��� ���̸� �޾��ֱ�
            SceneManager.LoadScene(transferMapName);
        }
    }
}
