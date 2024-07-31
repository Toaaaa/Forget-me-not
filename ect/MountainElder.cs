using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainElder : MonoBehaviour
{
    public StoryScriptable story;
    // Update is called once per frame
    void Update()
    {
        if(story.Stage2Check1 && !story.Stage2Check2)//장로에게 처음 말을 걸기 전
        {
            GetComponent<ObjectId>().ID = 12000;
        }
        if(story.Stage2Check7 && !story.Stage2Check8)//드래곤을 처치후 장로에게 말을 걸면
        {
            GetComponent<ObjectId>().ID = 18000;
        }
        if(story.Stage2Check8)//전부 다 끝난 뒤 다시 장로에게 말을 걸 경우
        {
            GetComponent<ObjectId>().ID = 31000;
        }
    }
}
