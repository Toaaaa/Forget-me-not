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
            transform.DOMove(targetplayerPlace.gameObject.transform.position, 0.5f).SetEase(Ease.Linear); //0.5초안에 이동.
        else
        {
            Debug.Log("타겟이 없습니다.");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌이 감지되었습니다.");
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
    private float WhenMaxHpPrint(PlayableC player) //힐량이 최대 체력을 넘어갈때, 얼마나 회복되는지 출력.
    {
        float print;
        print = player.maxHp - player.hp;
        return print;
    }
}
