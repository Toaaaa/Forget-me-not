using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBasedOnProgress : MonoBehaviour
{
    //스토리의 진행 상황 GameManager.ssob, 에 따라 활성화/비활성화를 조절해 줄 스크립트.

    public int indexOfStoryObject; //해당 스크립트가 적용될 스토리 오브젝트의 인덱스.

    private void Awake()//씬 재방문때 마다 확인.
    {
        CheckStory();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void CheckStory()
    {
        switch (indexOfStoryObject)
        {
            case 1:
                if (GameManager.Instance.storyScriptable.firstTime)
                {
                    gameObject.SetActive(false);//조건을 만족했을 경우 false로 길막기 해제. //오브젝트나 원하는 연출에 따라서 수정가능.
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 2:
                if (GameManager.Instance.storyScriptable.secondTime)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 3:
                if (GameManager.Instance.storyScriptable.isTutorial)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 4:
                if (GameManager.Instance.storyScriptable.isTutorialCompleted)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 5:
                if (GameManager.Instance.storyScriptable.isOnStage1)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 6:
                if (GameManager.Instance.storyScriptable.isStage1Completed)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 7:
                if (GameManager.Instance.storyScriptable.isOnStage2)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 8:
                if (GameManager.Instance.storyScriptable.isStage2Completed)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 9:
                if (GameManager.Instance.storyScriptable.isOnStage3)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 10:
                if (GameManager.Instance.storyScriptable.isStage3Completed)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 11:
                if (GameManager.Instance.storyScriptable.isOnStage4)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 12:
                if (GameManager.Instance.storyScriptable.isStage4Completed)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            case 13:
                if (GameManager.Instance.storyScriptable.isAllCompleted)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}
