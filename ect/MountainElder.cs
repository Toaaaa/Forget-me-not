using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainElder : MonoBehaviour
{
    public StoryScriptable story;
    // Update is called once per frame
    void Update()
    {
        if(story.Stage2Check1 && !story.Stage2Check2)//��ο��� ó�� ���� �ɱ� ��
        {
            GetComponent<ObjectId>().ID = 12000;
        }
        if(story.Stage2Check7 && !story.Stage2Check8)//�巡���� óġ�� ��ο��� ���� �ɸ�
        {
            GetComponent<ObjectId>().ID = 18000;
        }
        if(story.Stage2Check8)//���� �� ���� �� �ٽ� ��ο��� ���� �� ���
        {
            GetComponent<ObjectId>().ID = 31000;
        }
    }
}
