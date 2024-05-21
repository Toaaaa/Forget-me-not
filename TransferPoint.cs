using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //연결된 맵 이름


    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Stage1-1")
            return;

        if (linkedMapName == Player.Instance.currentMapName) //이동전 받은 맵 이름이 transferMapName과 같다면
        {
            Player.Instance.transform.position = this.transform.position; //해당 포탈의 위치에 플레이어 이동시켜주기
            
        }
    }
}