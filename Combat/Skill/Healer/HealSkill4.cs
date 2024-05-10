using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill4 : PlayerSkill //(리저렉션)
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
            if (targetPlayer.isDead)
            {
                targetPlayer.hp = targetPlayer.maxHp * 0.5f;
                targetPlayer.isDead = false;
                targetPlayer.isStunned = false;
                targetPlayer.isPoisoned = false;
            }
            else
            {
                targetPlayer.hp += targetPlayer.maxHp * 0.5f;
                if (targetPlayer.hp > targetPlayer.maxHp)
                {
                    targetPlayer.hp = targetPlayer.maxHp;
                    CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.transform.position, WhenMaxHpPrint(player), false, true);
                }
                else
                {
                    CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.transform.position, targetPlayer.maxHp * 0.5f, false, true);
                }
                Debug.Log("이미 살아있는 대상, 절반의 체력 회복.");
            }
            Destroy(gameObject);
        }
    }
    private float WhenMaxHpPrint(PlayableC player) //힐량이 최대 체력을 넘어갈때, 얼마나 회복되는지 출력.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}
