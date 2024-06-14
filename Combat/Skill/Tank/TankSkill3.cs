using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSkill3 : PlayerSkill //(땅울리기)
{
    public void targetLocked()
    {
        //여기서 스킬의 이펙트 or 애니메이션 재생.(player or target의 값또한 가지고 있기에 플레이어 or 적쪽에 애니메이션, 이펙트 재생 가능
        //on enable이 아니라 여기서 구현해야 하는 이유는, on enable때는 변수들이 지정되어 있지 않거나, 초기화 되기 전이라, 오류가 생길수도 있음.
        if (targetMob != null)
            transform.DOMove(targetMob.transform.position, 1f).SetEase(Ease.Linear);
        else
        {
            Debug.Log("타겟이 없습니다.");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌이 감지되었습니다.");
        if (collision.gameObject.tag == "Mob")
        {
            player.MultiDmg3(player,targetMob);//여기에 데미지 출력 효과도 포함되어있음.
            //피격시 방깍 이펙트 출력.
            Destroy(gameObject);
        }
    }
}
