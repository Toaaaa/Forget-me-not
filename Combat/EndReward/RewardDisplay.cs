using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDisplay : MonoBehaviour
{
    public float exp;//����ġ
    public int gold;//���
    public List<Item> rewardItems;//���� ������ ����Ʈ
    public bool expAllGiven;//����ġ�� ��� �־������� ���� ����ġ�� 0�̵�

    // (������ ���,monster list�� �����ִ� �����͸� ������� ������ ���� ����
    // (����ġ�� ������, ���� �⺻ +-random��ġ��,������ ��� ������ ������ �� Ȯ���� ����
    // ���� ���� 0~2�� ���� ���ں����� �߰� Ȯ���� ����)
    private void Start()
    {
        expAllGiven = false;
        HideReward();
    }


    public void SetReward()//������ �������ִ� �Լ�, ���� ���ĵ� ����
    {
        expAllGiven = false;//����ġ �����ϸ鼭, �ʱ�ȭ ���ֱ�
        //exp =...����
    }
    public void ShowReward()//������ �����ִ� �Լ�, ���÷��̿� ���� ǥ��
    {
        Debug.Log("����ǥ����");
    }
    public void HideReward()//������ ����� �Լ�, ���÷��̿� ���� ����
    {

    }
}
