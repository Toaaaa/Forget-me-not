using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill3 : PlayerSkill //큐어(일단은 독만 해제)
{
    public void targetLocked()
    {
        if (targetPlayer != null)
            transform.DOMove(targetplayerPlace.gameObject.transform.position, 0.5f).SetEase(Ease.Linear); //0.5초안에 이동.
        else
        {
            Debug.Log("타겟이 없습니다.");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CombatSlot>().player == targetPlayer)
        {
            if (!targetPlayer.isDead)
            {
                Debug.Log("큐어");
                targetPlayer.isPoisoned = false;
            }
            Destroy(gameObject);
        }
    }
}
