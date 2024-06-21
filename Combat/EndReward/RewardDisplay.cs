using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDisplay : MonoBehaviour
{
    public float exp;//경험치
    public int gold;//골드
    public List<Item> rewardItems;//보상 아이템 리스트
    public bool expAllGiven;//경험치가 모두 주어졌는지 남은 경험치가 0이됨

    // (보상의 경우,monster list에 남아있는 데이터를 기반으로 아이템 보상 고르기
    // (경험치는 고정값, 골드는 기본 +-random수치로,보상의 경우 보상의 정해진 각 확률에 따라
    // 보상 갯수 0~2를 먼저 고른뒤보상을 추가 확률로 배정)
    private void Start()
    {
        expAllGiven = false;
        HideReward();
    }


    public void SetReward()//보상을 설정해주는 함수, 각종 계산식들 포함
    {
        expAllGiven = false;//경험치 리필하면서, 초기화 해주기
        //exp =...계산식
    }
    public void ShowReward()//보상을 보여주는 함수, 디스플레이에 보상 표기
    {
        Debug.Log("보상표시중");
    }
    public void HideReward()//보상을 숨기는 함수, 디스플레이에 보상 숨김
    {

    }
}
