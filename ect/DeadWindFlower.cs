using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadWindFlower : MonoBehaviour
{
    public StoryScriptable story;
    public GameObject windFlower;//��ü�Ǵ� �ٶ���
    // Update is called once per frame
    void Update()
    {
        if (story.Stage2Extra3)
        {
            windFlower.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
