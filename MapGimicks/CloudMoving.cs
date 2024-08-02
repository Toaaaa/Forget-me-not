using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMoving : MonoBehaviour
{
    public Vector3 startPosition;
    Vector3 endPosition;
    public int randomX;//start position에 추가하는 랜덤 변수의 X 범위값
    public int randomY;//start position에 추가하는 랜덤 변수의 Y 범위값
    public float maxLength;//최대 이동 범위
    public float moveSpeed;//구름 이동 속도

    private void Start()
    {
        startPosition.x += Random.Range(-randomX, randomX);
        startPosition.y += Random.Range(-randomY, randomY);
        endPosition = startPosition + Vector3.right * (maxLength+randomX);
        this.transform.position = startPosition;

        MoveRight();
    }

    private void MoveRight()
    {
        float duration = maxLength / moveSpeed;

        // 오른쪽으로 이동한 후 다시 시작 위치로 돌아오는 애니메이션
        transform.DOMove(endPosition, duration)
            .OnComplete(() =>
            {
                transform.position = startPosition;
                MoveRight(); // 다시 이동 시작
            })
            .SetEase(Ease.Linear);
    }
}
