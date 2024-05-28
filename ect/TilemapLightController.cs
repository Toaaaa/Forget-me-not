using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapLightController : MonoBehaviour
{
    public Tilemap tilemap; // Ÿ�ϸ� ������Ʈ�� �����մϴ�.
    public Color brightColor; // ���� ���� ���� >>���İ� 0�� ����
    public Color darkColor; // ��ο� ���� ���� >>��ο� ������ ���� ����

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            tilemap.color = brightColor; // �÷��̾ ������ ������ ��� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            tilemap.color = darkColor; // �÷��̾ �������� ������ ��Ӱ� ����
        }
    }
}
