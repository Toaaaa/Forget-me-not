using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCardEffect : MonoBehaviour
{
    //카드는 처음 상태에서는 카드가 안보이는 상태이고, 카드를 셋 해줄때 생성되는 효과를 재생, 사용될때 사용되는 효과 등으로
    //실제 보이는 카드는 비주얼적인 효과만 가질뿐 실제 카운팅과 기능은 MonsterAttackManager에서 처리한다.

    public bool TestIsCard;//카드가 꺼져있는지 테스트용.

    private void Start()
    {
        //empty 애니메이션 실행.
    }

    private void Update()
    {
        if (TestIsCard)
        {
            this.GetComponent<Image>().color = Color.white;
        }
        else
        {
            this.GetComponent<Image>().color = Color.red;
        }
    }

    public void CardReset()//카드가 생성되는 효과 재생.
    {
        Debug.Log("카드가 생성되었습니다");
        TestIsCard = true;
    }

    public void CardUsed()//카드가 사용되는 효과 재생.
    {
        Debug.Log("카드가 사용되었습니다");
        TestIsCard = false;
    }
}
