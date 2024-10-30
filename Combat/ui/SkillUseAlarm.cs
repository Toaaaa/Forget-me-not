using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using System;

public class SkillUseAlarm : MonoBehaviour
{
    //��ų ���� �˶�â�� ���� ����� �ϴ� ��ũ��Ʈ.
    //�÷��̾��� ��ų �˶�â�� ������ ��ų �˶�â�� �����Ͽ� ���.
    //�ൿ�� ������ ��Ҹ� ����, Dotween.Kill�� uniTask�� CancellationTokenSource�� ���.

    /*
    1. alarm�� ����, ��ġ�� ������ ��ġ(�Ⱥ��̴°��� �����ִ� ������ ó�� ����) �� ��ġ�� �Է� ��
    2. alarm�� skill�̸���skill���� �ؽ�Ʈ�� �־��ְ� + dotween���� ��! �ϸ� ����.
    3. ������ �� ���� �ð�(2��) �� ���� ��ġ�� ������ ���ư� 

    ���� �ִϸ��̼��� ������ �� �ٸ� ��ų ����, �Ʒ��� ���� ó��
    a) ���� �ð��� ������ �� �ٸ� ��ų ����, 1,2���� �ൿ�� �ٽ� ����
    b) ���ư��� ���߿� �ٸ� ��ų ����, �� ���ư� �ڿ� 1,2���� �ൿ�� ����.
    */

    Vector3 originalPos;
    private CancellationTokenSource alarmCancellationTokenSource;
    [SerializeField]
    bool isPlayer;//�÷��̾� ��ų �˶�â����, ���� ��ų �˶�â���� ����
    bool isAnimating;//�˶��� ������ �ְų�, �������ϰ��.
    bool isReturning;//�˶��� ���ư��� �ִ� ����.

    public TextMeshProUGUI skillName;//��ų �̸�
    public TextMeshProUGUI skillDesc;//��ų ����

    void Awake()
    {
        originalPos = GetComponent<RectTransform>().localPosition;
    }

    private void OnEnable()
    {
        PlaceReset();//��ġ �ʱ�ȭ
    }

    public void PlaceReset()
    {
        //������ ��ġ�� ����
        GetComponent<RectTransform>().localPosition = originalPos;
    }
    public void SkillAlarmShow(string SName, string SDesc)//��ų ���� �˶��� ǥ���ϴ� ���
    {
        RectTransform ui = GetComponent<RectTransform>();

        //���� �۾� �����, ���
        alarmCancellationTokenSource?.Cancel();
        alarmCancellationTokenSource = new CancellationTokenSource();

        if (isReturning)//2�ʰ� ������ ���ư��� �߿� �ٸ� ��ų�� ����, ���� ǥ������ �˶��� ������ ���ư� �ڿ� ��ų �˶��� ���.
        {
            StartCoroutine(WaitForAllAnimationsToComplete(() =>
            {
                SkillAlarmShow(SName, SDesc);
            }));
            return;
        }
        if (isAnimating)//�˶��� �������϶�(2�ʰ� ������ ��) �ٸ� ��ų ����, ���� ǥ������ �˶��� ����ϰ� ���ο� �˶��� ���.
        {
            DOTween.Kill(ui);//�������� �ִϸ��̼� ���.
            PlaceReset();
            isAnimating = false;
            //AlarmRecallBeforeAnim();//2�ʰ� �������� �ٸ� ��ų �˶� ���.
            SkillAlarmShow(SName, SDesc);//���ȣ��
            return;
        }

        //������ ��ġ�� ����
         PlaceReset();
        //�ؽ�Ʈ �Է� + dotween���� ��! �ϸ� ����.
        skillName.text = SName;
        skillDesc.text = SDesc;

        isAnimating = true;
        isReturning = false;

        float endPosX = isPlayer ? -820 : 820;
        float returnPosX = isPlayer ? -1970 : 1970;
        //�˶� ����
            ui.DOAnchorPosX(endPosX, 0.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(async () => 
                {
                    try
                    {
                        // ������ CancellationToken�� �����Ͽ� UniTask�� ���
                        await UniTask.Delay(2000, cancellationToken: alarmCancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        // ��ҵ� ���, ����
                        return;
                    }
                    //������ �� 2�ʵ� ���� ��ġ�� ���ư�
                    isAnimating = false;
                    isReturning = true;
                    ui.DOAnchorPosX(returnPosX, 0.5f)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        isReturning = false;
                        PlaceReset();
                    });
                });
        
    }
    public void FinishAlarm()//������ ���� �Ǿ����� ��� �����ϴ� �˶��� ���� ��Ű�� �Լ�.
    {
        // ���� ���� ���� UniTask�� ���
        alarmCancellationTokenSource?.Cancel();

        RectTransform ui = GetComponent<RectTransform>();
        DOTween.Kill(ui);
        isAnimating = false;
        isReturning = false;
        float returnPosX = isPlayer ? -1970 : 1970;

        //�˶� ����
        ui.DOAnchorPosX(returnPosX, 0.5f)
                    .SetEase(Ease.InQuad);

    }
    private IEnumerator WaitForAllAnimationsToComplete(System.Action onComplete)
    {
        // DOTween�� ��� �۾��� �Ϸ�� ������ ���
        while (DOTween.IsTweening(transform))
        {
            yield return null;
        }

        isAnimating = false; // �÷��� �ʱ�ȭ
        onComplete?.Invoke(); // ��� �ִϸ��̼� �Ϸ� �� onComplete ȣ��
    }
}
