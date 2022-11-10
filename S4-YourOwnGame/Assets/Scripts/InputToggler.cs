using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputToggler : MonoBehaviour
{
    [SerializeField] CameraSensitivityController PlayerCameraSensitivityController;

    [ContextMenu("Disable")]
    public void DisableInputs() => ToggleUserInput(false);

    [ContextMenu("Enable")]
    public void EnableInputs() => ToggleUserInput(true);

    private void ToggleUserInput(bool isEnabled)
    {
        if (PlayerCameraSensitivityController != null)
            if (isEnabled)
                PlayerCameraSensitivityController.EnableMovement();
            else
                PlayerCameraSensitivityController.DisableMovement();
    }
}
