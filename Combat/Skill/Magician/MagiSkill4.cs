using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MagiSkill4 : PlayerSkill//�Ǿ�� ����Ʈ��
{
    public void targetLocked()
    {
        if (targetMob != null)
            transform.DOMove(targetMob.transform.position, 0.5f).SetEase(Ease.Linear);//����ü0.5�ʵ��� �̵�
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetMob.gameObject)
        {
            player.InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4OnMonster(collision.gameObject.transform);
            player.SkillDmgCalc4(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);
        }
    }
}
