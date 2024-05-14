using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public StoryScriptable SSobj;
    //이거를 스토리 스크립트 매니저와 함께 써서,
    private void Start()
    {
        SSobj.restAll();
    }
    private void Update()
    {
        
    }
}
