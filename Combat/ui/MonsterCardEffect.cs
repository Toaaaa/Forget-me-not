using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCardEffect : MonoBehaviour
{
    //ī��� ó�� ���¿����� ī�尡 �Ⱥ��̴� �����̰�, ī�带 �� ���ٶ� �����Ǵ� ȿ���� ���, ���ɶ� ���Ǵ� ȿ�� ������
    //���� ���̴� ī��� ���־����� ȿ���� ������ ���� ī���ð� ����� MonsterAttackManager���� ó���Ѵ�.

    public bool TestIsCard;//ī�尡 �����ִ��� �׽�Ʈ��.

    private void Start()
    {
        //empty �ִϸ��̼� ����.
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

    public void CardReset()//ī�尡 �����Ǵ� ȿ�� ���.
    {
        Debug.Log("ī�尡 �����Ǿ����ϴ�");
        TestIsCard = true;
    }

    public void CardUsed()//ī�尡 ���Ǵ� ȿ�� ���.
    {
        Debug.Log("ī�尡 ���Ǿ����ϴ�");
        TestIsCard = false;
    }
}
