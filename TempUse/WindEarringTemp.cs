using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEarringTemp : MonoBehaviour
{
    StoryScriptable story;
    // Start is called before the first frame update
    void Start()
    {
        story = GetComponent<ItemBox>().storyScriptable;
    }

    // Update is called once per frame
    void Update()
    {
        if(!story.stage2_windearring)
        {
            gameObject.SetActive(true);
        }
    }
}
