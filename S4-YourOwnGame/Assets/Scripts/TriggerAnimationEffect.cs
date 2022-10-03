using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationEffect : TriggerEffect
{
    [SerializeField] Animator Animator;
    [SerializeField] string PropertyName = string.Empty;

    private bool IsPlaying = false;

    public override void EndEffect()
    {
        if (IsPlaying)
        {
            IsPlaying = false;
            Animator.SetBool(PropertyName, false);
        }
    }

    public override void StartEffect()
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
            Animator.SetBool(PropertyName, true);
        }
    }
}
