using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShadowAnimator : MonoBehaviour
{
    public Animator mainAnimator;
    public Animator followerAnimator;

    void Start()
    {
        // ��ü�� Animator Controller�� �����ϴ� ������Ʈ�� ����
        followerAnimator.runtimeAnimatorController = mainAnimator.runtimeAnimatorController;

    }

    private void Update()
    {
        followerAnimator.SetBool("attacking", mainAnimator.GetBool("attacking"));
        followerAnimator.SetInteger("skillNum", mainAnimator.GetInteger("skillNum"));
    }

    public void SettingTriger()
    {
        followerAnimator.SetTrigger("death");
    }
}
