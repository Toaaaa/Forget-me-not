using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFlower : MonoBehaviour
{
    public StoryScriptable story;
    // Update is called once per frame
    void Update()
    {
        if (story.Stage2Extra2)
        {
            this.gameObject.SetActive(false);
        }
    }
}
