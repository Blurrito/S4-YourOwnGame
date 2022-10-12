using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPopupTrigger : MonoBehaviour
{
    [SerializeField] string hintText;
    [SerializeField] float staySeconds = 10;
    [SerializeField, Tooltip("Set to -1 to show infinitely.")] int amountToShowBeforeDestroy = 1;

    bool currentlyShowingThisPopup = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (currentlyShowingThisPopup) return;

        currentlyShowingThisPopup = true;
        Debug.Log(hintText);
        //TODO: instead of debug log, add to list of popups in canvas

        amountToShowBeforeDestroy--;
        Invoke(nameof(StopShowingPopup), staySeconds);
    }

    private void StopShowingPopup()
    {
        Debug.Log("Hint disabled");
        //TODO: instead of debug log, remove from list of popups in canvas

        currentlyShowingThisPopup = false;

        if (amountToShowBeforeDestroy == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
