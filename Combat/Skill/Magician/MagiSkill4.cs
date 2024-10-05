using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MagiSkill4 : PlayerSkill//피어싱 라이트닝
{
    public void targetLocked()
    {

        if (targetMob != null)
            transform.DOMove(targetMob.GetComponent<TestMob>().bottomPos.transform.position, 1.3f).SetEase(Ease.Linear);//투사체1.3초 동안 이동
        else
        {
            Destroy(gameObject);
        }
    }
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetMob.gameObject)
        {
            player.InGamePrefab.GetComponent<PlayerSFX>().PlayerSfx4OnMonster(targetMob.GetComponent<TestMob>().bottomPos.transform);//BottomPos는 몬스터 스프라이트의 하단 중앙부에 위치함.
            await UniTask.Delay(900);//천둥 애니메이션 재생후 데미지 모션과 적용의 동기화를 위한 딜레이.
            player.SkillDmgCalc4(this.gameObject);//여기에 데미지 출력 효과도 포함되어있음.
            //Destroy(gameObject);
        }
    }
}
