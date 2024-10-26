using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCardEffect : MonoBehaviour
{
    //카드는 처음 상태에서는 카드가 안보이는 상태이고, 카드를 셋 해줄때 생성되는 효과를 재생, 사용될때 사용되는 효과 등으로
    //실제 보이는 카드는 비주얼적인 효과만 가질뿐 실제 카운팅과 기능은 MonsterAttackManager에서 처리한다.

    public bool IsCardOn;//카드가 켜저있는지.
    public bool CardIsSpecial;//카드가 특수카드인지.
    [SerializeField]
    private List<Sprite> Cards;//카드 0 :일반 턴 카드, 카드 1: 특수 스킬 턴 카드

    private void Start()
    {
        //empty 애니메이션 실행.
    }

    private void Update()
    {
        if (IsCardOn)
        {
            Color card = this.GetComponent<Image>().color;
            card.a = 1;
            GetComponent<Image>().color = card;
        }
        else
        {
            Color card = this.GetComponent<Image>().color;
            card.a = 0;
            GetComponent<Image>().color = card;
        }
        if(CardIsSpecial)
        {
            this.GetComponent<Image>().sprite = Cards[1];
        }
        else
        {
            this.GetComponent<Image>().sprite = Cards[0];
        }

    }

    public void CardReset()//카드가 생성되는 효과 재생.
    {
        Debug.Log("카드가 생성되었습니다");
        IsCardOn = true;
    }

    public void CardUsed()//카드가 사용되는 효과 재생.
    {
        Debug.Log("카드가 사용되었습니다");
        //카드의 종류에 따른 사용 효과 재생 (일반,특수)
        //IsCardOn = false;는 monsterAttackManager에서 처리.
    }
    public void SpecialCard()
    {
        //카드가 특수카드로 바뀌는 효과 재생.
        //적당한 타이밍에 CardIsSpecial을 true 변경
        CardIsSpecial
            = true;//
    }
}
