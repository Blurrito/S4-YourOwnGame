//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class InputToggler : MonoBehaviour
//{
//    [SerializeField] CameraSensitivityController PlayerCameraSensitivityController;

//    [ContextMenu("Disable")]
//    public void DisableInputs() => ToggleUserInput(false);

//    [ContextMenu("Enable")]
//    public void EnableInputs() => ToggleUserInput(true);

//    private void ToggleUserInput(bool isEnabled)
//    {
//        if (PlayerCameraSensitivityController != null)
//            if (isEnabled)
//                PlayerCameraSensitivityController.EnableMovement();
//            else
//                PlayerCameraSensitivityController.DisableMovement();
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputToggler : MonoBehaviour
{
    [ContextMenu("Disable")]
    public void DisableInputs()
    {
        foreach (PlayerInput input in FindObjectsOfType<PlayerInput>())
        {
            input.enabled = false;
        }
    }

    [ContextMenu("Enable")]
    public void EnableInputs()
    {
        foreach (PlayerInput input in FindObjectsOfType<PlayerInput>())
        {
            input.enabled = true;
        }
    }
}
