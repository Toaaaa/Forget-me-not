using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndOfJourney : MonoBehaviour
{
    public GameObject Title;//������ �� ���� �ؽ�Ʈ 
    public GameObject Continue;//��� �����ϱ� ��ư
    public GameObject GameC;//���� ���� ��ư

    private int selectIndex;//0:��� �����ϱ� 1:���� ����
    private bool isbuted;//������ �� â�� �������ư�� Ȱ��ȭ �Ǿ�����.

    private async void OnEnable()
    {
        selectIndex = 0;
        isbuted = false;
        //CombatManager.Instance.isCombatStart = true;//�̰ɷ� �ٸ� ui�� + player�� �������� ����.
        await StartThisPage();
    }

    private void Update()
    {
        if (isbuted)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (selectIndex == 0)
                {
                    selectIndex = 1;
                }
                else
                {
                    selectIndex = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (selectIndex == 0)
                {
                    ContinueJourney();
                }
                else
                {
                    GameClose();
                }
            }
            if (selectIndex == 1)
            {
                Continue.GetComponent<Image>().color = new Color32(19, 19, 19, 255);//���þȵ� ��
                GameC.GetComponent<Image>().color = new Color32(96, 96, 96, 255);//���õ� ��
            }
            else
            {
                Continue.GetComponent<Image>().color = new Color32(96, 96, 96, 255);
                GameC.GetComponent<Image>().color = new Color32(19, 19, 19, 255);
            }
        }
    }


    private void ContinueJourney()//������ ������������ �ٽ� ����.
    {
        //CombatManager.Instance.isCombatStart = false;
        Debug.Log("������ ���� ���ư���");
        this.gameObject.SetActive(false);
        //������ ������������ �ٽ� �����ϴ� �Լ� ���� �߰��ϱ�.
    }
    private void GameClose()
    {
        //CombatManager.Instance.isCombatStart = false;
        Debug.Log("���� ����");
        this.gameObject.SetActive(false);
        //���� �����Ű�� �Լ� ���� �߰��ϱ�.
    }

    private async UniTask StartThisPage()//���ӿ����� â�� ��ο����� 1.5�ʵ� ���ӿ��� â�� ����Ǵ� �Լ�.
    {
        await UniTask.Delay(1500);
        Title.SetActive(true);
        await UniTask.Delay(300);
        Continue.SetActive(true);
        GameC.SetActive(true);
        Color c = Continue.GetComponent<Image>().color;
        c.a = 0;
        Continue.GetComponent<Image>().color = c;

        Color c2 = GameC.GetComponent<Image>().color;
        c2.a = 0;
        GameC.GetComponent<Image>().color = c2;

        Image img = Continue.GetComponent<Image>();
        Image img2 = GameC.GetComponent<Image>();
        img.DOFade(1, 0.8f).SetEase(Ease.InOutSine);
        img2.DOFade(1, 0.8f).SetEase(Ease.InOutSine)
            .onComplete = () =>
            {
                isbuted = true;
            };

        //������ �� �ؽ�Ʈ ����� ������ ������ ������ �Ҹ��� �����ϱ�. @@@��@@@
    }
    private void OnDisable()
    {
        Continue.GetComponent<Image>().color = new Color32(19, 19, 19, 0);
        GameC.GetComponent<Image>().color = new Color32(19, 19, 19, 0);
        Title.SetActive(false);
        Continue.SetActive(false);
        GameC.SetActive(false);
        isbuted = false;
    }
}
