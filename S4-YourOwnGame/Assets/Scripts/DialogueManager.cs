using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] TextMeshProUGUI subtitleText;
    [SerializeField] GameObject popupTemplate;
    [SerializeField] Transform popupContainer;

    List<(string text, float staySeconds)> subtitleQueue;

    private void Start()
    {
        instance = this;
    }

    public void AddSubtitleToqueue(string text, float staySeconds)
    {
        subtitleQueue.Add((text, staySeconds));
    }

    //TODO: create queue playing system



    public GameObject AddHintPopup(string text)
    {
        var newPopup = Instantiate(popupTemplate, popupContainer);
        newPopup.transform.Find("HintText").GetComponent<TextMeshProUGUI>().text = text;
        newPopup.SetActive(true);

        return newPopup;
    }
}
