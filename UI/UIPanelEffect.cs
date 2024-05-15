using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPanelEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DOTween.Init();
        // transform 의 scale 값을 모두 0.1f로 변경합니다.
        transform.localScale = Vector3.one * 0.1f;
        SetText();
        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnActive();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        // DOTween 함수를 차례대로 수행하게 해줍니다.
        var seq = DOTween.Sequence();

        // DOScale 의 첫 번째 파라미터는 목표 Scale 값, 두 번째는 시간입니다.
        //seq.Append(transform.DOScale(1.1f, 0.2f));
        //seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play();
    }

    public void Hide()
    {
        var seq = DOTween.Sequence();

        transform.localScale = Vector3.one * 0.2f;

        //seq.Append(transform.DOScale(1.1f, 0.1f));
        //seq.Append(transform.DOScale(0.2f, 0.2f));

        // 여기서는 닫기 애니메이션이 완료된 후 객체를 비활성화 합니다.
        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }



    public void OnDeactive()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.95f, 0.15f));
        seq.Append(transform.DOScale(1.05f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.15f));

        seq.Play().OnComplete(() => {
            Hide();
        });
    }
    public void OnActive()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.95f, 0.15f));
        seq.Append(transform.DOScale(1.05f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.15f));

        seq.Play().OnComplete(() => {
            Show();
        });
    }

    void SetText()
    {
        this.GetComponentInChildren<TextMeshProUGUI>().text = "포탈을 사용하시겠습니까?";
    }
}
