using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiSkill2 : PlayerSkill//시간 동기화
{
    public void targetLocked()
    {
        if (targetMob != null)
            transform.DOMove(targetMob.transform.position, 0.5f).SetEase(Ease.Linear);//투사체0.25초동안 이동
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetMob.gameObject)
        {
            player.InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx2OnMonster(collision.gameObject.transform);//플레이어의 위치에 스킬효과 표시
            player.SkillDmgCalc2(this.gameObject);//여기에 데미지 출력 효과도 포함되어있음.
            //Destroy(gameObject);
        }
    }
}
