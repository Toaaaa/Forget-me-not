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

    //�Ʒ� �޼������ ȣ��� @@@.Forget()�� �ٿ��־�� ��.
    public async UniTask Attack(string animationName)//�ش� �̸��� ���� ���� �ִϸ��̼��� ����
    {
        animator.SetBool("attacking", true);

        // ���� �ִϸ��̼��� ���� ������ ���
        await WaitForAnimationToComplete(animationName);

        // idle ���·� ��ȯ
        animator.SetBool("attacking", false);
    }
    public void Hit(string animationName) //�ǰݽ� �ִϸ��̼� ���
    {
        animator.SetTrigger("hit");
        // �ǰݽ� ��¦�̴� ȿ��.
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
        // �ִϸ��̼� ���°� ���۵� ������ ���
        while (!IsAnimationPlaying(animationName))
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        // �ִϸ��̼� ���°� ����� ������ ���
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
