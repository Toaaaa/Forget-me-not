using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour
{
    public StoryScriptable story;

    void Update()
    {
        if (!story.Stage2Check4)//���þƿ� �շ��� ���ΰ� ��ȭ
        {
            GetComponent<ObjectId>().ID = 26000;
        }
        if(story.Stage2Extra4)//�ٶ������� ��ã���� ���ΰ� ��ȭ
        {
            GetComponent<ObjectId>().ID = 27000;
        }
        if(story.Stage2Check4&&!story.isStage2Completed)//���þƿ� �շ��� ���ΰ� ù ��ȭ
        {
            GetComponent<ObjectId>().ID = 20000;
        }
        if(story.Stage2Extra3&&!story.Stage2Extra4)//�ٶ������� ��ã�� ���� ���ΰ� ��ȭ
        {
            GetComponent<ObjectId>().ID = 24000;
        }
    }
}
