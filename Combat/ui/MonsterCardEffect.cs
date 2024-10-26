using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCardEffect : MonoBehaviour
{
    //ī��� ó�� ���¿����� ī�尡 �Ⱥ��̴� �����̰�, ī�带 �� ���ٶ� �����Ǵ� ȿ���� ���, ���ɶ� ���Ǵ� ȿ�� ������
    //���� ���̴� ī��� ���־����� ȿ���� ������ ���� ī���ð� ����� MonsterAttackManager���� ó���Ѵ�.

    public bool IsCardOn;//ī�尡 �����ִ���.
    public bool CardIsSpecial;//ī�尡 Ư��ī������.
    [SerializeField]
    private List<Sprite> Cards;//ī�� 0 :�Ϲ� �� ī��, ī�� 1: Ư�� ��ų �� ī��

    private void Start()
    {
        //empty �ִϸ��̼� ����.
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

    public void CardReset()//ī�尡 �����Ǵ� ȿ�� ���.
    {
        Debug.Log("ī�尡 �����Ǿ����ϴ�");
        IsCardOn = true;
    }

    public void CardUsed()//ī�尡 ���Ǵ� ȿ�� ���.
    {
        Debug.Log("ī�尡 ���Ǿ����ϴ�");
        //ī���� ������ ���� ��� ȿ�� ��� (�Ϲ�,Ư��)
        //IsCardOn = false;�� monsterAttackManager���� ó��.
    }
    public void SpecialCard()
    {
        //ī�尡 Ư��ī��� �ٲ�� ȿ�� ���.
        //������ Ÿ�ֿ̹� CardIsSpecial�� true ����
        CardIsSpecial
            = true;//
    }
}
