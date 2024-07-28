using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour
{
    public StoryScriptable story;

    void Update()
    {
        if (!story.Stage2Check4)//리시아와 합류전 노인과 대화
        {
            GetComponent<ObjectId>().ID = 26000;
        }
        if(story.Stage2Extra4)//바람정령을 되찾은뒤 노인과 대화
        {
            GetComponent<ObjectId>().ID = 27000;
        }
        if(story.Stage2Check4&&!story.isStage2Completed)//리시아와 합류후 노인과 첫 대화
        {
            GetComponent<ObjectId>().ID = 20000;
        }
        if(story.Stage2Extra3&&!story.Stage2Extra4)//바람정령을 되찾은 직후 노인과 대화
        {
            GetComponent<ObjectId>().ID = 24000;
        }
    }
}
