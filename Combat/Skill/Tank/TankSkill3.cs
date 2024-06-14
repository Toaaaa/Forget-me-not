using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSkill3 : PlayerSkill //(���︮��)
{
    public void targetLocked()
    {
        //���⼭ ��ų�� ����Ʈ or �ִϸ��̼� ���.(player or target�� ������ ������ �ֱ⿡ �÷��̾� or ���ʿ� �ִϸ��̼�, ����Ʈ ��� ����
        //on enable�� �ƴ϶� ���⼭ �����ؾ� �ϴ� ������, on enable���� �������� �����Ǿ� ���� �ʰų�, �ʱ�ȭ �Ǳ� ���̶�, ������ ������� ����.
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
            player.MultiDmg3(player,targetMob);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //�ǰݽ� ��� ����Ʈ ���.
            Destroy(gameObject);
        }
    }
}
