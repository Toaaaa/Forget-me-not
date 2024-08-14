using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Animator shadowAnimator;//그림자의 애니메이터

    private void Start()
    {
        animator = GetComponent<Animator>();
        shadowAnimator = GetComponent<TestMob>().shadowAnimator;
    }

    public  async UniTask Attack(string animationName)//해당 이름을 가진 공격 애니메이션을 실행
    {
        animator.SetBool("attacking", true);
        Debug.Log("몬스터가 공격을 시작합니다");

        // 공격 애니메이션이 끝날 때까지 대기
        await WaitForAnimationToComplete(animationName);

        // idle 상태로 전환
        animator.SetBool("attacking", false);
    }

    public async UniTask Death(string animationName)
    {
        animator.SetTrigger("death");
        shadowAnimator.GetComponent<MonsterShadowAnimator>().SettingTriger();//death의 트리거 실행.

        await WaitForAnimationToComplete(animationName);

        Debug.Log("몬스터가 죽었습니다");
        CombatManager.Instance.monsterAliveList.Remove(this.gameObject);
        this.gameObject.SetActive(false);
    }


    private async UniTask WaitForAnimationToComplete(string animationName)
    {
        // 애니메이션 상태가 시작될 때까지 대기
        while (!IsAnimationPlaying(animationName))
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
 
        // 애니메이션 상태가 종료될 때까지 대기
        while (IsAnimationPlaying(animationName))
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f;
    }
}
