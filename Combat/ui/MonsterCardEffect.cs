using Cysharp.Threading.Tasks;
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
    private List<GameObject> Cards;//ī�� 0 :�Ϲ� �� ī��, ī�� 1: Ư�� ��ų �� ī��
    [SerializeField]
    private List<GameObject> CardEffect;//ī�� ������ ����� ����Ʈ.

    private void Start()
    {
        //empty �ִϸ��̼� ����.
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

    public async void CardReset()//ī�尡 �����Ǵ� ȿ�� ���.
    {
        CardEffect[0].SetActive(false);
        CardEffect[0].SetActive(true);
        await UniTask.Delay(600);//0.6��
        IsCardOn = true;
        CardIsSpecial = false;
    }

    public void CardUsed()//ī�尡 ���Ǵ� ȿ�� ���.
    {
        //ī�尡 ���ɶ� ����Ǵ� ȿ��.

        //IsCardOn = false;�� monsterAttackManager���� ó��.
    }
    public async UniTask SpecialCard()
    {
        //ī�尡 Ư��ī��� �ٲ�� ȿ�� ���.
        CardEffect[1].SetActive(false);
        CardEffect[1].SetActive(true);
        await UniTask.Delay(600);//0.6��
        CardIsSpecial= true;
    }
}
