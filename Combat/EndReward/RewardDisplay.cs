using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardDisplay : MonoBehaviour
{
    private System.Random random = new System.Random();
    public GameObject expDisplay;
    public GameObject goldDisplay;
    public GameObject itemDisplay;

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
        rewardItems = new List<Item>();
    }


    public void SetReward()//보상을 설정해주는 함수, 각종 계산식들 포함
    {
        expAllGiven = false;//경험치 리필하면서, 초기화 해주기
        exp = CombatManager.Instance.DeadMobExpCount;//경험치 세팅
        gold = RandomGold();//골드 세팅


        CombatManager.Instance.DeadMobExpCount = 0;//경험치 세팅 끝나면 리셋 해주기.
        CombatManager.Instance.DeadMobGoldCount = 0;//골드 세팅 끝나면 리셋 해주기.
        CombatManager.Instance.DeadMobItemDrop.Clear();//아이템 드랍 리스트 초기화
    }
    public void ShowReward()//보상을 보여주는 함수, 디스플레이에 보상 표기
    {
        expDisplay.GetComponent<TextMeshProUGUI>().text =exp.ToString();
        goldDisplay.GetComponent<TextMeshProUGUI>().text = gold.ToString();
        for(int i = 0; i<rewardItems.Count; i++)
        {
            itemDisplay.GetComponent<TextMeshProUGUI>().text += rewardItems[i].name + "\n";
        }
    }

    private int RandomGold()//골드를 랜덤으로 주는 함수 combatManager의 DeadMobGoldCount에 -10~10사이의 랜덤값을 더해줌
    {
        int randomGold = Random.Range(-10, 10);
        CombatManager.Instance.DeadMobGoldCount += randomGold;
        if(CombatManager.Instance.DeadMobGoldCount < 0)
        {
            CombatManager.Instance.DeadMobGoldCount = 3;
        }
        return CombatManager.Instance.DeadMobGoldCount;
    }

    private void ItemSet()
    {
        int totalItemCount = SetItemCountLimit();
        if(totalItemCount < CombatManager.Instance.DeadMobItemDrop.Count) //몹에서 떨군 아이템이 아이템 드랍 한계치보다 많을 경우
        {
            for(int i = 0; i<totalItemCount; i++)
            {
                rewardItems.Add(CombatManager.Instance.DeadMobItemDrop[i]);
            }
        }
        else
        {
            for(int i = 0; i<CombatManager.Instance.DeadMobItemDrop.Count; i++) //몹에서 떨군 아이템이 아이템 드랍 한계치보다 적을 경우
            {
                rewardItems.Add(CombatManager.Instance.DeadMobItemDrop[i]);
            }
        }

    }
    private int SetItemCountLimit()//총 아이템 드랍 개수를 0에서 2로 세팅해주는 함수.
    {

        double randValue = random.NextDouble(); // 0.0부터 1.0 사이의 난수를 생성

        if (randValue < 0.5)
        {
            return 0; // 50% 확률로 0을 반환
        }
        else if (randValue < 0.9)
        {
            return 1; // 40% 확률로 1을 반환 (0.5 <= randValue < 0.9)
        }
        else
        {
            return 2; // 10% 확률로 2를 반환 (0.9 <= randValue <= 1.0)
        
        }


    }
    public void GiveReward()//골드와 아이템을 인벤토리로 보내주는 함수
    {
        GameManager.Instance.inventory.goldHave += gold;

        for(int i = 0; i<rewardItems.Count; i++)
        {
            GameManager.Instance.inventory.AddItem(rewardItems[i], 1, (int)rewardItems[i].itemType);
        }
    }
}
