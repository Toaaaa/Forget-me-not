using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDinner : MonoBehaviour
{
    public StoryScriptable story;
    // Update is called once per frame
    void Update()
    {

        if(story.Stage2Check1 && !story.Stage2Check2)//�Ĵ��� ���ٰ� �ѵ� �ٷ� �Ĵ����� ���� ���� ���� �� ���.
        {
            GetComponent<ObjectId>().tempID = 25000;
        }
        if(story.Stage2Check2 && !story.Stage2Check4)//�Ĵ翡�� ���� �� �Ŀ� �ٽ� ���� �� ���.
        {
            GetComponent<ObjectId>().tempID = 14000;
        }
        if(story.Stage2Check4)//�Ĵ翡�� ���� �ɾ� �ٽ� �շ���.
        {
            this.gameObject.SetActive(false);
        }
    }
}
