using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill1 : PlayerSkill //임시로 워리어의 스킬 1번의 전용 프로젝타일 스크립트.
{

    public void targetLocked()
    {
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
            player.SkillDmgCalc1();//여기에 데미지 출력 효과도 포함되어있음.
            Destroy(gameObject);
        }
    }
}
