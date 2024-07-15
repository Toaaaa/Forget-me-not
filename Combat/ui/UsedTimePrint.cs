using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedTimePrint : MonoBehaviour
{
    public Vector2 startForce = new Vector2(0.6f, 1f); // �ʱ� �� (������ ���� �߻�)
    public float duration = 0.6f; // ������ ���� �ð�


    private void OnEnable()
    {
        // �ʱ� ��ġ
        Vector3 startPosition = this.transform.position;

        // �߰� ��ǥ ��ġ (������ ���� �߻�)
        Vector3 midPoint = startPosition + new Vector3(startForce.x, startForce.y, 0);

        // ���� ��ġ (�Ʒ��� �������� ��ġ)
        Vector3 endPoint = startPosition + new Vector3(startForce.x * 2, 0, 0);

        // DOTween ������ ����
        Sequence sequence = DOTween.Sequence();

        // ������ ���� �߻� (�߰� ��ǥ ��ġ�� �̵�)
        sequence.Append(this.transform.DOMove(midPoint, duration - 0.1f).SetEase(Ease.OutQuad));

        // �Ʒ��� ������ (���� ��ġ�� �̵�)
        //sequence.Append(this.transform.DOMove(endPoint, duration / 2).SetEase(Ease.InQuad));
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
