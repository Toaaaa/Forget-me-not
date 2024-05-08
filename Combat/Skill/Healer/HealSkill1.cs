using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealSkill1 : PlayerSkill
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
        Debug.Log("�浹�� �����Ǿ����ϴ�.");
        if (collision.GetComponent<CombatSlot>().player == targetPlayer)
        {
            CombatManager.Instance.selectedPlayer.hp += player.atk * 3f;
            if (CombatManager.Instance.selectedPlayer.hp > CombatManager.Instance.selectedPlayer.maxHp)
            {
                CombatManager.Instance.damagePrintManager.PrintDamage(targetplayerPlace.transform.position, WhenMaxHpPrint(player), false, true);
                CombatManager.Instance.selectedPlayer.hp = CombatManager.Instance.selectedPlayer.maxHp;
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
