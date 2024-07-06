using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RuneStone : MonoBehaviour //�������� 2���� �巡�� ������ ����ϴ� runestone.
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
        if (storyScriptable.Stage2Check7)//�巡�� ������ �����Ǹ� ���� ������ ���ӿ�����Ʈ�� ��Ȱ��ȭ ��.
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
    public async void DistoryRune()//������ �����Ҷ� ����Ǵ� ȿ��. +���丮 üũ����Ʈ ���� + ���ӿ�����Ʈ ��Ȱ��ȭ (������ ȿ���� ���� ������Ʈ�� �ܺο� ����)
    {
        explosionFX.SetActive(true);
        storyScriptable.Stage2Check7 = true;
        await RuneDisable();
    }

    private async UniTask RuneDisable()
    {
        await UniTask.Delay(1000);
        this.gameObject.SetActive(false);
        //2.5�ʵ� �������� �ϴ� ��ũ��Ʈ ����.
    }

    private async UniTask AnimateLightIntensity() //���� ���⸦ 3.5 ���� 5���̷� �����̴� �Լ�.
    {
        float elapsedTime = 0f;
        bool increasing = true;

        while (true)
        {
            if(light2D.gameObject.activeSelf == false)
            {
                break;
            }//���ӿ�����Ʈ�� ��Ȱ��ȭ �Ǹ� ������ ��������.
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
            // ����ȯ
            elapsedTime = 0f;
            increasing = !increasing;
        }
    }
}
