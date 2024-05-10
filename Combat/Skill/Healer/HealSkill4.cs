using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill4 : PlayerSkill //(��������)
{
    public void targetLocked()
    {
        if (targetPlayer != null)
            transform.DOMove(targetplayerPlace.gameObject.transform.position, 0.5f).SetEase(Ease.Linear); //0.5�ʾȿ� �̵�.
        else
        {
            Debug.Log("Ÿ���� �����ϴ�.");
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
                Debug.Log("�̹� ����ִ� ���, ������ ü�� ȸ��.");
            }
            Destroy(gameObject);
        }
    }
    private float WhenMaxHpPrint(PlayableC player) //������ �ִ� ü���� �Ѿ��, �󸶳� ȸ���Ǵ��� ���.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}
