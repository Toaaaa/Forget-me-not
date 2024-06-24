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
    public GameObject Title;//여정의 끝 메인 텍스트 
    public GameObject Continue;//계속 여정하기 버튼
    public GameObject GameC;//게임 종료 버튼

    private int selectIndex;//0:계속 여정하기 1:게임 종료
    private bool isbuted;//여정의 끝 창이 켜지고버튼이 활성화 되었는지.

    private async void OnEnable()
    {
        selectIndex = 0;
        isbuted = false;
        //CombatManager.Instance.isCombatStart = true;//이걸로 다른 ui들 + player의 움직임을 고정.
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
                Continue.GetComponent<Image>().color = new Color32(19, 19, 19, 255);//선택안된 색
                GameC.GetComponent<Image>().color = new Color32(96, 96, 96, 255);//선택된 색
            }
            else
            {
                Continue.GetComponent<Image>().color = new Color32(96, 96, 96, 255);
                GameC.GetComponent<Image>().color = new Color32(19, 19, 19, 255);
            }
        }
    }


    private void ContinueJourney()//마지막 저장지점에서 다시 시작.
    {
        //CombatManager.Instance.isCombatStart = false;
        Debug.Log("마지막 지점 돌아가기");
        this.gameObject.SetActive(false);
        //마지막 저장지점에서 다시 시작하는 함수 추후 추가하기.
    }
    private void GameClose()
    {
        //CombatManager.Instance.isCombatStart = false;
        Debug.Log("게임 종료");
        this.gameObject.SetActive(false);
        //게임 종료시키는 함수 추후 추가하기.
    }

    private async UniTask StartThisPage()//게임오버후 창이 어두워지고 1.5초뒤 게임오버 창이 실행되는 함수.
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

        //여정의 끝 텍스트 등장시 오래된 전등이 켜지는 소리와 등장하기. @@@띵@@@
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
