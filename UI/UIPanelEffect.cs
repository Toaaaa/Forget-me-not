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
        // transform �� scale ���� ��� 0.1f�� �����մϴ�.
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

        // DOTween �Լ��� ���ʴ�� �����ϰ� ���ݴϴ�.
        var seq = DOTween.Sequence();

        // DOScale �� ù ��° �Ķ���ʹ� ��ǥ Scale ��, �� ��°�� �ð��Դϴ�.
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

        // ���⼭�� �ݱ� �ִϸ��̼��� �Ϸ�� �� ��ü�� ��Ȱ��ȭ �մϴ�.
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
        this.GetComponentInChildren<TextMeshProUGUI>().text = "��Ż�� ����Ͻðڽ��ϱ�?";
    }
}
