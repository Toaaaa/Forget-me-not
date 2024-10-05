using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePrint : MonoBehaviour
{
    private void OnEnable()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 TargetPos = rectTransform.anchoredPosition + new Vector2(0, 40);

        DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, TargetPos, 1f).SetEase(Ease.Linear);
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
