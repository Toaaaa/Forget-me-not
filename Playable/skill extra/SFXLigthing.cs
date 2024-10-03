using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SFXLigthing : MonoBehaviour
{
    public Light2D light2D;
    public bool TwinckleLight;

    [SerializeField]
    private float L_Time0;//�⺻�� 0, ����Ʈ�� �ѱ��� ���ð��� �ʿ��� ��� ���.    
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
        Light().Forget();
    }

    private async UniTask Light()
    {
        Color lightColor = light2D.color;
        
        await UniTask.Delay((int)(L_Time0 * 1000));//���ð�
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
                if(TwinckleLight)
                    TwinkleEffect(L_Time2);//���� L_Time2�ð����� ��½��½ �ϴ� ȿ��.
            });
    }

    private void TwinkleEffect(float duration)
    {
        // ���� ���� ȿ���� ������ �ݺ� Ƚ�� (duration ���� �߻�)
        int twinkleCount = 4; // �� ���� �����Ÿ��� Ƚ���� ���� ����
        float twinkleDuration = duration / (twinkleCount * 2); // ���� �������� �Դٰ����ϴ� �ð�
        float OriginalIntensity = light2D.intensity;

        Sequence twinkleSequence = DOTween.Sequence();

        for (int i = 0; i < twinkleCount; i++)
        {
            twinkleSequence.Append(DOTween.To(() => light2D.intensity, x => light2D.intensity = x, OriginalIntensity*0.5f, twinkleDuration)) // ���ϰ�
                .Append(DOTween.To(() => light2D.intensity, x => light2D.intensity = x, OriginalIntensity, twinkleDuration)); // ���ϰ�
        }

        twinkleSequence.Play();
    }
}
