using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    [SerializeField] string DialogueName = string.Empty;
    [SerializeField] bool playOnce = false;
    bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed && playOnce) return;
        hasPlayed = true;

        if (!other.CompareTag("Player")) return;

        AddtoDialogueQueue();
    }

    [ContextMenu("Add to queue")]
    public void AddtoDialogueQueue()
    {
        DialogueManager.instance.AddDialogueToQueue(DialogueName);
    }
}
