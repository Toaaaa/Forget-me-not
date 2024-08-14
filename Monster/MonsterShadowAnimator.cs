using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShadowAnimator : MonoBehaviour
{
    public Animator mainAnimator;
    public Animator followerAnimator;

    void Start()
    {
        // 본체의 Animator Controller를 따라하는 오브젝트에 설정
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
