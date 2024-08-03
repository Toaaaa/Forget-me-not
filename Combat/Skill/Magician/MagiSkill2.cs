using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiSkill2 : PlayerSkill//ġ��Ÿ ����
{
    public void targetLocked()
    {
        if (targetPlayer != null)
            transform.DOMove(targetplayerPlace.playerPrefab.transform.position, 0.5f).SetEase(Ease.Linear); //0.5�ʾȿ� �̵�.
        else
        {
            Debug.Log("Ÿ���� �����ϴ�.");
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
                    Debug.Log("�̹� ġ��Ÿ ������ �������Դϴ�.");
                }
                Destroy(gameObject);
            }
        }
    }
}
