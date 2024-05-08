using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //연결된 맵 이름


    private void Start()
    {
        if (linkedMapName == Player.Instance.currentMapName) //이동전 받은 맵 이름이 transferMapName과 같다면
        {
            Player.Instance.transform.position = this.transform.position; //해당 포탈의 위치에 플레이어 이동시켜주기
            
        }
    }
}