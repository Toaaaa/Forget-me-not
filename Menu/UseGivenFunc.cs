using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseGivenFunc : MonoBehaviour //uiselectingsystem�� �����Ǵ� ������ ��ư�鿡 ���� �־ ���� �ٸ� ����� �����ϰ��ϴ� �Լ�.
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
