using UnityEngine;
using System.Collections;

public static class AnimationExtensions
{

    public static IEnumerator PlayAnimation(this Animator animator,
                                                   int state)
    {
        animator.SetInteger("AnimState", state);

        do
        {
            yield return null;
        } while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Default"));

        animator.SetInteger("AnimState", 0);

    }

}