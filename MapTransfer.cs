using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransfer : MonoBehaviour
{
    public string transferMapName; //�̵��� ���� �̸� 
    public bool isDown; //���� 2�� �϶� �Ʒ��ʰ� ����� ������? //�ϳ��� ���� ��� false.
    public int mapBuilding;//�� ������ �������� ������ ��� 0�� �ƴ� ��ȣ�� �Է�(�̵��� ���� ��ȣ)+0�� ������ �������� �ش� ���ڴ� ����.
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //�̵��� ���̸� �޾��ֱ�
            Player.Instance.isDown = isDown;
            Player.Instance.buildingNum = mapBuilding;
            SceneChangeManager.Instance.transferMapName = transferMapName;
            SceneChangeManager.Instance.ChangeScene();
        }
    }
}
//������
//�ʳ����� �� ������ �̵��� ��� ���� ����� ��Ż�� mapbuilding �� ���� ���ڸ� �־��ְ�
//transfermapname���� ������ �ϴ���� ����� ���� �̸��� �־��ָ� �ȴ�.
