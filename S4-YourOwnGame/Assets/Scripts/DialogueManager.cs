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
    //[SerializeField] float secondsBetweenDialogue = 5f;

    //List<DialogueRecord> dialogueQueue = new();
    List<Dialogue> Queue = new();
    DialogueFile Text;

    private void Start()
    {
        if (DialogueFile != null)
            Text = JsonUtility.FromJson<DialogueFile>(DialogueFile.text);
        instance = this;
        StartCoroutine(DisplayDialogue());
        //StartCoroutine(ContinueQueue());
    }

    public void AddDialogueToQueue(string DialogueName)
    {
        Dialogue Line = Text.GetDialogueByName(DialogueName, SelectedLanguage);
        if (Line != null)
            Queue.Add(Line);
    }

    //PROBLEM: WaitForSeconds is not accurate at values below 1.
    //IEnumerator ContinueQueue()
    //{
    //    yield return new WaitUntil(() => dialogueQueue.Any());

    //    float timeBetweenCharacters = dialogueQueue[0].typingTimeInSeconds / dialogueQueue[0].text.Length;
    //    Debug.Log(timeBetweenCharacters);
    //    AudioSource.PlayClipAtPoint(dialogueQueue[0].audio, Camera.main.transform.position);

    //    foreach (char character in dialogueQueue[0].text)
    //    {
    //        subtitleText.text += character;
    //        yield return new WaitForSeconds(timeBetweenCharacters);
    //    }

    //    yield return new WaitForSecondsRealtime(dialogueQueue[0].remainTimeInSeconds);

    //    subtitleText.text = "";

    //    yield return new WaitForSecondsRealtime(secondsBetweenDialogue);

    //    dialogueQueue.RemoveAt(0);

    //    StartCoroutine(ContinueQueue());
    //}

    private IEnumerator DisplayDialogue()
    {
        yield return new WaitUntil(() => Queue.Any());
        SubtitleBackground.SetActive(true);

        while (Queue.Count > 0)
        {
            subtitleText.text = Queue[0].String;
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
