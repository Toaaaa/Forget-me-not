using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class RewardPageManager : MonoBehaviour
{
    //보상 창에서의 각종 오브젝트들, 변수들.
    public PlayableC[] charactersInParty;//전투에 참여한 캐릭터들 
    public GameObject[] rewardCharacter;//전투에 참여한 캐릭터들의 위치 //여기에 playerInfoOnReward 컴포넌트 있음.
    public GameObject[] spotLightSingle;//캐릭터들에게 빛을 비추는 스포트라이트
    public GameObject[] spotLightDouble1;//레벨업시 해당 캐릭터에게 사용해 줄 더블 스포트 라이트1
    public GameObject[] spotLightDouble2;//레벨업시 해당 캐릭터에게 사용해 줄 더블 스포트 라이트2
    public RewardDisplay rewardDisplay;//보상창의 보상들을 표시해 줄 스크립트 (순서대로 경험치,골드,아이템)

    private bool CanClose;//보상창을 닫을 수 있는지 여부
    public float decreaseRate = 80f; // 경험치 배분시 초당 감소할 경험치 양

    private async void OnEnable()
    {
        charactersInParty = new PlayableC[CombatManager.Instance.playerList.Count];
        charactersInParty = CombatManager.Instance.playerList.ToArray();
        for(int i=0; i<4; i++)
        {
            spotLightSingle[i].SetActive(false);
            spotLightDouble1[i].SetActive(false);
            spotLightDouble2[i].SetActive(false);
        }//시작전 불 한번 다 끄기
        for(int i = 0; i< rewardCharacter.Length; i++)
        {
            rewardCharacter[i].SetActive(false);
        }//한번 다 꺼주고
        for (int i = 0; i < charactersInParty.Length; i++)
        {
            rewardCharacter[i].SetActive(true);
            rewardCharacter[i].GetComponent<PlayerInfoOnReward>().character = charactersInParty[i];
            rewardCharacter[i].GetComponent<Image>().sprite = charactersInParty[i].charaterRewardImage;
            if (charactersInParty[i].name == "Magician" && !charactersInParty[i].isPerson)
            {
                rewardCharacter[i].GetComponent<Image>().sprite = charactersInParty[i].RewardImageForCat;
            }
        }//해당 전투에 참여한 플레이어들의 스프라이트 등록 + 표시

        await TurnOnSpotlight();
    }

    private async void Update()
    {
        if (CanClose)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rewardDisplay.expAllGiven = false;
                CanClose = false;
                CloseRewardPage();
            }
        }
        if (rewardDisplay.expAllGiven)
        {
            await ExpGiveDone();
        }
        for (int i = 0; i < charactersInParty.Length; i++)
        {
            if (rewardCharacter[i].GetComponent<PlayerInfoOnReward>().levelUp)
            {
                ChangeSpotLight(i);
            }
        }
    }


    private async UniTask TurnOnSpotlight() //리워드 페이지가 시작되면 1.8초뒤 스포트 라이트를 켜주며 동시에 리워드 보상도 표시.+경험치 배분
    {
        RewardDisplayShow();//보상 설정 + 표시
        await UniTask.Delay(1800);
        for (int i = 0; i < charactersInParty.Length; i++)
        {
            spotLightSingle[i].SetActive(true);
        }
        await UniTask.Delay(1800);//1.8초뒤 캐릭터들에게 경험치 배분
        ExpAdding();//경험치 배분
    }
    private void ChangeSpotLight(int i)//레벨업시 single스포트 라이트에서 double스포트 라이트로 교체.
    {
        if (spotLightDouble1[i])
        {
            //켜지는 소리 안내게.
        }
        spotLightSingle[i].SetActive(false);
        spotLightDouble1[i].SetActive(true);
        spotLightDouble2[i].SetActive(true);
    }
    private async UniTask ExpGiveDone() //경험치가 모두 주어지면 2초뒤 보상창을 닫을수 있게 해줌
    {
        await UniTask.Delay(2000);
        rewardDisplay.expAllGiven = false;
        CanClose = true;
    }


    private void RewardDisplayShow()//총 보상(경험치,골드,아이템)을 표시해 주는 디스플레이 표시
    {
        rewardDisplay.SetReward();//보상을 설정해주는 함수, 각종 계산식들 포함
        rewardDisplay.ShowReward();//보상을 보여주는 함수, 디스플레이에 보상 표기
    }

    private void ExpAdding()//캐릭터들에 경험치 배분
    {
        //여기서는 경험치를 캐릭터들에게 배분해주는 함수.
        for(int i = 0; i< charactersInParty.Length; i++)
        {
            ExpGiveToChar(charactersInParty[i]).Forget();
        }
        rewardDisplay.ExpGive();
    }

    private void CloseRewardPage()
    {
        CheckStageBossClear();//보스방에서 보스를 클리어 했을경우 스토리 진행 체크포인트 저장.
        CheckIfStoryEnemy();//스토리상 진행되는 전투일 경우 해당 경우 체크포인트 저장.
        rewardDisplay.GiveReward();//보상을 주는 함수(아이템 + 골드)
        rewardDisplay.expAllGiven = false;
        SceneChangeManager.Instance.RewardEnd();
        
    }

    private void CheckStageBossClear()//보스방에서 보스를 클리어 했을경우 스토리 진행 체크포인트 저장.
    {
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "battle in stage1 Boss":
                GameManager.Instance.storyScriptable.Stage1BossCompleted = true;
                break;
            case "battle in stage2 Boss"://스테이지 2 보스 클리어
                GameManager.Instance.storyScriptable.Stage2Check7Dragon = true;
                break;
            default:
                break;
        }
    }
    private void CheckIfStoryEnemy()
    {
        string mapName = Player.Instance.currentMapName;
        switch(mapName)
        {
            case "Stage1-5"://스테이지 1-5에서의 고블린 타일맵 트리거 전투
                GameManager.Instance.storyScriptable.Stage1Encountered = true;
                break;
            default:
                break;
        }
    }
    private async UniTaskVoid ExpGiveToChar(PlayableC c)
    {
        while (rewardDisplay.OriginalExp > 0)
        {
            float decreaseAmount = decreaseRate * Time.deltaTime; // 한 프레임에서 감소할 양
            rewardDisplay.OriginalExp -= decreaseAmount/charactersInParty.Length;
            c.exp += decreaseAmount; //for문이 4번 돌면서 deltatime의 수치가 1/n로 줄어들더라 그래서 캐릭터 인원수 곱해줌.

            // 경험치가 0보다 작아지지 않도록 설정
            if (rewardDisplay.OriginalExp <= 0)
            {
                rewardDisplay.OriginalExp = 0;
            }

            // 다음 프레임까지 대기
            await UniTask.Yield();
        }
    }

}
