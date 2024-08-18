using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiSkill3 : PlayerSkill //�ӵ����� ������
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
        if (collision.gameObject == targetMob.gameObject)
        {
            player.MultiDmg3(player, targetMob);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            targetMob.playerSFX[0].gameObject.SetActive(false);
            targetMob.playerSFX[0].gameObject.SetActive(true);
            //���ο� ȿ�� ���
            Destroy(gameObject);
        }
    }
}
