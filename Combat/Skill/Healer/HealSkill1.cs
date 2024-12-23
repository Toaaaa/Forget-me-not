using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealSkill1 : PlayerSkill //(단일 힐)
{
    public void targetLocked()
    {
        if (targetPlayer != null)
            transform.DOMove(targetplayerPlace.playerPrefab.transform.position, 0.5f).SetEase(Ease.Linear); //0.5초안에 이동.
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerPrefab")
        {
            if (targetPlayer != null && collision.GetComponent<CharacterPrefab>().player == targetPlayer)
            {
                targetPlayer.hp += playerAtk * 3f;
                if (targetPlayer.hp > targetPlayer.maxHp)
                {
                    CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.gameObject, WhenMaxHpPrint(player), false, true);
                    targetPlayer.hp = targetPlayer.maxHp;
                }
                else
                {
                    CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.gameObject, player.atk * 3f, false, true);
                }
                Destroy(gameObject);
            }
        }
    }
    private float WhenMaxHpPrint(PlayableC player) //힐량이 최대 체력을 넘어갈때, 얼마나 회복되는지 출력.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}
