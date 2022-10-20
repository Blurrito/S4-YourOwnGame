using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    [SerializeField] DialogueRecord dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AddtoDialogueQueue();
    }

    [ContextMenu("Add to queue")]
    public void AddtoDialogueQueue()
    {
        DialogueManager.instance.AddDialogueToQueue(dialogue);
    }
}
