using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public  async UniTask Attack(string animationName)//�ش� �̸��� ���� ���� �ִϸ��̼��� ����
    {
        animator.SetBool("attacking", true);
        Debug.Log("���Ͱ� ������ �����մϴ�");

        // ���� �ִϸ��̼��� ���� ������ ���
        await WaitForAnimationToComplete(animationName);

        // idle ���·� ��ȯ
        animator.SetBool("attacking", false);
    }

    public async UniTask Death(string animationName)
    {
        animator.SetTrigger("death");

        await WaitForAnimationToComplete(animationName);

        Debug.Log("���Ͱ� �׾����ϴ�");
        CombatManager.Instance.monsterAliveList.Remove(this.gameObject);
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
