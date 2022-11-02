using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] GameObject SubtitleBackground;
    [SerializeField] GameObject popupTemplate;
    [SerializeField] Transform popupContainer;
    [SerializeField] TextAsset DialogueFile;
    [SerializeField] string SelectedLanguage;

    List<Dialogue> Queue = new();
    DialogueFile Text;

    private void Start()
    {
        if (DialogueFile != null)
            Text = JsonUtility.FromJson<DialogueFile>(DialogueFile.text);
        instance = this;
        StartCoroutine(DisplayDialogue());
    }

    public void AddDialogueToQueue(string DialogueName)
    {
        Dialogue Line = Text.GetDialogueByName(DialogueName, SelectedLanguage);
        if (Line != null)
            Queue.Add(Line);
    }

    private IEnumerator DisplayDialogue()
    {
        yield return new WaitUntil(() => Queue.Any());
        SubtitleBackground.SetActive(true);

        while (Queue.Count > 0)
        {
            subtitleText.text = Queue[0].String;
            if (Queue[0].AudioClip != string.Empty)
                AudioManager.instance.PlayAudioClip(Queue[0].AudioClip);
            yield return new WaitForSecondsRealtime(Queue[0].ScreenTime);
            Queue.RemoveAt(0);
        }

        SubtitleBackground.SetActive(false);
        StartCoroutine(DisplayDialogue());
    }

    public GameObject AddHintPopup(string text)
    {
        var newPopup = Instantiate(popupTemplate, popupContainer);
        newPopup.transform.Find("HintText").GetComponent<TextMeshProUGUI>().text = text;
        newPopup.SetActive(true);

        return newPopup;
    }

    public void AddHintPopupAndDestroy(string text, float time)
    {
        var newPopup = Instantiate(popupTemplate, popupContainer);
        newPopup.transform.Find("HintText").GetComponent<TextMeshProUGUI>().text = text;
        newPopup.SetActive(true);
        StartCoroutine(DestroyAfterTime(newPopup, time));
    }

    public IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(obj);
    }
}

[System.Serializable]
public class DialogueRecord
{


    public string text;
    public float typingTimeInSeconds;
    public float remainTimeInSeconds;
    public AudioClip audio;

    public DialogueRecord(string text, float typingTimeInSeconds, float remainTimeInSeconds, AudioClip audio)
    {
        this.text = text;
        this.typingTimeInSeconds = typingTimeInSeconds;
        this.remainTimeInSeconds = remainTimeInSeconds;
        this.audio = audio;
    }
}
