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
    private float L_Time0;//기본은 0, 라이트를 켜기전 대기시간이 필요할 경우 사용.    
    [SerializeField]
    private float L_Time1;//번쩍 하는 시간 alpha가 0에서 255까지 가는 시간
    [SerializeField]
    private float L_Time2;//번쩍 하는 시간 alpha가 255에 유지되는 시간
    [SerializeField]
    private float L_Time3;//번쩍 하는 시간 alpha가 255에서 0까지 가는 시간

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
        
        await UniTask.Delay((int)(L_Time0 * 1000));//대기시간
        // 알파 값을 0에서 255로 증가시키기
        DOTween.To(() => lightColor.a, x => lightColor.a = x, 1f, L_Time1)
            .OnUpdate(() => light2D.color = lightColor)
            .OnComplete(() =>
            {
                // 알파 값을 255에서 유지하는 동안 (time2)
                DOVirtual.DelayedCall(L_Time2, () =>
                {
                    // 알파 값을 255에서 0으로 감소시키기
                    DOTween.To(() => lightColor.a, x => lightColor.a = x, 0f, L_Time3)
                        .OnUpdate(() => light2D.color = lightColor);
                });
                if(TwinckleLight)
                    TwinkleEffect(L_Time2);//빛이 L_Time2시간동안 번쩍번쩍 하는 효과.
            });
    }

    private void TwinkleEffect(float duration)
    {
        // 빛의 강약 효과를 구현할 반복 횟수 (duration 동안 발생)
        int twinkleCount = 4; // 이 값은 파직거림의 횟수로 조정 가능
        float twinkleDuration = duration / (twinkleCount * 2); // 강약 강약으로 왔다갔다하는 시간
        float OriginalIntensity = light2D.intensity;

        Sequence twinkleSequence = DOTween.Sequence();

        for (int i = 0; i < twinkleCount; i++)
        {
            twinkleSequence.Append(DOTween.To(() => light2D.intensity, x => light2D.intensity = x, OriginalIntensity*0.5f, twinkleDuration)) // 약하게
                .Append(DOTween.To(() => light2D.intensity, x => light2D.intensity = x, OriginalIntensity, twinkleDuration)); // 강하게
        }

        twinkleSequence.Play();
    }
}
