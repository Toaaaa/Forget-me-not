using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnlockEffect : MonoBehaviour
{
    [SerializeField] private float bounceHeight = 130f; // ������ ����
    [SerializeField] private float duration = 0.5f; // �ִϸ��̼� ���� �ð�
    [SerializeField] private int loopCount = -1; // �ݺ� Ƚ��, -1�̸� ���� �ݺ�
    Vector3 startPosition;

    void OnEnable()
    {
        // �ʱ� ��ġ ����
        startPosition = transform.localPosition;

        // ���� �����̴� �ִϸ��̼� ����
        transform.DOLocalMoveY(startPosition.y + bounceHeight, duration)
            .SetEase(Ease.InOutSine) // ��¡ ����
            .SetLoops(loopCount, LoopType.Yoyo) // �ݺ� ����, Yoyo�� ���Ʒ��� �ݺ�
            .SetRelative(); // ������� ��ġ �̵�
    }

    void OnDisable()
    {
        // ��Ȱ��ȭ�� �� �ִϸ��̼��� �����ϰ� ���� ��ġ�� �ǵ���
        transform.DOKill();
        transform.localPosition = startPosition;
    }
}
