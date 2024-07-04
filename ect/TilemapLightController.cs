using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapLightController : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 컴포넌트를 연결합니다.
    public Color brightColor; // 밝을 때의 색상 >>알파값 0로 설정//예시값 a =90정도
    public Color darkColor; // 어두울 때의 색상 >>어두운 정도의 색상 설정//예시값 a =220정도

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            tilemap.color = brightColor; // 플레이어가 구간에 들어오면 밝게 설정
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            tilemap.color = darkColor; // 플레이어가 구간에서 나가면 어둡게 설정
        }
    }
}
