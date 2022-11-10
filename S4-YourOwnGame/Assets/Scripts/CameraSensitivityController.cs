using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class CameraSensitivityController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CameraObject;
    [SerializeField] CinemachineInputProvider CameraInput;
    [SerializeField] PlayerInput CurrentInput;
    [SerializeField] InputActionAsset InputMaps;

    [SerializeField] InputActionReference[] KeyboardMouseMapCameraControls;
    [SerializeField] float MouseHorizontalGain = 2f;
    [SerializeField] float MouseVerticalGain = 2f;

    [SerializeField] InputActionReference[] ControllerMapCameraControls;
    [SerializeField] float JoystickHorizontalGain = 1.5f;
    [SerializeField] float JoystickVerticalGain = 1.5f;

    [SerializeField] string DisableInputActionMapName = "PlayerDisable";

    private bool m_AllowSwitching = true;
    private bool m_ControllerEnabled = false;
    private int m_CurrentKeyboardMouseMap = 0;
    private int m_CurrentControllerMap = 0;

    private void Start()
    {
        if (CurrentInput != null)
            CurrentInput.SwitchCurrentControlScheme(InputSystem.devices.ToArray());
    }

    public void OnInputDeviceChange()
    {
        if (m_AllowSwitching)
        {
            m_ControllerEnabled = !m_ControllerEnabled;
            SetNewActionMap();
        }
    }

    public void OnActionMapChange()
    {
        if (m_AllowSwitching)
        {
            AdvanceInputMapIndex();
            SetNewActionMap();
        }
    }

    public void EnableSwitching() => m_AllowSwitching = true;

    public void DisableSwitching() => m_AllowSwitching = false;

    public void EnableMovement() => SetNewActionMap();

    public void DisableMovement()
    {
        InputActionMap FoundMap = InputMaps.FindActionMap(DisableInputActionMapName);
        if (FoundMap != null)
        {
            InputDevice[] FoundDevices = InputSystem.devices.ToArray().Where(x => x.description.deviceClass == "Keyboard" || x.description.deviceClass == "Mouse").ToArray();
            if (FoundDevices.Length > 0)
            {
                CurrentInput.currentActionMap = FoundMap;
                CurrentInput.SwitchCurrentControlScheme(FoundDevices);
            }
        }
    }

    public CameraSensitivitySettings GetCameraSettings() => new CameraSensitivitySettings(m_ControllerEnabled, m_CurrentKeyboardMouseMap, m_CurrentControllerMap, m_AllowSwitching);

    public void SetCameraSettings(CameraSensitivitySettings Settings)
    {
        m_AllowSwitching = Settings.AllowSwitching;
        m_ControllerEnabled = Settings.ControllerEnabled;
        m_CurrentKeyboardMouseMap = Settings.CurrentKeyboardMouseMap;
        m_CurrentControllerMap = Settings.CurrentControllerMap;
        SetNewActionMap();
    }

    private void SetNewActionMap()
    {
        int CurrentIndex = m_ControllerEnabled ? m_CurrentControllerMap : m_CurrentKeyboardMouseMap;
        InputActionReference NewMap = m_ControllerEnabled ? ControllerMapCameraControls[CurrentIndex] : KeyboardMouseMapCameraControls[CurrentIndex];

        if (NewMap != null)
        {
            InputActionMap FoundMap = InputMaps.FindActionMap(NewMap.name.Split('/')[0]);
            if (FoundMap != null)
            {
                InputDevice[] FoundDevices = GetInputDevices();
                if (FoundDevices.Length > 0)
                {
                    CurrentInput.currentActionMap = FoundMap;
                    CurrentInput.SwitchCurrentControlScheme(FoundDevices);
                    SetCameraInputActionMap(NewMap);
                    if (m_ControllerEnabled)
                        ChangeCameraSensitivity(JoystickHorizontalGain, JoystickVerticalGain);
                    else
                        ChangeCameraSensitivity(MouseHorizontalGain, MouseVerticalGain);
                }
            }
        }
    }

    private InputDevice[] GetInputDevices()
    {
        InputDevice[] FoundDevices = null;
        if (m_ControllerEnabled)
            FoundDevices = InputSystem.devices.ToArray().Where(x => x.description.deviceClass != "Keyboard" && x.description.deviceClass != "Mouse").ToArray();
        else
            FoundDevices = InputSystem.devices.ToArray().Where(x => x.description.deviceClass == "Keyboard" || x.description.deviceClass == "Mouse").ToArray();
        return FoundDevices;
    }

    private void AdvanceInputMapIndex()
    {
        if (m_ControllerEnabled)
        {
            if (++m_CurrentControllerMap >= ControllerMapCameraControls.Length)
                m_CurrentControllerMap = 0;
        }
        else
        {
            if (++m_CurrentKeyboardMouseMap >= KeyboardMouseMapCameraControls.Length)
                m_CurrentKeyboardMouseMap = 0;
        }
    }

    private void ChangeCameraSensitivity(float HorizontalSensitivity, float VerticalSensitivity)
    {
        if (CameraObject != null)
        {
            CinemachinePOV Aim = CameraObject.GetCinemachineComponent<CinemachinePOV>();
            if (Aim != null)
            {
                Aim.m_HorizontalAxis.m_MaxSpeed = HorizontalSensitivity;
                Aim.m_VerticalAxis.m_MaxSpeed = VerticalSensitivity;
            }
        }
    }

    private void SetCameraInputActionMap(InputActionReference NewMap)
    {
        if (CameraInput != null)
            CameraInput.XYAxis = NewMap;
    }
}

public struct CameraSensitivitySettings
{
    public bool ControllerEnabled { get; set; }
    public int CurrentKeyboardMouseMap { get; set; }
    public int CurrentControllerMap { get; set; }
    public bool AllowSwitching { get; set; }

    public CameraSensitivitySettings(bool ControllerEnabled, int CurrentKeyboardMouseMap, int CurrentControllerMap, bool AllowSwitching)
    {
        this.ControllerEnabled = ControllerEnabled;
        this.CurrentKeyboardMouseMap = CurrentKeyboardMouseMap;
        this.CurrentControllerMap = CurrentControllerMap;
        this.AllowSwitching = AllowSwitching;
    }
}
