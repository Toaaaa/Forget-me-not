using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill1 : PlayerSkill //�ӽ÷� �������� ��ų 1���� ���� ������Ÿ�� ��ũ��Ʈ.
{

    public void targetLocked()
    {
        if (targetMob != null)
            transform.DOMove(targetMob.transform.position, 1f).SetEase(Ease.Linear);
        else
        {
            Debug.Log("Ÿ���� �����ϴ�.");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹�� �����Ǿ����ϴ�.");
        if (collision.gameObject.tag == "Mob")
        {
            player.SkillDmgCalc1(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);
        }
    }
}
