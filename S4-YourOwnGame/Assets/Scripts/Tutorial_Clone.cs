using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Tutorial_Clone : MonoBehaviour
{
    [SerializeField] InputActionAsset inputAsset;

    [SerializeField] string createCloneActionName;
    [SerializeField] string cancelCloneActionName;
    [SerializeField] string retryCloneActionName;

    [SerializeField] ButtonTrigger firstButton;
    [SerializeField] ButtonTrigger EnterSecondRoomTrigger;
    [SerializeField] ButtonTrigger secondButton;
    [SerializeField] ButtonTrigger midBridgeTrigger;
    [SerializeField] ButtonTrigger bottomPitTrigger;

    [SerializeField] Rigidbody firstBridgePart;
    [SerializeField] Rigidbody secondBridgePart;

    [SerializeField] DeathHandler bottomPitDeath;

    [SerializeField] GameObject BridgeNoCheatBarrier;

    InputAction createCloneAction;
    InputAction cancelCloneAction;
    InputAction retryCloneAction;

    private bool tutorialStarted = false;

    private bool pressedSecondButtonDuringRecording = false;

    private void Start()
    {
        createCloneAction = inputAsset.FindAction(createCloneActionName, true);
        cancelCloneAction = inputAsset.FindAction(cancelCloneActionName, true);
        retryCloneAction = inputAsset.FindAction(retryCloneActionName, true);
    }

    private void Update()
    {
        if (secondButton.IsPressed && CloneRecordingCreator.instance != null) pressedSecondButtonDuringRecording = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!tutorialStarted && other.CompareTag("Player"))
        {
            createCloneAction.Disable();
            cancelCloneAction.Disable();
            retryCloneAction.Disable();

            CloneManager.instance.canUseClones = true;

            CloneRecordingCreator.shouldHaveTimer = false;
            tutorialStarted = true;
            StartCoroutine(StartBasicsTutorial());
        }
    }

    private IEnumerator StartBasicsTutorial()
    {
        //wait until dialoguemanager is loaded, so we can show subtitles and popups
        yield return new WaitUntil(() => DialogueManager.instance != null);

        //[Subtitle/voiceline: "something something enter mindspace something something"]
        DialogueManager.instance.AddDialogueToQueue("CloneTutorial_EnterMindspace");

        //[Show popup: press C to create a clone]
        GameObject createClonePopup = DialogueManager.instance.AddHintPopup($"Press [{GetKeyNameForAction(createCloneAction)}] to create a clone.");

        //+ ability: press C
        createCloneAction.Enable();

        //{Wait until player creates clone} [destroy popup]
        yield return new WaitUntil(() => CloneRecordingCreator.instance != null);
        Destroy(createClonePopup);

        //[Subtitle/voiceline: Move to the button with the clone]
        DialogueManager.instance.AddDialogueToQueue("CloneTutorial_MoveToButton");

        //{Wait until player (still in clone character) touches button, door opens}
        yield return new WaitUntil(() => firstButton.IsPressed);

        //[Subtitle/voiceline: Dispose of the clone]
        DialogueManager.instance.AddDialogueToQueue("CloneTutorial_DisposeOfClone");

        //[Show popup: press X to dispose of the clone manually]
        GameObject cancelClonePopup = DialogueManager.instance.AddHintPopup($"Press [{GetKeyNameForAction(cancelCloneAction)}] to exit your mindspace.");

        //+ ability: press X
        cancelCloneAction.Enable();

        yield return new WaitForSeconds(0.5f);
        CloneRecordingCreator.instance.GetComponent<StarterAssets.ThirdPersonController>().enabled = false;

        //[Wait until player presses X, then destroy hint popup]
        yield return new WaitUntil(() => CloneRecordingPlayer.instance != null);
        Destroy(cancelClonePopup);

        //[Subtitle/voiceline: The clone will now repeat its movements, go through the door once the clone opens it.]
        DialogueManager.instance.AddDialogueToQueue("CloneTutorial_ExplainReplay");

        //{Wait until player enters next room}
        yield return new WaitUntil(() => EnterSecondRoomTrigger.IsPressed);
        Destroy(EnterSecondRoomTrigger.gameObject);

        CloneRecordingCreator.shouldHaveTimer = true;

        StartCoroutine(StartRetryAbilityTutorial());
    }

    private IEnumerator StartRetryAbilityTutorial()
    {
        //[Subtitle/voiceline: See that button over there? It extends a bridge. Doesn't stay extended though.]
        DialogueManager.instance.AddDialogueToQueue("CloneTutorial_PressButtonToOpenBridge");

        //[Show popup: press C to create a clone]
        GameObject createClonePopup = DialogueManager.instance.AddHintPopup($"Press [{GetKeyNameForAction(createCloneAction)}] to create a clone.");

        //{Wait until player creates clone}
        yield return new WaitUntil(() => CloneRecordingCreator.instance != null);
        Destroy(createClonePopup);

        //[Subtitle/voiceline: The clone will automatically disintegrate after a few seconds. You can still dispose of it earlier if you want to.]
        DialogueManager.instance.AddDialogueToQueue("CloneTutorial_AutomaticDisintegration");

        secondStage:
        //{Wait until player back in control of protagonist}
        yield return new WaitUntil(() => CloneRecordingPlayer.instance != null);

        //<if button not pressed during recording> => 
        if (!pressedSecondButtonDuringRecording)
        {
            //	[Subtitle/voiceline: The bridge is not going to open itself. Create a new clone and try again]
            DialogueManager.instance.AddDialogueToQueue("CloneTutorial_CreateNewCloneToTryAgain");
            yield return new WaitUntil(() => CloneRecordingCreator.instance != null);
            goto secondStage;
        }
        else //<else (if button pressed during recording)> =>
        {
            bottomPitDeath.TriggerPlayerDeath = false;

            //[Somehow ensure player does not make it to the other side (for example, the bridge breaking)]
            midBridgeTrigger.gameObject.SetActive(true);
            yield return new WaitUntil(() => midBridgeTrigger.IsPressed);
            Destroy(midBridgeTrigger.gameObject);
            firstBridgePart.isKinematic = false;
            secondBridgePart.isKinematic = false;
            bottomPitTrigger.gameObject.SetActive(true);

            yield return new WaitUntil(() => bottomPitTrigger.IsPressed);
            yield return new WaitForSeconds(3);
            DialogueManager.instance.AddDialogueToQueue("CloneTutorial_RetryCloneRecording");
            Destroy(BridgeNoCheatBarrier);

            //	[Show popup: Press R to retry with the last recording]
            GameObject retryClonePopup = DialogueManager.instance.AddHintPopup($"Press [{GetKeyNameForAction(retryCloneAction)}] to retry with the latest clone recording.");

            //+ ability: press R
            retryCloneAction.Enable();

            yield return new WaitUntil(() => !bottomPitTrigger.IsPressed);
            bottomPitDeath.TriggerPlayerDeath = true;
            firstBridgePart.isKinematic = true;
            secondBridgePart.isKinematic = true;
            Destroy(retryClonePopup);
        }
    }

    private string GetKeyNameForAction(InputAction inputAction)
    {
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();

        int bindingIndex = inputAction.GetBindingIndex(group: playerInput.currentControlScheme);

        return inputAction.GetBindingDisplayString(bindingIndex, out string deviceLayoutName, out string controlPath);
    }
}
