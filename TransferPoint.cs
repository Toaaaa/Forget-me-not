using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //연결된 맵 이름
    public bool isDown; //길이 2개 일때 아래쪽과 연결된 곳인지? //하나만 있을 경우 up.
    //만약 


    private void Start()
    {

        if (linkedMapName == Player.Instance.currentMapName) //이동전 받은 맵 이름이 transferMapName과 같다면
        {
            if(Player.Instance.isDown)
            {
                if (isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //해당 포탈의 위치에 플레이어 이동시켜주기
                    return;
                }    
            }
            else //플레이어의 isdown이 false 일 경우
            {
                if(!isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //해당 포탈의 위치에 플레이어 이동시켜주기
                    return;
                }
            }
            
        }
    }
}