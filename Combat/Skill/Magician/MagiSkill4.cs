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
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            await UniTask.Delay(1000);//���� õ�� ����Ʈ�� ���缭 ������ ����.
            player.SkillDmgCalc4(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);
        }
    }
}
