using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiSkill2 : PlayerSkill//�ð� ����ȭ
{
    public void targetLocked()
    {
        if (targetMob != null)
            transform.DOMove(targetMob.transform.position, 0.5f).SetEase(Ease.Linear);//����ü0.25�ʵ��� �̵�
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetMob.gameObject)
        {
            player.InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx2OnMonster(collision.gameObject.transform);//�÷��̾��� ��ġ�� ��ųȿ�� ǥ��
            player.SkillDmgCalc2(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);
        }
    }
}
