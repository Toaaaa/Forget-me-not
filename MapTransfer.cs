using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransfer : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //이동전 맵이름 받아주기
            SceneManager.LoadScene(transferMapName);
        }
    }
}
