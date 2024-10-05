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
            transform.DOMove(targetMob.GetComponent<TestMob>().bottomPos.transform.position, 1.3f).SetEase(Ease.Linear);//����ü1.3�� ���� �̵�
        else
        {
            Destroy(gameObject);
        }
    }
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetMob.gameObject)
        {
            player.InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4OnMonster(targetMob.GetComponent<TestMob>().bottomPos.transform);//BottomPos�� ���� ��������Ʈ�� �ϴ� �߾Ӻο� ��ġ��.
            await UniTask.Delay(900);//õ�� �ִϸ��̼� ����� ������ ��ǰ� ������ ����ȭ�� ���� ������.
            player.SkillDmgCalc4(this.gameObject);//���⿡ ������ ��� ȿ���� ���ԵǾ�����.
            //Destroy(gameObject);
        }
    }
}
