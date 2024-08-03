using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiSkill2 : PlayerSkill//치명타 버프
{
    public void targetLocked()
    {
        if (targetPlayer != null)
            transform.DOMove(targetplayerPlace.playerPrefab.transform.position, 0.5f).SetEase(Ease.Linear); //0.5초안에 이동.
        else
        {
            Debug.Log("타겟이 없습니다.");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Skill")
        {
            return;
        }
        if (collision.tag == "PlayerPrefab")
        {
            if (targetPlayer != null && collision.GetComponent<CharacterPrefab>().player == targetPlayer)
            {
                if(!targetPlayer.critBuff)
                {
                    targetPlayer.crit += 15;
                    targetPlayer.critBuff = true;
                }
                else
                {
                    Debug.Log("이미 치명타 버프가 적용중입니다.");
                }
                Destroy(gameObject);
            }
        }
    }
}
