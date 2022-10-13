using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPopupTrigger : MonoBehaviour
{
    [SerializeField] string hintText;
    [SerializeField] float staySeconds = 10;
    [SerializeField, Tooltip("Set to -1 to show infinitely.")] int amountToShowBeforeDestroy = 1;

    GameObject createdPopup;

    bool currentlyShowingThisPopup = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (currentlyShowingThisPopup) return;

        currentlyShowingThisPopup = true;
        var newPopup = DialogueManager.instance.AddHintPopup(hintText);

        amountToShowBeforeDestroy--;
        Invoke(nameof(StopShowingPopup), staySeconds);

        createdPopup = newPopup;
    }

    private void StopShowingPopup()
    {
        Destroy(createdPopup);

        currentlyShowingThisPopup = false;

        if (amountToShowBeforeDestroy == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
