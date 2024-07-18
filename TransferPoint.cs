using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //연결된 맵 이름
    public bool isDown; //길이 2개 일때 아래쪽과 연결된 곳인지? //하나만 있을 경우 up.
    public int mapBuilding;//맵 내부의 빌딩과의 연결일 경우 0이 아닌 번호를 입력


    private void Start()
    {
        if (linkedMapName == Player.Instance.currentMapName) //이동전 받은 맵 이름이 transferMapName과 같다면
        {
            //빌딩 번호가 0이 아닌 경우 아래의 스크립트 사용 (맵 내부의 건물간의 이동시)
            if (mapBuilding > 0)
            {
                if(Player.Instance.buildingNum == mapBuilding)//빌딩 번호가 동일할 경우
                {
                    Player.Instance.transform.position = this.transform.position; //해당 포탈의 위치에 플레이어 이동시켜주기
                    return;
                }
                else
               {
                   return;
               }
            }
        }   


        //빌딩 번호가 0일 경우 아래의 스크립트 사용
        if (linkedMapName == Player.Instance.currentMapName) //이동전 받은 맵 이름이 transferMapName과 같다면
        {
            if(Player.Instance.isDown)
            {
                if (isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //해당 포탈의 위치에 플레이어 이동시켜주기
                    return;
                }    
                else
                {
                    throw new System.Exception("플레이어의 isDown과 포탈의 isDown이 다릅니다.");
                }
            }
            else //플레이어의 isdown이 false 일 경우
            {
                if(!isDown)
                {
                    Player.Instance.transform.position = this.transform.position; //해당 포탈의 위치에 플레이어 이동시켜주기
                    return;
                }
                else
                {
                    throw new System.Exception("플레이어의 isDown과 포탈의 isDown이 다릅니다.");
                }
            }
            
        }
    }
}