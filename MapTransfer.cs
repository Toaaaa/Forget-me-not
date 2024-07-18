using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransfer : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름 
    public bool isDown; //길이 2개 일때 아래쪽과 연결된 곳인지? //하나만 있을 경우 false.
    public int mapBuilding;//맵 내부의 빌딩과의 연결일 경우 0이 아닌 번호를 입력(이동할 곳의 번호)+0인 밖으로 나갈때도 해당 숫자는 고정.
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //이동전 맵이름 받아주기
            Player.Instance.isDown = isDown;
            Player.Instance.buildingNum = mapBuilding;
            SceneChangeManager.Instance.transferMapName = transferMapName;
            SceneChangeManager.Instance.ChangeScene();
        }
    }
}
//사용법은
//맵내부의 집 등으로 이동할 경우 서로 연결된 포탈은 mapbuilding 에 같은 숫자를 넣어주고
//transfermapname에는 전까지 하던대로 연결된 맵의 이름을 넣어주면 된다.
