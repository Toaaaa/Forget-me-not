using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public StoryScriptable SSobj;
    //�̰Ÿ� ���丮 ��ũ��Ʈ �Ŵ����� �Բ� �Ἥ,
    private void Start()
    {
        SSobj.GetComponent<StoryScriptable>().restAll();
    }
    private void Update()
    {
        
    }
}