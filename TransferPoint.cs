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
            Camera.main.transform.position = new Vector3(this.GetComponentInParent<MapTransfer>().transform.position.x, this.GetComponentInParent<MapTransfer>().transform.position.y, Camera.main.transform.position.z); //카메라도 이동시켜주기
            VirtualCamera.Instance.transform.position = new Vector3(this.GetComponentInParent<MapTransfer>().transform.position.x, this.GetComponentInParent<MapTransfer>().transform.position.y, VirtualCamera.Instance.transform.position.z); //카메라도 이동시켜주기
            VirtualCamera.Instance.gameObject.SetActive(false);
            VirtualCamera.Instance.gameObject.SetActive(true);
        }
    }
}