using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDinner : MonoBehaviour
{
    public StoryScriptable story;
    // Update is called once per frame
    void Update()
    {

        if(story.Stage2Check1 && !story.Stage2Check2)//식당을 간다고 한뒤 바로 식당으로 따라 가서 말을 걸 경우.
        {
            GetComponent<ObjectId>().tempID = 25000;
        }
        if(story.Stage2Check2 && !story.Stage2Check4)//식당에서 말을 건 후에 다시 말을 걸 경우.
        {
            GetComponent<ObjectId>().tempID = 14000;
        }
        if(story.Stage2Check4)//식당에서 말을 걸어 다시 합류함.
        {
            this.gameObject.SetActive(false);
        }
    }
}
