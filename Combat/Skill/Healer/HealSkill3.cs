using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill3 : PlayerSkill //큐어(일단은 독만 해제)
{
    public void targetLocked()
    {
        if (targetMob != null)
        {
            transform.DOMove(targetMob.transform.position, 0.1f).SetEase(Ease.Linear);
        }
        else
        {
            Debug.Log("타겟이 없습니다.");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            player.HolyRayDmgCalc(this.gameObject);//여기에 데미지 출력 효과도 포함되어있음.
            //Destroy(gameObject);
        }
            
    }
}
