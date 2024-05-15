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
        switch(storyIndex)
        {
            case 1000:
                SceneChangeManager.Instance.Fade_img.alpha = 1;//ù ����� ������ȭ�鿡��
                if (!gameManager.storyScriptable.firstTime)
                {
                    SceneChangeManager.Instance.OnBlackOutFinCust(2f);//õõ�� ������ Ǯ��.
                    StartCoroutine(AutoStoryTime(1000, 4.0f));
                }
                break;
            case 2000:
                if (!gameManager.storyScriptable.secondTime)
                {
                    //gameManager.Player.CAT.SetActive(true);//����� ���ֱ�.(����̴� Onenable�� fadein �̹����� ������ �Լ� ����)
                    StartCoroutine(AutoStoryTime(2000, 2.0f));
                }
                break;

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
