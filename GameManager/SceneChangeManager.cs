using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    public CanvasGroup Fade_img;
    public CanvasGroup Fade_battle1;
    public CanvasGroup Fade_battle2;//전투씬 전환용 캔버스.

    public string transferMapName; //이동할 맵의 이름
    public string battleSceneName; //전투씬 이름


    [SerializeField]
    float fadeDuration; //암전되는 시간
    [SerializeField]
    float fadeOutDuration;

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
                GameManager.Instance.onSceneChange = true;
                //Fade_img.blocksRaycasts = true; //레이캐스트 막기
            })
            .OnComplete(() =>
            {
                StartCoroutine(EndBattleInfo());
                CombatManager.Instance.combatDisplay.gameObject.SetActive(false);
            });
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
    IEnumerator EndBattleInfo()
    {
        yield return new WaitForSeconds(1.0f);
        //여기서 여러 결과창 표시.
        //결과창 확인이 다 끝나면
        //GameManager.Instance.onSceneChange = false; 이거 하기.
        //그리고 원래 씬으로 돌아가기.
        SceneManager.LoadScene(Player.Instance.currentMapName);
        yield return new WaitForSeconds(1.0f);
        Fade_battle1.transform.DOMoveY(2190, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.transform.DOMoveY(-2115, combatFadeOutDuration).SetEase(Ease.InOutSine);
    }
}
