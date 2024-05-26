using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransfer : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름 
    public bool isDown; //길이 2개 일때 아래쪽과 연결된 곳인지? //하나만 있을 경우 false.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //이동전 맵이름 받아주기
            Player.Instance.isDown = isDown;
            SceneChangeManager.Instance.transferMapName = transferMapName;
            SceneChangeManager.Instance.ChangeScene();
        }
    }
}
