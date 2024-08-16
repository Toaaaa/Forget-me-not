using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiSkill2 : PlayerSkill//ġ��Ÿ ����
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
        if (collision.tag == "Skill")
        {
            return;
        }
        if (collision.gameObject.tag == "Mob")
        {
            player.SkillDmgCalc2(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);
        }
    }
}
