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
    //스킬 사용시 알람창을 띄우는 기능을 하는 스크립트.
    //플레이어의 스킬 알람창과 몬스터의 스킬 알람창을 구분하여 사용.
    //행동의 완전한 취소를 위해, Dotween.Kill과 uniTask의 CancellationTokenSource를 사용.

    /*
    1. alarm은 먼저, 위치를 최초의 위치(안보이는곳에 숨어있는 상태의 처음 상태) 로 위치값 입력 후
    2. alarm의 skill이름과skill설명 텍스트를 넣어주고 + dotween으로 샥! 하며 등장.
    3. 등장한 뒤 일정 시간(2초) 후 원래 위치로 빠르게 돌아감 

    만약 애니메이션이 끝나기 전 다른 스킬 사용시, 아래와 같이 처리
    a) 일정 시간이 지나기 전 다른 스킬 사용시, 1,2번의 행동을 다시 시전
    b) 돌아가는 도중에 다른 스킬 사용시, 다 돌아간 뒤에 1,2번의 행동을 시전.
    */

    Vector3 originalPos;
    private CancellationTokenSource alarmCancellationTokenSource;
    [SerializeField]
    bool isPlayer;//플레이어 스킬 알람창인지, 몬스터 스킬 알람창인지 구분
    bool isAnimating;//알람이 나오고 있거나, 등장중일경우.
    bool isReturning;//알람이 돌아가고 있는 상태.

    public TextMeshProUGUI skillName;//스킬 이름
    public TextMeshProUGUI skillDesc;//스킬 설명

    void Awake()
    {
        originalPos = GetComponent<RectTransform>().localPosition;
    }

    private void OnEnable()
    {
        PlaceReset();//위치 초기화
    }

    public void PlaceReset()
    {
        //최초의 위치로 세팅
        GetComponent<RectTransform>().localPosition = originalPos;
    }
    public void SkillAlarmShow(string SName, string SDesc)//스킬 사용시 알람을 표시하는 기능
    {
        RectTransform ui = GetComponent<RectTransform>();

        //이전 작업 존재시, 취소
        alarmCancellationTokenSource?.Cancel();
        alarmCancellationTokenSource = new CancellationTokenSource();

        if (isReturning)//2초가 지나고 돌아가는 중에 다른 스킬을 사용시, 현재 표시중인 알람이 완전히 돌아간 뒤에 스킬 알람을 출력.
        {
            StartCoroutine(WaitForAllAnimationsToComplete(() =>
            {
                SkillAlarmShow(SName, SDesc);
            }));
            return;
        }
        if (isAnimating)//알람이 등장중일때(2초가 지나기 전) 다른 스킬 사용시, 현재 표시중인 알람을 취소하고 새로운 알람을 출력.
        {
            DOTween.Kill(ui);//적용중인 애니메이션 취소.
            PlaceReset();
            isAnimating = false;
            //AlarmRecallBeforeAnim();//2초가 지나기전 다른 스킬 알람 출력.
            SkillAlarmShow(SName, SDesc);//재귀호출
            return;
        }

        //최초의 위치로 세팅
         PlaceReset();
        //텍스트 입력 + dotween으로 슥! 하며 등장.
        skillName.text = SName;
        skillDesc.text = SDesc;

        isAnimating = true;
        isReturning = false;

        float endPosX = isPlayer ? -820 : 820;
        float returnPosX = isPlayer ? -1970 : 1970;
        //알람 등장
            ui.DOAnchorPosX(endPosX, 0.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(async () => 
                {
                    try
                    {
                        // 지정한 CancellationToken을 전달하여 UniTask를 대기
                        await UniTask.Delay(2000, cancellationToken: alarmCancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        // 취소될 경우, 리턴
                        return;
                    }
                    //등장한 후 2초뒤 원래 위치로 돌아감
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
    public void FinishAlarm()//전투가 종료 되었을때 즉시 등장하는 알람을 종료 시키는 함수.
    {
        // 현재 실행 중인 UniTask를 취소
        alarmCancellationTokenSource?.Cancel();

        RectTransform ui = GetComponent<RectTransform>();
        DOTween.Kill(ui);
        isAnimating = false;
        isReturning = false;
        float returnPosX = isPlayer ? -1970 : 1970;

        //알람 종료
        ui.DOAnchorPosX(returnPosX, 0.5f)
                    .SetEase(Ease.InQuad);

    }
    private IEnumerator WaitForAllAnimationsToComplete(System.Action onComplete)
    {
        // DOTween의 모든 작업이 완료될 때까지 대기
        while (DOTween.IsTweening(transform))
        {
            yield return null;
        }

        isAnimating = false; // 플래그 초기화
        onComplete?.Invoke(); // 모든 애니메이션 완료 후 onComplete 호출
    }
}
