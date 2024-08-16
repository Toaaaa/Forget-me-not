using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class SFXLigthing : MonoBehaviour
{
    public Light2D light2D;
    [SerializeField]
    private float L_Time1;//��½ �ϴ� �ð� alpha�� 0���� 255���� ���� �ð�
    [SerializeField]
    private float L_Time2;//��½ �ϴ� �ð� alpha�� 255�� �����Ǵ� �ð�
    [SerializeField]
    private float L_Time3;//��½ �ϴ� �ð� alpha�� 255���� 0���� ���� �ð�

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }
    private void OnEnable()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>();
        Light();
    }

    private void Light()
    {
        Color lightColor = light2D.color;

        // ���� ���� 0���� 255�� ������Ű��
        DOTween.To(() => lightColor.a, x => lightColor.a = x, 1f, L_Time1)
            .OnUpdate(() => light2D.color = lightColor)
            .OnComplete(() =>
            {
                // ���� ���� 255���� �����ϴ� ���� (time2)
                DOVirtual.DelayedCall(L_Time2, () =>
                {
                    // ���� ���� 255���� 0���� ���ҽ�Ű��
                    DOTween.To(() => lightColor.a, x => lightColor.a = x, 0f, L_Time3)
                        .OnUpdate(() => light2D.color = lightColor);
                });
            });
    }
}
