using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] TextMeshProUGUI textObject;
    List<(string text, float staySeconds)> subtitleQueue;

    private void Start()
    {
        instance = this;
    }

    public void AddSubtitleToqueue(string text, float staySeconds)
    {
        subtitleQueue.Add((text, staySeconds));
    }
}
