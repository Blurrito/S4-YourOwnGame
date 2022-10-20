using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial_Clone : MonoBehaviour
{
    [SerializeField] InputActionAsset inputAsset;

    [SerializeField] string createCloneActionName;
    [SerializeField] string cancelCloneActionName;
    [SerializeField] string retryCloneActionName;

    [SerializeField] ButtonTrigger firstButton;

    InputAction createCloneAction;
    InputAction cancelCloneAction;
    InputAction retryCloneAction;

    private bool tutorialStarted = false;

    private void Start()
    {
        createCloneAction = inputAsset.FindAction(createCloneActionName, true);
        cancelCloneAction = inputAsset.FindAction(cancelCloneActionName, true);
        retryCloneAction = inputAsset.FindAction(retryCloneActionName, true);

        createCloneAction.Disable();
        cancelCloneAction.Disable();
        retryCloneAction.Disable();

        CloneRecordingCreator.shouldHaveTimer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!tutorialStarted && other.CompareTag("Player"))
        {
            tutorialStarted = true;
            StartCoroutine(StartBasicsTutorial());
        }
    }

    private IEnumerator StartBasicsTutorial()
    {
        //wait until dialoguemanager is loaded, so we can show subtitles and popups
        yield return new WaitUntil(() => DialogueManager.instance != null);

        //[Subtitle/voiceline: "something something enter mindspace something something"]
        Debug.Log("voice: enter mindspace");

        //{Wait until subtitle/voiceline finished}
        //TODO

        //[Show popup: press C to create a clone]
        GameObject createClonePopup = DialogueManager.instance.AddHintPopup($"Press [{GetKeyNameForAction(createCloneAction)}] to create a clone.");

        //+ ability: press C
        createCloneAction.Enable();

        //{Wait until player creates clone} [destroy popup]
        yield return new WaitUntil(() => CloneRecordingCreator.instance != null);
        Destroy(createClonePopup);

        //[Subtitle/voiceline: Move to the button with the clone]
        Debug.Log("voice: move to button");

        //{Wait until player (still in clone character) touches button, door opens}
        yield return new WaitUntil(() => firstButton.IsPressed);

        //[Subtitle/voiceline: Dispose of the clone]
        Debug.Log("voice: kill that clone");

        //[Show popup: press X to dispose of the clone manually]
        GameObject cancelClonePopup = DialogueManager.instance.AddHintPopup($"Press [{GetKeyNameForAction(cancelCloneAction)}] to cancel a clone.");

        //+ ability: press X
        cancelCloneAction.Enable();

        //[Wait until player presses X, then destroy hint popup]
        yield return new WaitUntil(() => CloneRecordingPlayer.instance != null);
        Destroy(cancelClonePopup);

        //[Subtitle/voiceline: The clone will now repeats its movements, go through the door once the clone opens it.]
        Debug.Log("voice: the clone will repeat your mindspace actions and movements. Go through the door to advance.");

        //{Wait until player enters next room}
        //TODO

        //StartCoroutine(StartRetryAbilityTutorial());
    }

    private IEnumerator StartRetryAbilityTutorial()
    {
        //[Subtitle/voiceline: See that button over there? It extends a bridge. Doesn't stay extended though.]
        Debug.Log("Send a clone to the button to extend the bridge");
        return null;
        //[Show popup: press C to create a clone]

        //{Wait until player creates clone}
        //[Spawn the clone and give control, also show the timer]

        //[Subtitle/voiceline: The clone will automatically disintegrate after a few seconds. You can still dispose of it earlier if you want to.]

        //10
        //{Wait until player back in control of protagonist}
        //<if button not pressed during recording> => 
        //	[Subtitle/voiceline: The bridge is not going to open itself. Create a new clone and try again]
        //	GOTO 10
        //<else (if button pressed during recording)> =>
        //	[Somehow ensure player does not make it to the other side (for example, the bridge breaking)]
        //	[Subtitle/voiceline: You did not make it? Doesn't matter. You can just use the same clone and try again]
        //	[Show popup: Press R to retry with the last recording]
        //	+ ability: press R
        //	(This time, bridge stays normal and player can advance)
    }

    private string GetKeyNameForAction(InputAction inputAction)
    {
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();

        int bindingIndex = inputAction.GetBindingIndex(group: playerInput.currentControlScheme);

        return inputAction.GetBindingDisplayString(bindingIndex, out string deviceLayoutName, out string controlPath);
    }
}
