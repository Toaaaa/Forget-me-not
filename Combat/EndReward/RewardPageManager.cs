using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using UnityEngine.UI;

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

    private async void Start()
    {
        charactersInParty = new PlayableC[CombatManager.Instance.playerList.Count];
        charactersInParty = CombatManager.Instance.playerList.ToArray();
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
        Debug.Log("경험치 배분");
    }

    private void CloseRewardPage()
    {
        rewardDisplay.GiveReward();//보상을 주는 함수(아이템 + 골드)
        rewardDisplay.expAllGiven = false;
        SceneChangeManager.Instance.RewardEnd();
        
    }

}
