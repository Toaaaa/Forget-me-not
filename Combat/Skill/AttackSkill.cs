using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : PlayerSkill //��� ĳ������ �⺻ ����
{


    public void targetLocked()
    {
        //���⼭ ��ų�� ����Ʈ or �ִϸ��̼� ���.(player or target�� ������ ������ �ֱ⿡ �÷��̾� or ���ʿ� �ִϸ��̼�, ����Ʈ ��� ����
        //on enable�� �ƴ϶� ���⼭ �����ؾ� �ϴ� ������, on enable���� �������� �����Ǿ� ���� �ʰų�, �ʱ�ȭ �Ǳ� ���̶�, ������ ������� ����.
        if (targetMob != null)
           transform.DOMove(targetMob.transform.position, 0.8f).SetEase(Ease.Linear);
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
            Debug.Log("������ �������� �����ϴ�.");
            player.AttackDmgCalc(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);//������ ��� �ȿ� destroy ����.
        }
    }
}
