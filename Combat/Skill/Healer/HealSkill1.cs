using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealSkill1 : PlayerSkill //(���� ��)
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
            targetPlayer.hp += player.atk * 3f;
            if (targetPlayer.hp > targetPlayer.maxHp)
            {
                CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.transform.position, WhenMaxHpPrint(player), false, true);
                targetPlayer.hp = targetPlayer.maxHp;
            }
            else
            {
                CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.transform.position, player.atk * 3f, false, true);
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
