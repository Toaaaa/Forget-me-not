using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class RewardPageManager : MonoBehaviour
{
    //���� â������ ���� ������Ʈ��, ������.
    public PlayableC[] charactersInParty;//������ ������ ĳ���͵� 
    public GameObject[] rewardCharacter;//������ ������ ĳ���͵��� ��ġ //���⿡ playerInfoOnReward ������Ʈ ����.
    public GameObject[] spotLightSingle;//ĳ���͵鿡�� ���� ���ߴ� ����Ʈ����Ʈ
    public GameObject[] spotLightDouble1;//�������� �ش� ĳ���Ϳ��� ����� �� ���� ����Ʈ ����Ʈ1
    public GameObject[] spotLightDouble2;//�������� �ش� ĳ���Ϳ��� ����� �� ���� ����Ʈ ����Ʈ2
    public RewardDisplay rewardDisplay;//����â�� ������� ǥ���� �� ��ũ��Ʈ (������� ����ġ,���,������)

    private bool CanClose;//����â�� ���� �� �ִ��� ����

    private async void Start()
    {
        charactersInParty = new PlayableC[CombatManager.Instance.playerList.Count];
        charactersInParty = CombatManager.Instance.playerList.ToArray();
        for(int i = 0; i< rewardCharacter.Length; i++)
        {
            rewardCharacter[i].SetActive(false);
        }//�ѹ� �� ���ְ�
        for (int i = 0; i < charactersInParty.Length; i++)
        {
            rewardCharacter[i].SetActive(true);
            rewardCharacter[i].GetComponent<PlayerInfoOnReward>().character = charactersInParty[i];
            rewardCharacter[i].GetComponent<Image>().sprite = charactersInParty[i].charaterRewardImage;
            if (charactersInParty[i].name == "Magician" && !charactersInParty[i].isPerson)
            {
                rewardCharacter[i].GetComponent<Image>().sprite = charactersInParty[i].RewardImageForCat;
            }
        }//�ش� ������ ������ �÷��̾���� ��������Ʈ ��� + ǥ��

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


    private async UniTask TurnOnSpotlight() //������ �������� ���۵Ǹ� 1.8�ʵ� ����Ʈ ����Ʈ�� ���ָ� ���ÿ� ������ ���� ǥ��.+����ġ ���
    {
        RewardDisplayShow();//���� ���� + ǥ��
        await UniTask.Delay(1800);
        for (int i = 0; i < charactersInParty.Length; i++)
        {
            spotLightSingle[i].SetActive(true);
        }
        await UniTask.Delay(1800);//1.8�ʵ� ĳ���͵鿡�� ����ġ ���
        ExpAdding();//����ġ ���
    }
    private async UniTask ExpGiveDone() //����ġ�� ��� �־����� 2�ʵ� ����â�� ������ �ְ� ����
    {
        await UniTask.Delay(2000);
        rewardDisplay.expAllGiven = false;
        CanClose = true;
    }


    private void RewardDisplayShow()//�� ����(����ġ,���,������)�� ǥ���� �ִ� ���÷��� ǥ��
    {
        rewardDisplay.SetReward();//������ �������ִ� �Լ�, ���� ���ĵ� ����
        rewardDisplay.ShowReward();//������ �����ִ� �Լ�, ���÷��̿� ���� ǥ��
    }

    private void ExpAdding()//ĳ���͵鿡 ����ġ ���
    {
        Debug.Log("����ġ ���");
    }

    private void CloseRewardPage()
    {
        rewardDisplay.GiveReward();//������ �ִ� �Լ�(������ + ���)
        rewardDisplay.expAllGiven = false;
        SceneChangeManager.Instance.RewardEnd();
        
    }

}
