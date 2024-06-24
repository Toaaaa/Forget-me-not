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
    public float decreaseRate = 80f; // ����ġ ��н� �ʴ� ������ ����ġ ��

    private async void OnEnable()
    {
        charactersInParty = new PlayableC[CombatManager.Instance.playerList.Count];
        charactersInParty = CombatManager.Instance.playerList.ToArray();
        for(int i=0; i<4; i++)
        {
            spotLightSingle[i].SetActive(false);
            spotLightDouble1[i].SetActive(false);
            spotLightDouble2[i].SetActive(false);
        }//������ �� �ѹ� �� ����
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
        for (int i = 0; i < charactersInParty.Length; i++)
        {
            if (rewardCharacter[i].GetComponent<PlayerInfoOnReward>().levelUp)
            {
                ChangeSpotLight(i);
            }
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
    private void ChangeSpotLight(int i)//�������� single����Ʈ ����Ʈ���� double����Ʈ ����Ʈ�� ��ü.
    {
        if (spotLightDouble1[i])
        {
            //������ �Ҹ� �ȳ���.
        }
        spotLightSingle[i].SetActive(false);
        spotLightDouble1[i].SetActive(true);
        spotLightDouble2[i].SetActive(true);
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
        //���⼭�� ����ġ�� ĳ���͵鿡�� ������ִ� �Լ�.
        for(int i = 0; i< charactersInParty.Length; i++)
        {
            ExpGiveToChar(charactersInParty[i]).Forget();
        }
        rewardDisplay.ExpGive();
    }

    private void CloseRewardPage()
    {
        rewardDisplay.GiveReward();//������ �ִ� �Լ�(������ + ���)
        rewardDisplay.expAllGiven = false;
        SceneChangeManager.Instance.RewardEnd();
        
    }
    private async UniTaskVoid ExpGiveToChar(PlayableC c)
    {
        while (rewardDisplay.OriginalExp > 0)
        {
            float decreaseAmount = decreaseRate * Time.deltaTime; // �� �����ӿ��� ������ ��
            Debug.Log("OriginalExp : " + rewardDisplay.OriginalExp);
            rewardDisplay.OriginalExp -= decreaseAmount/charactersInParty.Length;
            c.exp += decreaseAmount; //for���� 4�� ���鼭 deltatime�� ��ġ�� 1/n�� �پ����� �׷��� ĳ���� �ο��� ������.

            // ����ġ�� 0���� �۾����� �ʵ��� ����
            if (rewardDisplay.OriginalExp <= 0)
            {
                rewardDisplay.OriginalExp = 0;
            }

            // ���� �����ӱ��� ���
            await UniTask.Yield();
        }
    }

}
