using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill2 : PlayerSkill //(������)
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
        if(collision.tag == "Skill")
        {
            return;
        }
        if (collision.tag == "PlayerPrefab")
        {
            if (targetPlayer != null && collision.GetComponent<CharacterPrefab>().player == targetPlayer)
            {
                if (!targetPlayer.isDead)
                {
                    targetPlayer.hp += player.atk * 2f;
                    if (targetPlayer.hp > targetPlayer.maxHp)
                    {
                        targetPlayer.hp = targetPlayer.maxHp;
                        CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.gameObject, WhenMaxHpPrint(player), false, true);
                    }
                    else
                    {
                        CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.gameObject, player.atk * 2f, false, true);
                    }
                }
                Destroy(gameObject);
            }
        }           
    }
    private float WhenMaxHpPrint(PlayableC player) //������ �ִ� ü���� �Ѿ��, �󸶳� ȸ���Ǵ��� ���.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}
