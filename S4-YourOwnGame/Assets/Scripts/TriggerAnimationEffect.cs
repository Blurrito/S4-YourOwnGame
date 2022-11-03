using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationEffect : TriggerEffect
{
    [SerializeField] string PropertyName = string.Empty;
    [SerializeField] bool IsTrigger = false;

    public Animator Animator = null;
    public bool IsPlaying = false;

    private void Start()
    {
        if (Animator == null) Animator = gameObject.GetComponent<Animator>();
    }

    public override void EndEffect()
    {
        if (IsPlaying)
        {
            //IsPlaying = false;
            IsPlaying = !IsPlaying;
            if (!IsTrigger)
                Animator.SetBool(PropertyName, IsPlaying);
        }
    }

    public override void StartEffect()
    {
        IsPlaying = !IsPlaying;
        if (!IsTrigger)
            Animator.SetBool(PropertyName, IsPlaying);
        Animator.SetTrigger(PropertyName);
    }
}
