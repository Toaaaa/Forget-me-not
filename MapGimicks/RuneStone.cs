using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RuneStone : MonoBehaviour //스테이지 2에서 드래곤 봉인을 담당하는 runestone.
{
    [SerializeField]
    Light2D light2D;
    [SerializeField]
    float duration = 1.5f;
    [SerializeField]
    float maxIntensity = 5f;
    [SerializeField]
    float minIntensity = 2f;

    public StoryScriptable storyScriptable;
    public GameObject explosionFX;
    public GameObject stoneDragon;

    private void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponentInChildren<Light2D>();
        }
        if (storyScriptable.Stage2Check7)//드래곤 봉인이 해제되면 빛이 꺼지고 게임오브젝트가 비활성화 됨.
        {
            this.gameObject.SetActive(false);
        }
        AnimateLightIntensity().Forget();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            DistoryRune();
        }
        if(storyScriptable.Stage2Check7Dragon)
        {
            stoneDragon.SetActive(false);
        }
    }
    public async void DistoryRune()//봉인을 해제할때 재생되는 효과. +스토리 체크포인트 적용 + 게임오브젝트 비활성화 (터지는 효과는 개별 오브젝트로 외부에 존재)
    {
        explosionFX.SetActive(true);
        storyScriptable.Stage2Check7 = true;
        await RuneDisable();
    }

    private async UniTask RuneDisable()
    {
        await UniTask.Delay(1000);
        this.gameObject.SetActive(false);
        //2.5초뒤 전투입장 하는 스크립트 실행.
    }

    private async UniTask AnimateLightIntensity() //빛의 세기를 3.5 에서 5사이로 깜빡이는 함수.
    {
        float elapsedTime = 0f;
        bool increasing = true;

        while (true)
        {
            if(light2D.gameObject.activeSelf == false)
            {
                break;
            }//게임오브젝트가 비활성화 되면 루프를 빠져나옴.
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                if (increasing)
                {
                    light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
                }
                else
                {
                    light2D.intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
                }

                elapsedTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            // 역전환
            elapsedTime = 0f;
            increasing = !increasing;
        }
    }
}
