using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class CombatBuffs : MonoBehaviour
{
    public PlayableC player;
    public Sprite currentBuff;
    public List<Sprite> buffdebuffSprites;


    public void BuffCheck(int num)
    {
        switch (num)
        {
            case 0://���� ������
                currentBuff = buffdebuffSprites[0]; //���� �̹���
                break;
            case 1://���ݷ� ����
                checkBuffType();
                break;
            case 2://�÷��̾� ����
                currentBuff = buffdebuffSprites[5];
                break;
            case 3://�÷��̾� �ߵ�
                currentBuff = buffdebuffSprites[6];
                break;
            case 4://��ų ���
                currentBuff = buffdebuffSprites[7];
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
            currentBuff = buffdebuffSprites[1];
        }
        else if(player.defenseBuff == true)
        {
            currentBuff = buffdebuffSprites[2];
        }
        else if(player.speedBuff == true)
        {
            currentBuff = buffdebuffSprites[3];
        }
        else if(player.critBuff == true)
        {
            currentBuff = buffdebuffSprites[4];
        }
    }
}
