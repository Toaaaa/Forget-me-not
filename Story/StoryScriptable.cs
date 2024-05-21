using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryProgress", menuName = "StoryProgress")]
public class StoryScriptable : ScriptableObject
{
    public List<bool> storyScriptable;

    public bool isScript;// if(!isScript)�� �ؼ� �ٸ� ���� �׽�Ʈ ���϶� �ش�ʿ� ��ũ��Ʈ�� ���� ���� �ʰ� �ϱ�.
    //��ũ��Ʈ������ �׽�Ʈ �� ���� true, �ٸ� ����� �׽�Ʈ �� ���� false.
    public bool firstTime;//���� ó�� �ʿ��� ������ ��. //ó�� ������ false�� ��ũ��Ʈ�� ������ true�� �ٲ�.
    public bool secondTime;//�ι�° ���� ó�� �� ��. //ó�� ������ false�� ��ũ��Ʈ�� ������ true�� �ٲ�.
    public bool second_map1;
    public bool second_map2;//�ֺ� �ʵ� Ž���Ϸ� ����. //Ž���� ���� true�� �ι�° ������ ���ƿ��� �� ���� ��ũ��Ʈ�� �Ѿ.
    public bool isTutorial;
    public bool isTutorialCompleted;
    public bool isOnStage1;
    public bool isStage1Completed;
    public bool isOnStage2;
    public bool isStage2Completed;
    public bool isOnStage3;
    public bool isStage3Completed;
    public bool isOnStage4;
    public bool isStage4Completed;
    public bool isAllCompleted;

    public void restAll()
    {
        firstTime = false;
        secondTime = false;
        second_map1 = false;
        second_map2 = false;
        isTutorial = false;
        isTutorialCompleted = false;
        isOnStage1 = false;
        isStage1Completed = false;
        isOnStage2 = false;
        isStage2Completed = false;
        isOnStage3 = false;
        isStage3Completed = false;
        isOnStage4 = false;
        isStage4Completed = false;
        isAllCompleted = false;
    }

}
