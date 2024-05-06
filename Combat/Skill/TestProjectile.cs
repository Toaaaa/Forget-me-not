using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : PlayerSkill //�ӽ÷� �������� ��ų 1���� ���� ������Ÿ�� ��ũ��Ʈ.
{

    private void Start()
    {
        Debug.Log("������Ÿ���� �����Ǿ����ϴ�.");
    }
    public void targetLocked()
    {
        if (targetMob != null)
            transform.DOMove(targetMob.GetComponent<RectTransform>().position, 1f).SetEase(Ease.Linear);
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
            targetMob = collision.gameObject.GetComponent<TestMob>();
            Debug.Log(collision.gameObject.name + "�� ���� �Ͽ����ϴ�");
            player.SkillDmgCalc1();//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            Destroy(gameObject);
        }
    }
}
