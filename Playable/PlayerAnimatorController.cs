using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //아래 메서드들은 호출시 @@@.Forget()을 붙여주어야 함.
    public async UniTask Attack(string animationName)//해당 이름을 가진 공격 애니메이션을 실행
    {
        animator.SetBool("attacking", true);

        // 공격 애니메이션이 끝날 때까지 대기
        await WaitForAnimationToComplete(animationName);

        // idle 상태로 전환
        animator.SetBool("attacking", false);
    }
    public void Hit(string animationName) //피격시 애니메이션 재생
    {
        animator.SetTrigger("hit");
        // 피격시 반짝이는 효과.
    }

    public void Death()
    {
        animator.SetBool("death",true);
    }

    public void Revive()
    {
        animator.SetBool("death", false);
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
