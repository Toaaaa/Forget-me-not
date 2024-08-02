using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMoving : MonoBehaviour
{
    public Vector3 startPosition;
    Vector3 endPosition;
    public int randomX;//start position�� �߰��ϴ� ���� ������ X ������
    public int randomY;//start position�� �߰��ϴ� ���� ������ Y ������
    public float maxLength;//�ִ� �̵� ����
    public float moveSpeed;//���� �̵� �ӵ�

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

        // ���������� �̵��� �� �ٽ� ���� ��ġ�� ���ƿ��� �ִϸ��̼�
        transform.DOMove(endPosition, duration)
            .OnComplete(() =>
            {
                transform.position = startPosition;
                MoveRight(); // �ٽ� �̵� ����
            })
            .SetEase(Ease.Linear);
    }
}
