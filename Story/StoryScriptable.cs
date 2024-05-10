using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryProgress", menuName = "StoryProgress")]
public class StoryScriptable : ScriptableObject
{
    public bool isScript;// if(!isScript)�� �ؼ� �ٸ� ���� �׽�Ʈ ���϶� �ش�ʿ� ��ũ��Ʈ�� ���� ���� �ʰ� �ϱ�.
    //��ũ��Ʈ������ �׽�Ʈ �� ���� true, �ٸ� ����� �׽�Ʈ �� ���� false.
    public bool firstTime;//���� ó�� �ʿ��� ������ ��. //ó�� ������ false�� ��ũ��Ʈ�� ������ true�� �ٲ�.
    public bool secondTime;//�ι�° ���� ó�� �� ��. //ó�� ������ false�� ��ũ��Ʈ�� ������ true�� �ٲ�.
    public bool second_map1;
    public bool second_map2;
    public bool second_map3;//�ι�° �ʿ����� �ֺ� �ʵ� Ž���Ϸ� ����. //Ž���� ���� true�� �ι�° ������ ���ƿ��� �� ���� ��ũ��Ʈ�� �Ѿ.
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
}
