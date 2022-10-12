using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    [SerializeField] string text;
    [SerializeField] float staySeconds = 10;
    [SerializeField, Tooltip("Set to -1 to show infinitely.")] int amountToShowBeforeDestroy = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        DialogueManager.instance.AddSubtitleToqueue(text, staySeconds);
    }
}
