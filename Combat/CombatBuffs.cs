using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class CombatBuffs : MonoBehaviour
{
    public PlayableC player;


    private void Update()
    {
        
    }


    public void BuffCheck(int num)
    {
        switch (num)
        {
            case 0://���� ������ (���ݷ�)
                noBuff();
                break;
            case 1://���ݷ� ����
                checkBuffType();
                break;
            case 2://�÷��̾� ����
                playerStun();
                break;
            case 3://�÷��̾� �ߵ�
                playerPoison();
                break;
            case 4://��ų ���
                //���ٸ� ȿ�� ��������.
                break;
            case 5://���� ������ (����)
                noStun();
                break;
            case 6://���� ������ (�ߵ�)
                noPoison();
                break;
            default:
                break;
        }

        //�÷��̾��� ���� ����� ���¸� üũ�� �̹��� ����.
    }
    private void checkBuffType()
    {
        if(player.attackBuff == true)
        {
            // �ش� ĳ������ �ֺ��� ������ ���� ��Ÿ���� �ִϸ��̼�. ( ��Ȯ���� ���δ� �ִϸ��̼��� �Ķ���͸� on ���� �ٲ��ֱ⸸ �ϱ�)
        }
        else if(player.defenseBuff == true)
        {
            // �ش� ĳ������ �ֺ��� ����� ���� ��Ÿ���� �ִϸ��̼�.
        }
        else if(player.speedBuff == true)
        {
            // �ش� ĳ������ �ֺ��� �Ķ��� ���� ��Ÿ���� �ִϸ��̼�.
        }
        else if(player.critBuff == true)
        {
            // �ش� ĳ������ �ֺ��� �ʷϻ� ���� ��Ÿ���� �ִϸ��̼�.
        }
    }
    private void noBuff()
    {

    }
    private void playerStun() //�÷��̾� ���� ���� ȸ���ϴ� �ִϸ��̼�
    {

    }
    private void playerPoison() //�÷��̾� ui �׵θ� ȿ��
    {

    }
    private void noStun()
    {

    }
    private void noPoison()
    {

    }
}
