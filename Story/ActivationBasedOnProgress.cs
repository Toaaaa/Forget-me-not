using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBasedOnProgress : MonoBehaviour
{
    //���丮�� ���� ��Ȳ GameManager.ssob, �� ���� Ȱ��ȭ/��Ȱ��ȭ�� ������ �� ��ũ��Ʈ.

    public int indexOfStoryObject; //�ش� ��ũ��Ʈ�� ����� ���丮 ������Ʈ�� �ε���.

    private void Awake()//�� ��湮�� ���� Ȯ��.
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
                    gameObject.SetActive(false);//������ �������� ��� false�� �渷�� ����. //������Ʈ�� ���ϴ� ���⿡ ���� ��������.
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
