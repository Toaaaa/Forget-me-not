using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill3 : PlayerSkill //ť��(�ϴ��� ���� ����)
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
        if (collision.tag == "PlayerPrefab")
        {
            if (targetPlayer != null && collision.GetComponent<CharacterPrefab>().player == targetPlayer)
            {
                if (!targetPlayer.isDead)
                {
                    Debug.Log("ť��");
                    targetPlayer.isPoisoned = false;
                }
                Destroy(gameObject);
            }
        }
            
    }
}
