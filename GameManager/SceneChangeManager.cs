using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    public CanvasGroup Fade_img;
    public CanvasGroup Fade_battle1;
    public CanvasGroup Fade_battle2;//������ ��ȯ�� ĵ����.

    public string transferMapName; //�̵��� ���� �̸�
    public string battleSceneName; //������ �̸�


    [SerializeField]
    float fadeDuration = 0.7f; //�����Ǵ� �ð�
    [SerializeField]
    float fadeOutDuration = 0.4f;

    [SerializeField]
    float combatFadeDuration = 0.7f; //ȭ���� ������ �ӵ�
    [SerializeField]
    float combatFadeOutDuration = 0.7f; //ȭ���� ������ �ӵ�

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
            Player.Instance.GetComponent<RandomEncounter>().timeMoved = 0; //�̵��� ��ī���� �ð� �ʱ�ȭ.
            Player.Instance.GetComponent<RandomEncounter>().encounterCount = 0; //�̵��� ��ī���� ī��Ʈ �ʱ�ȭ.
            //Fade_img.blocksRaycasts = true; //����ĳ��Ʈ ����
        })
        .OnComplete(() =>
        {
            StartCoroutine(LoadScene());
        });
    }
    private void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        Fade_img.DOFade(0, fadeDuration)
            .OnComplete(() =>
            {
                GameManager.Instance.onSceneChange = false;
                Fade_img.blocksRaycasts = false;
            });
    }
    public void ChangeBattleScene()
    {
        //���⼭ �¿�� �ݴ� ȿ�� �ְ�
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(690, combatFadeDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-615, combatFadeDuration).SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
            GameManager.Instance.onSceneChange = true;
            //Fade_img.blocksRaycasts = true; //����ĳ��Ʈ ����
        })
            .OnComplete(() =>
            {
                CombatManager.Instance.combatDisplay.gameObject.SetActive(true);
                StartCoroutine(LoadBattleScene());
            });
    }

    public void OnBattleScene() //������ ���۵Ǹ� ȭ���� ����
    {
        //���⼭ �¿�� ������ ȿ�� �ְ�.
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(2190, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-2115, combatFadeOutDuration).SetEase(Ease.InOutSine);
        GameManager.Instance.onSceneChange = false;
        CombatManager.Instance.isCombatStart = true;
        //������ ���� �ɼ� �ְ� �ϱ�.
    }
    public void LeaveBattleScene()
    {
        //���⼭ �ٽ� �¿�� ������ ȿ�� �ְ�
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(690, combatFadeDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-615, combatFadeDuration).SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
                GameManager.Instance.onSceneChange = true;
                //Fade_img.blocksRaycasts = true; //����ĳ��Ʈ ����
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
        //���⼭ ���� ���â ǥ��.
        //���â Ȯ���� �� ������
        //GameManager.Instance.onSceneChange = false; �̰� �ϱ�.
        //�׸��� ���� ������ ���ư���.
        SceneManager.LoadScene(Player.Instance.currentMapName);
        yield return new WaitForSeconds(1.0f);
        Fade_battle1.transform.DOMoveY(2190, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.transform.DOMoveY(-2115, combatFadeOutDuration).SetEase(Ease.InOutSine);
    }
}
