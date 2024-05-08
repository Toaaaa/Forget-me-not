using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferPoint : MonoBehaviour
{
    public string linkedMapName; //����� �� �̸�


    private void Start()
    {
        if (linkedMapName == Player.Instance.currentMapName) //�̵��� ���� �� �̸��� transferMapName�� ���ٸ�
        {
            Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�
            Camera.main.transform.position = new Vector3(this.GetComponentInParent<MapTransfer>().transform.position.x, this.GetComponentInParent<MapTransfer>().transform.position.y, Camera.main.transform.position.z); //ī�޶� �̵������ֱ�
            VirtualCamera.Instance.transform.position = new Vector3(this.GetComponentInParent<MapTransfer>().transform.position.x, this.GetComponentInParent<MapTransfer>().transform.position.y, VirtualCamera.Instance.transform.position.z); //ī�޶� �̵������ֱ�
            VirtualCamera.Instance.gameObject.SetActive(false);
            VirtualCamera.Instance.gameObject.SetActive(true);
        }
    }
}