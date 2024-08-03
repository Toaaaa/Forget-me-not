using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    public CanvasGroup Fade_img;
    public CanvasGroup Fade_battle1;
    public CanvasGroup Fade_battle2;//전투씬 전환용 캔버스.

    public string transferMapName; //이동할 맵의 이름
    public string battleSceneName; //전투씬 이름

    public bool duringBlackout; //암전중인지
    public bool keepPlayerNoSprite;//플레이어 스프라이트 끄기 유지를 위한 임시 변수(player 스크립트에서 사용)

    [SerializeField]
    float fadeDuration; //암전되는 시간
    [SerializeField]
    float fadeOutDuration;
    [SerializeField]
    float blackoutDuration; //스토리 암전 되는 시간
    [SerializeField]
    float whiteoutDuration;
    [SerializeField]
    float combatFadeDuration; //화면이 닫히는 속도
    [SerializeField]
    float combatFadeOutDuration; //화면이 열리는 속도

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void BlackOut()
    {
        Fade_img.DOFade(1, blackoutDuration)
        .OnStart(() =>
        {
            duringBlackout = true;
        })
        .OnComplete(() =>
        {
            StartCoroutine(BlackOutout());
        });
    }

    public void BlackOutEndJourney()
    {
        Fade_img.DOFade(1, blackoutDuration)
        .OnStart(() =>
        {
            duringBlackout = true;
        })
        .OnComplete(() =>
        {
            StartCoroutine(BlackOutJourneyout());
        });
    }
    public void ChangeScene()
    {
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() =>
        {
            GameManager.Instance.onSceneChange = true;
            Player.Instance.GetComponent<RandomEncounter>().timeMoved = 0; //이동시 인카운터 시간 초기화.
            Player.Instance.GetComponent<RandomEncounter>().encounterCount = 0; //이동시 인카운터 카운트 초기화.
            //Fade_img.blocksRaycasts = true; //레이캐스트 막기
        })
        .OnComplete(() =>
        {
            VirtualCamera.Instance.GetComponent<CinemachineVirtualCamera>().enabled = false; //비활성화 >> 활성화 해주면 자동으로 플레이어 위치에 세팅됨.
            StartCoroutine(LoadScene());
        });
    }
    private void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        Fade_img.DOFade(0, fadeOutDuration)
            .OnStart(() =>
            {
                VirtualCamera.Instance.GetComponent<CinemachineVirtualCamera>().enabled = true;
                Camera.main.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, Camera.main.transform.position.z);
            })
            .OnUpdate(() =>
            {
                VirtualCamera.Instance.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, VirtualCamera.Instance.transform.position.z);
                Camera.main.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, Camera.main.transform.position.z);
            })
            .OnComplete(() =>
            {
                GameManager.Instance.onSceneChange = false;
                Fade_img.blocksRaycasts = false;
            });
    }
    public void OnBlackOutFin()
    {
        Fade_img.DOFade(0, whiteoutDuration)
            .OnStart(() =>
            {
                duringBlackout = false;
            });
    }
    public void OnBlackOutJourneyFin()
    {
        Fade_img.DOFade(0, 0.1f)
            .OnStart(() =>
            {
                CombatManager.Instance.EndOfJourney.gameObject.SetActive(true);
                CombatManager.Instance.LostCombatMobClear();//전투에서 죽은 몬스터들 제거, 이곳에서 제거해야지, 멋대로 combatend()함수가 진행 안됨.
                duringBlackout = false;
            });
    }
    public void OnBlackOutFinCust(float duration)
    {
        Fade_img.DOFade(0, duration)
            .OnStart(() =>
            {
                duringBlackout = false;
            });
    }
    public void ChangeBattleScene()
    {
        //여기서 좌우로 닫는 효과 넣고
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(690, combatFadeDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-615, combatFadeDuration).SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
            GameManager.Instance.onSceneChange = true;
            //Fade_img.blocksRaycasts = true; //레이캐스트 막기
        })
            .OnComplete(() =>
            {
                CombatManager.Instance.combatDisplay.gameObject.SetActive(true);
                StartCoroutine(LoadBattleScene());
            });
    }

    public void OnBattleScene() //전투가 시작되며 화면이 열림
    {
        //여기서 좌우로 열리는 효과 넣고.
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(2190, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-2115, combatFadeOutDuration).SetEase(Ease.InOutSine);
        GameManager.Instance.onSceneChange = false;
        CombatManager.Instance.isCombatStart = true;
        //전투가 시작 될수 있게 하기.
    }
    public void LeaveBattleScene()
    {
        //여기서 다시 좌우로 닫히는 효과 넣고
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(690, combatFadeDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-615, combatFadeDuration).SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
                keepPlayerNoSprite= true;
                GameManager.Instance.onSceneChange = true;
                //Fade_img.blocksRaycasts = true; //레이캐스트 막기
            })
            .OnComplete(() =>
            {
                StartCoroutine(EndBattleInfo());
                CombatManager.Instance.combatDisplay.gameObject.SetActive(false);
            });
    }
    public void RewardEnd()//보상상이 끝나면 호출될 함수 (창닫고,플레이어 스프라이트 키고,보상창 끄고, 다시열기)
    {
        //좌우로 닫히는 효과
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(690, combatFadeDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-615, combatFadeDuration).SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
                keepPlayerNoSprite = true;//플레이어 스프라이트 끄기 유지
                GameManager.Instance.onSceneChange = true;
                //Fade_img.blocksRaycasts = true; //레이캐스트 막기
            })
            .OnComplete(() =>
            {
                GameManager.Instance.rewardPageManager.gameObject.SetActive(false);
                CombatManager.Instance.combatDisplay.gameObject.SetActive(false);
                StartCoroutine(EndReward());
            });
    }


    IEnumerator BlackOutout()
    { 
        yield return new WaitForSeconds(1f);
        OnBlackOutFin();
    }
    IEnumerator BlackOutJourneyout()
    {
        yield return new WaitForSeconds(0.5f);
        OnBlackOutJourneyFin();
    }

    IEnumerator LoadScene()
    {
        SceneManager.LoadScene(transferMapName);
        yield return 1.0f;
        OnSceneLoaded(SceneManager.GetSceneByName(transferMapName), LoadSceneMode.Single);
    }

    IEnumerator LoadBattleScene()
    {
        SceneManager.LoadScene(battleSceneName);
        Player.Instance.CombatPositioning();
        yield return 1.0f;
        OnBattleScene();
    }
    IEnumerator EndBattleInfo()//전투 끝난뒤 보상창 띄우기
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.onSceneChange = false;
        //좌우로 열리는 효과
        Fade_battle1.transform.DOMoveY(2690, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.transform.DOMoveY(-2615, combatFadeOutDuration).SetEase(Ease.InOutSine);
        GameManager.Instance.rewardPageManager.gameObject.SetActive(true);
    }
    IEnumerator EndReward()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.Camera.GetComponent<PixelPerfectCamera>().enabled = true;//전투씬+보상씬이 끝나면 다시 픽셀 퍼펙트 카메라 활성화.
        SceneManager.LoadScene(Player.Instance.currentMapName);
        keepPlayerNoSprite = false;//플레이어 스프라이트 다시 켜기
        Fade_battle1.transform.DOMoveY(2690, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.transform.DOMoveY(-2615, combatFadeOutDuration).SetEase(Ease.InOutSine);
    }



}
