using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStoryWhenStart : MonoBehaviour
{
    public int storyIndex;
    public GameManager gameManager;
    public bool isAutoPlay;
    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager.storyScriptable.isScript)
        {
            switch (storyIndex)
            {
                case 1000:
                    if (!gameManager.storyScriptable.firstTime)
                    {
                        SceneChangeManager.Instance.Fade_img.alpha = 1;//첫 장면은 검정색화면에서
                        SceneChangeManager.Instance.OnBlackOutFinCust(2f);//천천히 암전이 풀림.
                        StartCoroutine(AutoStoryTime(1000, 4.0f));
                    }
                    break;
                case 2000:
                    if (!gameManager.storyScriptable.secondTime)
                    {
                        //gameManager.Player.CAT.SetActive(true);//고양이 켜주기.(고양이는 Onenable시 fadein 이미지가 켜지는 함수 보유)
                        StartCoroutine(AutoStoryTime(2000, 2.0f));
                    }
                    break;
                case 5000:
                    if (!gameManager.storyScriptable.isOnStage1)
                    {
                        StartCoroutine(AutoStoryTime(5000, 2.0f));
                        gameManager.storyScriptable.isOnStage1 = true;
                    }
                    break;
                case 8000:
                    if (!gameManager.storyScriptable.Stage1BossCompleted)
                    {
                        StartCoroutine(AutoStoryTime(8000, 2.0f));
                        gameManager.storyScriptable.Stage1BossCompleted = true;
                    }
                    break;
            }
        }
    }

    IEnumerator AutoStoryTime(int storyIndex,float waittime)
    {
        gameManager.isTalk = true;
        yield return new WaitForSeconds(waittime);
        gameManager.cantAction = false;
        isAutoPlay = true;
    }
    private void Update()
    {
        if (isAutoPlay)
        {
            gameManager.textManager.storyScriptPlay(storyIndex);
        }
    }
}
