using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnlockEffect : MonoBehaviour
{
    [SerializeField] private float bounceHeight = 130f; // 움직일 높이
    [SerializeField] private float duration = 0.5f; // 애니메이션 지속 시간
    [SerializeField] private int loopCount = -1; // 반복 횟수, -1이면 무한 반복
    Vector3 startPosition;

    void OnEnable()
    {
        // 초기 위치 저장
        startPosition = transform.localPosition;

        // 위로 움직이는 애니메이션 설정
        transform.DOLocalMoveY(startPosition.y + bounceHeight, duration)
            .SetEase(Ease.InOutSine) // 이징 설정
            .SetLoops(loopCount, LoopType.Yoyo) // 반복 설정, Yoyo는 위아래로 반복
            .SetRelative(); // 상대적인 위치 이동
    }

    void OnDisable()
    {
        // 비활성화될 때 애니메이션을 중지하고 원래 위치로 되돌림
        transform.DOKill();
        transform.localPosition = startPosition;
    }
}
