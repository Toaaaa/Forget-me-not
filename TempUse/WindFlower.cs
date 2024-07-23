using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFlower : MonoBehaviour
{
    public bool isThisCliff; //절벽에 있는 꽃인지(true), 바람나무에 있는 꽃인지(false)
    public StoryScriptable story;

    private void Update()
    {
        if (isThisCliff)//절벽의 꽃의 경우
        {
            if (story.Stage2Extra2)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else//바람나무의 꽃의 경우
        {
            if (story.Stage2Extra3)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
