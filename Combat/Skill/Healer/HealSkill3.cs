using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill3 : PlayerSkill //ť��(�ϴ��� ���� ����)
{
    public void targetLocked()
    {
        if (targetMob != null)
        {
            transform.DOMove(targetMob.transform.position, 0.1f).SetEase(Ease.Linear);
        }
        else
        {
            Debug.Log("Ÿ���� �����ϴ�.");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            player.HolyRayDmgCalc(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);
        }
            
    }
}
