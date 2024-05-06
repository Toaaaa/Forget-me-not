using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePrint : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOMove(transform.position + new Vector3(0, 30, 0), 1f).SetEase(Ease.Linear);
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
