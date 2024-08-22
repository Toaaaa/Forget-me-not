using Cysharp.Threading.Tasks;
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

    public float exp;//����ġ
    public float OriginalExp;//�����ֱ��� ó�� ����ġ.
    public int gold;//���
    public List<Item> rewardItems;//���� ������ ����Ʈ
    public bool expAllGiven;//����ġ�� ��� �־������� ���� ����ġ�� 0�̵�
    public float decreaseRate = 80f; // ����ġ ��н� �ʴ� ������ ����ġ ��

    // (������ ���,monster list�� �����ִ� �����͸� ������� ������ ���� ����
    // (����ġ�� ������, ���� �⺻ +-random��ġ��,������ ��� ������ ������ �� Ȯ���� ����
    // ���� ���� 0~2�� ���� ���ں����� �߰� Ȯ���� ����)
    private void OnEnable()
    {
        expAllGiven = false;
        rewardItems = new List<Item>();
    }
    private void Update()
    {
        expDisplay.GetComponent<TextMeshProUGUI>().text = ((int)exp).ToString();
    }

    public void SetReward()//������ �������ִ� �Լ�, ���� ���ĵ� ����
    {
        if (CombatManager.Instance.selectedFlee)//���� ������ ���ؼ� ������ ������ ��� ���� ���� ���
        {
            CombatManager.Instance.DeadMobExpCount = 0;
            CombatManager.Instance.DeadMobGoldCount = 0;
            CombatManager.Instance.DeadMobItemDrop.Clear();
        }
        expAllGiven = false;//����ġ �����ϸ鼭, �ʱ�ȭ ���ֱ�
        exp = CombatManager.Instance.DeadMobExpCount;//����ġ ����
        OriginalExp = exp;
        gold = RandomGold();//��� ����


        CombatManager.Instance.DeadMobExpCount = 0;//����ġ ���� ������ ���� ���ֱ�.
        CombatManager.Instance.DeadMobGoldCount = 0;//��� ���� ������ ���� ���ֱ�.
        CombatManager.Instance.DeadMobItemDrop.Clear();//������ ��� ����Ʈ �ʱ�ȭ
    }
    public void ShowReward()//������ �����ִ� �Լ�, ���÷��̿� ���� ǥ��
    {
        expDisplay.GetComponent<TextMeshProUGUI>().text =exp.ToString();
        goldDisplay.GetComponent<TextMeshProUGUI>().text = gold.ToString();
        for(int i = 0; i<rewardItems.Count; i++)
        {
            itemDisplay.GetComponent<TextMeshProUGUI>().text += rewardItems[i].name + "\n";
        }
    }

    private int RandomGold()//��带 �������� �ִ� �Լ� combatManager�� DeadMobGoldCount�� -10~10������ �������� ������
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
        if(totalItemCount < CombatManager.Instance.DeadMobItemDrop.Count) //������ ���� �������� ������ ��� �Ѱ�ġ���� ���� ���
        {
            for(int i = 0; i<totalItemCount; i++)
            {
                rewardItems.Add(CombatManager.Instance.DeadMobItemDrop[i]);
            }
        }
        else
        {
            for(int i = 0; i<CombatManager.Instance.DeadMobItemDrop.Count; i++) //������ ���� �������� ������ ��� �Ѱ�ġ���� ���� ���
            {
                rewardItems.Add(CombatManager.Instance.DeadMobItemDrop[i]);
            }
        }

    }
    private int SetItemCountLimit()//�� ������ ��� ������ 0���� 2�� �������ִ� �Լ�.
    {

        double randValue = random.NextDouble(); // 0.0���� 1.0 ������ ������ ����

        if (randValue < 0.5)
        {
            return 0; // 50% Ȯ���� 0�� ��ȯ
        }
        else if (randValue < 0.9)
        {
            return 1; // 40% Ȯ���� 1�� ��ȯ (0.5 <= randValue < 0.9)
        }
        else
        {
            return 2; // 10% Ȯ���� 2�� ��ȯ (0.9 <= randValue <= 1.0)
        
        }


    }
    public void GiveReward()//���� �������� �κ��丮�� �����ִ� �Լ�
    {
        GameManager.Instance.inventory.goldHave += gold;

        for(int i = 0; i<rewardItems.Count; i++)
        {
            GameManager.Instance.inventory.AddItem(rewardItems[i], 1, (int)rewardItems[i].itemType);
        }
    }
    public void ExpGive()//����ġ�� �ʴ� 80�� �����ִ� �Լ�
    {
        DecreaseExperienceOverTime().Forget();
    }
    private async UniTaskVoid DecreaseExperienceOverTime()
    {
        while (exp > 0)
        {
            float decreaseAmount = decreaseRate * Time.deltaTime; // �� �����ӿ��� ������ ��
            exp -= decreaseAmount;
            // ����ġ�� 0���� �۾����� �ʵ��� ����
            if (exp <= 0)
            {
                expAllGiven = true;
                exp = 0;
            }

            // ���� �����ӱ��� ���
            await UniTask.Yield();
        }

    }
}
