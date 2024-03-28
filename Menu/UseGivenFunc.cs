using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseGivenFunc : MonoBehaviour //uiselectingsystem과 연동되는 선택할 버튼들에 각각 넣어서 서로 다른 기능을 연출하게하는 함수.
{
  public enum uiFunc
    {
        a,
        b,
        c,
        d

    }
    
    public void UseFunc()
    {
        uiFunc unif = this.GetComponent<uiFunc>();
        switch(unif)
        {
            case uiFunc.a:
                Debug.Log("a");
                break;
            case uiFunc.b:
                Debug.Log("b");
                break;
            case uiFunc.c:
                Debug.Log("c");
                break;
            case uiFunc.d:
                Debug.Log("d");
                break;
        }
    }
}
