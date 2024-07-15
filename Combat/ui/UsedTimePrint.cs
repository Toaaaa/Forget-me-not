using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedTimePrint : MonoBehaviour
{
    public Vector2 startForce = new Vector2(0.6f, 1f); // 초기 힘 (오른쪽 위로 발사)
    public float duration = 0.6f; // 움직임 지속 시간


    private void OnEnable()
    {
        // 초기 위치
        Vector3 startPosition = this.transform.position;

        // 중간 목표 위치 (오른쪽 위로 발사)
        Vector3 midPoint = startPosition + new Vector3(startForce.x, startForce.y, 0);

        // 최종 위치 (아래로 떨어지는 위치)
        Vector3 endPoint = startPosition + new Vector3(startForce.x * 2, 0, 0);

        // DOTween 시퀀스 생성
        Sequence sequence = DOTween.Sequence();

        // 오른쪽 위로 발사 (중간 목표 위치로 이동)
        sequence.Append(this.transform.DOMove(midPoint, duration - 0.1f).SetEase(Ease.OutQuad));

        // 아래로 떨어짐 (최종 위치로 이동)
        //sequence.Append(this.transform.DOMove(endPoint, duration / 2).SetEase(Ease.InQuad));
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
