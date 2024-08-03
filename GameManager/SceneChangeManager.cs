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
    public CanvasGroup Fade_battle2;//������ ��ȯ�� ĵ����.

    public string transferMapName; //�̵��� ���� �̸�
    public string battleSceneName; //������ �̸�

    public bool duringBlackout; //����������
    public bool keepPlayerNoSprite;//�÷��̾� ��������Ʈ ���� ������ ���� �ӽ� ����(player ��ũ��Ʈ���� ���)

    [SerializeField]
    float fadeDuration; //�����Ǵ� �ð�
    [SerializeField]
    float fadeOutDuration;
    [SerializeField]
    float blackoutDuration; //���丮 ���� �Ǵ� �ð�
    [SerializeField]
    float whiteoutDuration;
    [SerializeField]
    float combatFadeDuration; //ȭ���� ������ �ӵ�
    [SerializeField]
    float combatFadeOutDuration; //ȭ���� ������ �ӵ�

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
            Player.Instance.GetComponent<RandomEncounter>().timeMoved = 0; //�̵��� ��ī���� �ð� �ʱ�ȭ.
            Player.Instance.GetComponent<RandomEncounter>().encounterCount = 0; //�̵��� ��ī���� ī��Ʈ �ʱ�ȭ.
            //Fade_img.blocksRaycasts = true; //����ĳ��Ʈ ����
        })
        .OnComplete(() =>
        {
            VirtualCamera.Instance.GetComponent<CinemachineVirtualCamera>().enabled = false; //��Ȱ��ȭ >> Ȱ��ȭ ���ָ� �ڵ����� �÷��̾� ��ġ�� ���õ�.
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
                CombatManager.Instance.LostCombatMobClear();//�������� ���� ���͵� ����, �̰����� �����ؾ���, �ڴ�� combatend()�Լ��� ���� �ȵ�.
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
                keepPlayerNoSprite= true;
                GameManager.Instance.onSceneChange = true;
                //Fade_img.blocksRaycasts = true; //����ĳ��Ʈ ����
            })
            .OnComplete(() =>
            {
                StartCoroutine(EndBattleInfo());
                CombatManager.Instance.combatDisplay.gameObject.SetActive(false);
            });
    }
    public void RewardEnd()//������� ������ ȣ��� �Լ� (â�ݰ�,�÷��̾� ��������Ʈ Ű��,����â ����, �ٽÿ���)
    {
        //�¿�� ������ ȿ��
        Fade_battle1.GetComponent<RectTransform>().DOLocalMoveY(690, combatFadeDuration).SetEase(Ease.InOutSine);
        Fade_battle2.GetComponent<RectTransform>().DOLocalMoveY(-615, combatFadeDuration).SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
                keepPlayerNoSprite = true;//�÷��̾� ��������Ʈ ���� ����
                GameManager.Instance.onSceneChange = true;
                //Fade_img.blocksRaycasts = true; //����ĳ��Ʈ ����
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
    IEnumerator EndBattleInfo()//���� ������ ����â ����
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.onSceneChange = false;
        //�¿�� ������ ȿ��
        Fade_battle1.transform.DOMoveY(2690, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.transform.DOMoveY(-2615, combatFadeOutDuration).SetEase(Ease.InOutSine);
        GameManager.Instance.rewardPageManager.gameObject.SetActive(true);
    }
    IEnumerator EndReward()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.Camera.GetComponent<PixelPerfectCamera>().enabled = true;//������+������� ������ �ٽ� �ȼ� ����Ʈ ī�޶� Ȱ��ȭ.
        SceneManager.LoadScene(Player.Instance.currentMapName);
        keepPlayerNoSprite = false;//�÷��̾� ��������Ʈ �ٽ� �ѱ�
        Fade_battle1.transform.DOMoveY(2690, combatFadeOutDuration).SetEase(Ease.InOutSine);
        Fade_battle2.transform.DOMoveY(-2615, combatFadeOutDuration).SetEase(Ease.InOutSine);
    }



}
