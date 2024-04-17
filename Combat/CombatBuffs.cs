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
            case 0://버프 미적용
                currentBuff = buffdebuffSprites[0]; //투명 이미지
                break;
            case 1://공격력 버프
                checkBuffType();
                break;
            case 2://플레이어 스턴
                currentBuff = buffdebuffSprites[5];
                break;
            case 3://플레이어 중독
                currentBuff = buffdebuffSprites[6];
                break;
            case 4://스킬 잠금
                currentBuff = buffdebuffSprites[7];
                break;
            default:
                break;
        }

        //플레이어의 버프 디버프 상태를 체크해 이미지 변경.
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
