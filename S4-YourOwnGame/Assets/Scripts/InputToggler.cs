using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputToggler : MonoBehaviour
{
    [ContextMenu("Disable")]
    public void DisableInputs()
    {
        foreach(PlayerInput input in FindObjectsOfType<PlayerInput>())
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
