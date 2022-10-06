using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnabledEffect : TriggerEffect
{
    [SerializeField] public bool EnableOnTrigger = true;

    public override void EndEffect() => gameObject.SetActive(!EnableOnTrigger);

    public override void StartEffect() => gameObject.SetActive(EnableOnTrigger);
}
