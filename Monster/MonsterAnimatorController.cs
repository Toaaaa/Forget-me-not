using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Animator shadowAnimator;//�׸����� �ִϸ�����

    private void Start()
    {
        animator = GetComponent<Animator>();
        shadowAnimator = GetComponent<TestMob>().shadowAnimator;
    }
    
    //�Ʒ� �޼������ ȣ��� @@@.Forget()�� �ٿ��־�� ��.
    public  async UniTask Attack(string animationName)//�ش� �̸��� ���� ���� �ִϸ��̼��� ����
    {
        animator.SetBool("attacking", true);

        // ���� �ִϸ��̼��� ���� ������ ���
        await WaitForAnimationToComplete(animationName);

        // idle ���·� ��ȯ
        animator.SetBool("attacking", false);
    }

    public async UniTask Death(string animationName)
    {
        animator.SetTrigger("death");
        shadowAnimator.GetComponent<MonsterShadowAnimator>().SettingTriger();//death�� Ʈ���� ����.
        try { CombatManager.Instance.monsterAliveList.Remove(this.gameObject); }//���Ͱ� ���� ��� ��� ����Ʈ���� �����Ͽ�, ȭ��ǥ�� �������� ���� ȥ�� ������ �ϱ�.
        catch (ObjectDisposedException e)
        { Console.WriteLine("Caught: {0}", e.Message);}

        await WaitForAnimationToComplete(animationName);

        this.gameObject.SetActive(false);
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
