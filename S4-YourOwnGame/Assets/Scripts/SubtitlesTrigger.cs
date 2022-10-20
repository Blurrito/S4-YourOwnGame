using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    [SerializeField] string DialogueName = string.Empty;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AddtoDialogueQueue();
    }

    [ContextMenu("Add to queue")]
    public void AddtoDialogueQueue()
    {
        DialogueManager.instance.AddDialogueToQueue(DialogueName);
    }
}
