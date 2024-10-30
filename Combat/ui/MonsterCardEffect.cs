using Cysharp.Threading.Tasks;
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
    private List<GameObject> Cards;//카드 0 :일반 턴 카드, 카드 1: 특수 스킬 턴 카드
    [SerializeField]
    private List<GameObject> CardEffect;//카드 생성시 재생될 이펙트.

    private void Start()
    {
        //empty 애니메이션 실행.
    }

    private void Update()
    {
        if (IsCardOn)
        {
            if (CardIsSpecial)
            {
                Cards[1].SetActive(true);
                Cards[0].SetActive(false);
            }
            else
            {
                Cards[0].SetActive(true);
                Cards[1].SetActive(false);
            }
        }
        else
        {
            Cards[0].SetActive(false);
            Cards[1].SetActive(false);
        }

    }

    public async void CardReset()//카드가 생성되는 효과 재생.
    {
        CardEffect[0].SetActive(false);
        CardEffect[0].SetActive(true);
        await UniTask.Delay(600);//0.6초
        IsCardOn = true;
        CardIsSpecial = false;
    }

    public void CardUsed()//카드가 사용되는 효과 재생.
    {
        //카드가 사용될때 재생되는 효과.

        //IsCardOn = false;는 monsterAttackManager에서 처리.
    }
    public async UniTask SpecialCard()
    {
        //카드가 특수카드로 바뀌는 효과 재생.
        CardEffect[1].SetActive(false);
        CardEffect[1].SetActive(true);
        await UniTask.Delay(600);//0.6초
        CardIsSpecial= true;
    }
}
