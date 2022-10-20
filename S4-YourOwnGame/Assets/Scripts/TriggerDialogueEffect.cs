using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueEffect : TriggerEffect
{
    [SerializeField] string DialogueName = string.Empty;

    public override void EndEffect() { }

    public override void StartEffect() => DialogueManager.instance.AddDialogueToQueue(DialogueName);
}
