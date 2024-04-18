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
            case 0://버프 미적용 (공격력)
                noBuff();
                break;
            case 1://공격력 버프
                checkBuffType();
                break;
            case 2://플레이어 스턴
                playerStun();
                break;
            case 3://플레이어 중독
                playerPoison();
                break;
            case 4://스킬 잠금
                //별다른 효과 없을예정.
                break;
            case 5://버프 미적용 (스턴)
                noStun();
                break;
            case 6://버프 미적용 (중독)
                noPoison();
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
            // 해당 캐릭터의 주변에 빨간색 빛이 나타나는 애니메이션. ( 정확히는 전부다 애니메이션의 파라미터를 on 으로 바꿔주기만 하기)
        }
        else if(player.defenseBuff == true)
        {
            // 해당 캐릭터의 주변에 노란색 빛이 나타나는 애니메이션.
        }
        else if(player.speedBuff == true)
        {
            // 해당 캐릭터의 주변에 파란색 빛이 나타나는 애니메이션.
        }
        else if(player.critBuff == true)
        {
            // 해당 캐릭터의 주변에 초록색 빛이 나타나는 애니메이션.
        }
    }
    private void noBuff()
    {

    }
    private void playerStun() //플레이어 위에 별이 회전하는 애니메이션
    {

    }
    private void playerPoison() //플레이어 ui 테두리 효과
    {

    }
    private void noStun()
    {

    }
    private void noPoison()
    {

    }
}
